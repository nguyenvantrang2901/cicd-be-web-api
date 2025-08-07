using APIs_Manager_Inventory.Data;
using APIs_Manager_Inventory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIs_Manager_Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly HomeDBContext _context;
        public DeliveryController(HomeDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Delivery>>> GetAllDelivery()
        {
            var deliveries = await _context.Sys_Delivery.ToListAsync();
            if(deliveries == null)
            {
                return NotFound("No data delivery");
            }
            return Ok(new
            {
                success = true,
                message = "Found data deliveries",
                data = deliveries
            });
        }

        [HttpGet("GetDeliveryById/{id}")]
        public async Task<ActionResult<Delivery>> GetDeliveryById(int id)
        {
            var delivery = await _context.Sys_Delivery.FindAsync(id);
            if (delivery == null)
            {
                return NotFound("No data delivery");
            }
            return Ok(new
            {
                success = true,
                message = "Found data delivery",
                data = delivery
            });
        }

        [HttpPost("CreateNewDelivery")]
        public async Task<ActionResult<Delivery>> CreateNewDelivery(Delivery newDelivery)
        {
            _context.Sys_Delivery.Add(newDelivery);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Create new delivery successfully",
                data = newDelivery
            });
        }

        [HttpPut("UpdateDelivery/{id}")]
        public async Task<ActionResult<Delivery>> UpdateDelivery(int id, Delivery updateDelivery)
        {
            if(id != updateDelivery.Id)
            {
                return NotFound("No data delivery");
            }
            _context.Entry(updateDelivery).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Update delivery successfully",
                data = updateDelivery
            });
        }

        [HttpDelete("DeleteDelivery/{id}")]
        public async Task<ActionResult<Delivery>> DeleteDelivery(int id)
        {
            var delivery = await _context.Sys_Delivery.FindAsync(id);
            if(delivery == null)
            {
                return NotFound("No data delivery");
            }
            _context.Sys_Delivery.Remove(delivery);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Delete delivery successfully, id: " + delivery.Id,
            });
        }
    }
}
