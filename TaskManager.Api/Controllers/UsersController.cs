using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Halper;
using TaskManager.Api.Models;
using TaskManager.Api.Models.Data;
using TaskManager.Api.Models.Services;
using TaskManager.Common.Models;

namespace TaskManager.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ApplicationContext _db;
        public UsersController(ApplicationContext db)
        {
            _db = db;
            _userService = new UserService(db);
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] UserModel userModel)
        {
            if (userModel != null)
            {
               bool result = _userService.Create(userModel);
               return result ? Ok() : NotFound();
            }
            return BadRequest();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPatch("update/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserModel userModel) 
        {
            if (userModel != null)
            {
                bool result = _userService.Update(id, userModel);
                return result ? Ok() : NotFound();
            }
            return BadRequest();
        }
        //[Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            bool result = _userService.Delete(id);
            return result ? Ok() : NotFound();
        }
        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetUsers() 
        {
            return await _db.Users.Select(u => u.ToDto()).ToListAsync();
        }
        
    }
}
