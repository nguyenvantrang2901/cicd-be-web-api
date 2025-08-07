using APIs_Manager_Inventory.Data;
using APIs_Manager_Inventory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIs_Manager_Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly HomeDBContext _context;
        public ProductController(HomeDBContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllProduct")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProduct()
        {
            var products = await _context.Sys_Product.ToListAsync();
            return Ok(new
            {
                success = true,
                message = "Found product",
                data = products
            });
        }

        [HttpPost("CreateNewProduct")]
        public async Task<ActionResult<Product>> CreateNewProduct(Product newProduct)
        {
            _context.Sys_Product.Add(newProduct);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Create new product successfully",
                data = newProduct
            });
        }

        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = _context.Sys_Product.FindAsync(id);
            if(product == null)
            {
                return NotFound("No data product");
            }
            return Ok(new
            {
                success = true,
                message = "Found product Ok",
                data = product
            });
        }

        [HttpPut("UpdateProduct/{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product updateProduct)
        {
            if(id != updateProduct.Id)
            {
                return NotFound("No data product found");
            }
            _context.Entry(updateProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Update product successfully",
                data = updateProduct
            });
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Sys_Product.FindAsync(id);
            if(product == null)
            {
                return NotFound("No data product found");
            }
            _context.Sys_Product.Remove(product);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Delete product successfully, id: " + product.Id,
            });
        }
    }
}
