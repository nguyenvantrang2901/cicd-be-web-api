using APIs_Manager_Inventory.Data;
using APIs_Manager_Inventory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIs_Manager_Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeaderItemController : ControllerBase
    {
        private readonly HomeDBContext _context;
        public HeaderItemController (HomeDBContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllHeaderItem")]
        public async Task<ActionResult<IEnumerable<HeaderItem>>> GetAllHeaderItem()
        {
            var headerItems = await _context.Sys_HeaderItem.ToListAsync();
            return Ok(new
            {
                success = true,
                message = "Header Item found",
                data = headerItems
            });
        }

        [HttpPost("CreateNewHeaderItem")]
        public async Task<ActionResult<HeaderItem>> CreateNewHeaderItem(HeaderItem newHeader)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.Sys_HeaderItem.Add(newHeader);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Create new header item successfully",
                data = newHeader
            });
        }
    }
}
