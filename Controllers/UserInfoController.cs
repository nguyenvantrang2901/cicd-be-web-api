using APIs_Manager_Inventory.Data;
using APIs_Manager_Inventory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIs_Manager_Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly HomeDBContext _context;
        public UserInfoController(HomeDBContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllUserInfo")]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetAllUserInfo()
        {
            var users = await _context.Sys_User.ToListAsync();
            return Ok(new
            {
                success = true,
                message = "Get User Info",
                data = users
            });
        }

        [HttpPost("CreateNewUser")]
        public async Task<ActionResult<UserInfo>> CreateNewUser(UserInfo newUser)
        {
            newUser.Id = Guid.NewGuid();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _context.Sys_User.Add(newUser);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Create new user successfully",
                data = newUser
            });
        }
        //Get user by Id
        [HttpGet("GetUserInfoById/{id}")]
        public async Task<ActionResult<UserInfo>> GetUserInfoById(Guid id)
        {
            var currentUser = await _context.Sys_User.FindAsync(id);
            if (currentUser == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "User not found"
                });
            }
            return Ok(new
            {
                success = true,
                message = "Found User",
                data = currentUser
            });
        }
        //Update
        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UserInfo user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Sys_User.Any(e => e.Id == id))
                {
                    return NotFound();
                }
            }
            return Ok(new
            {
                success = true,
                message = "Update user successfully",
                data = user
            });
        }


        [HttpDelete("DeleteUser/{id}")]
        public async Task<ActionResult<UserInfo>> DeleteUser(Guid id)
        {
            var user = await _context.Sys_User.FindAsync(id);
            if (user == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "User not found"
                });
            }
            _context.Sys_User.Remove(user);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Delete user successfully"
            });
        }
    }
}
