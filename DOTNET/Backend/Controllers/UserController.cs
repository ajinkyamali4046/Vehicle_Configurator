using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using v_conf_dn.Models;
using v_conf_dn.Services;
using v_conf_dn.Dto;
using LoginRequest = v_conf_dn.Dto.LoginRequest;

namespace v_conf_dn.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;

        public UserController(IUser userService)
        {
            _userService = userService;
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<User>> GetUserId(string username)
        {
            return await _userService.getUSerId(username);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            try
            {
                var userIdResult = await _userService.getUSerId(user.Username);

                // Check if userIdResult is null or has a value
               

                var result = await _userService.createUser(user);
                return Ok(result);
            }
            catch (DbUpdateException ex)
            {
                return Conflict("Integrity constraint violation occurred: " + (ex.InnerException?.Message ?? ex.Message));
            }
            catch (Exception ex)
            {
                return Conflict("An unexpected error occurred: " + ex.Message);
            }
        }





        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Invalid login request.");
            }

            var user = await _userService.AuthenticateAsync(request.Username, request.Password);

            if (user == null)
            {
                return Unauthorized(); // Authentication failed
            }

            // You can return a token or user details here as needed
            return Ok(new { Message = "Login successful" });
        }
    }
    }
