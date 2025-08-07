using APIs_Manager_Inventory.Data;
using APIs_Manager_Inventory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIs_Manager_Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly HomeDBContext _context;
        public OrderController(HomeDBContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllOrder")]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrder()
        {
            var order = await _context.Sys_Order.Include(o => o.Items)
                            .ThenInclude(i => i.Product).ToListAsync();
            if (order == null)
            {
                return NotFound("No data order");
            }
            return Ok(new
            {
                success= true,
                message = "Found order",
                data = order
            });
        }
        
        [HttpGet("GetOrderById/{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _context.Sys_Order.Include(o => o.Items)
                .ThenInclude(i => i.Product).FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return NotFound("No data order");
            }
            return Ok(new
            {
                success= true,
                message = "Found Order",
                data = order
            });
        }

        [HttpPost("CreateNewOrder")]
        public async Task<ActionResult<Order>> CreateNewOrder(Order newOrder)
        {
            _context.Sys_Order.Add(newOrder);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success= true,
                message = "Create new order successfully",
                data = newOrder
            });
        }

        [HttpPut("UpdateOrder/{id}")]
        public async Task<ActionResult<Order>> UpdateOrder(int id, Order updateOrder)
        {
            if(id != updateOrder.Id)
            {
                return NotFound("No data order");
            }
            _context.Entry(updateOrder).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success= true,
                message = "Update order successfully",
                data = updateOrder
            });
        }

        [HttpDelete("DeleteOrder/{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var order = await _context.Sys_Order.FindAsync(id);
            if(order == null)
            {
                return NotFound("No data order");
            }
            _context.Sys_Order.Remove(order);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success= true,
                message = "Delete order successfully, id: " + order.Id
            });

        }
    }
}
