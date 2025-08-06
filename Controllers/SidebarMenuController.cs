using APIs_Manager_Inventory.Data;
using APIs_Manager_Inventory.DTO;
using APIs_Manager_Inventory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIs_Manager_Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SidebarMenuController : ControllerBase
    {
        private readonly HomeDBContext _context;
        public SidebarMenuController (HomeDBContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllSidebar")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllSidebarUpdate()
        {
            var sidebar = await _context.Sys_Sidebar
                .Include(s => s.Children)
                .ToListAsync();

            // Convert to DTO
            var sidebarDTOs = sidebar.Select(s => new SidebarDTO
            {
                Id = s.Id,
                Name = s.Name,
                Icon = s.Icon,
                Children = s.Children.Select(c => new SidebarItemDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Route = c.Route,
                    SidebarId = c.SidebarId
                }).ToList()
            }).ToList();

            // Group by Name + Icon
            var groupedSidebar = sidebarDTOs
                .GroupBy(s => new { s.Name, s.Icon })
                .Select(g => new
                {
                    Id = g.First().Id, // hoặc để null nếu không cần
                    Name = g.Key.Name,
                    Icon = g.Key.Icon,
                    Children = g.SelectMany(x => x.Children)
                                .Select(c => new
                                {
                                    c.Id,
                                    c.Name,
                                    c.Route
                                }).ToList()
                })
                .ToList();

            return Ok(new
            {
                success = true,
                message = "Sidebar grouped successfully",
                data = groupedSidebar
            });
        }

        [HttpGet("GetSidebarByHeaderId/{headerId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetSidebarByHeaderId(int headerId)
        {
            var sidebar = await _context.Sys_Sidebar
                .Where(s => s.HeaderItemId == headerId)
                .Include(s => s.Children)
                .ToListAsync();

            var sidebarDTOs = sidebar.Select(s => new SidebarDTO
            {
                Id = s.Id,
                Name = s.Name,
                Icon = s.Icon,
                Children = s.Children.Select(c => new SidebarItemDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Route = c.Route,
                    SidebarId = c.SidebarId
                }).ToList()
            }).ToList();

            // GỘP theo Name + Icon
            var groupedSidebar = sidebarDTOs
                .GroupBy(s => new { s.Name, s.Icon })
                .Select(g => new
                {
                    Id = g.First().Id, // giữ ID đầu tiên, hoặc null
                    Name = g.Key.Name,
                    Icon = g.Key.Icon,
                    Children = g.SelectMany(x => x.Children)
                                .Select(c => new
                                {
                                    c.Id,
                                    c.Name,
                                    c.Route
                                }).ToList()
                }).ToList();

            return Ok(new
            {
                success = true,
                message = $"Sidebar for header {headerId}",
                data = groupedSidebar
            });
        }

        // GET BY ID
        [HttpGet("GetSidebarById/{id}")]
        public async Task<ActionResult<Sidebar>> GetSidebarById(int id)
        {
            var sidebar = await _context.Sys_Sidebar
                .Include(s => s.Children)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sidebar == null) return NotFound();
            return Ok(new
            {
                success = true,
                message = "Found Sidebar Ok",
                data = sidebar
            });
        }

        // CREATE
        [HttpPost("CreateNewSidebar")]
        public async Task<ActionResult> CreateNewSidebar([FromBody] SidebarDTO dto)
        {
            var sidebar = new Sidebar
            {
                Name = dto.Name,
                Icon = dto.Icon,
                Children = dto.Children.Select(c => new SidebarItem
                {
                    Name = c.Name,
                    Route = c.Route
                }).ToList()
            };

            _context.Sys_Sidebar.Add(sidebar);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Create new sidebar successfully",
                data = sidebar
            });
        }

        // UPDATE
        [HttpPut("UpdateSidebar/{id}")]
        public async Task<ActionResult> UpdateSidebar(int id, [FromBody] SidebarDTO dto)
        {
            var sidebar = await _context.Sys_Sidebar
                .Include(s => s.Children)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sidebar == null) return NotFound();

            sidebar.Name = dto.Name;
            sidebar.Icon = dto.Icon;

            // Xoá hết rồi thêm lại
            _context.Sys_SidebarItem.RemoveRange(sidebar.Children);
            sidebar.Children = dto.Children.Select(c => new SidebarItem
            {
                Name = c.Name,
                Route = c.Route
            }).ToList();

            await _context.SaveChangesAsync();
            return Ok(sidebar);
        }

        // DELETE
        [HttpDelete("DeleteSidebar/{id}")]
        public async Task<ActionResult> DeleteSidebar(int id)
        {
            var sidebar = await _context.Sys_Sidebar
                .Include(s => s.Children)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sidebar == null) return NotFound();

            _context.Sys_Sidebar.Remove(sidebar);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Delete siderbar sucessfully with id: " + id
            });
        }
    }
}
