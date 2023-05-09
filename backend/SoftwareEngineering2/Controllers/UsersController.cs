using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace SoftwareEngineering2.Controllers {
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) {
            _userService = userService;
        }

        // POST: api/users/log_in
        [HttpPost("log_in")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorised")]
        [SwaggerResponse(200, "OK")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO) {
            if (string.IsNullOrWhiteSpace(loginDTO.Username) || string.IsNullOrWhiteSpace(loginDTO.Password))
                return BadRequest(new { message = "Nonempty username and password are required" });

            var result = await _userService.CreateJWTToken(loginDTO.Username, loginDTO.Password);

            if (result is null)
                return Unauthorized();

            return Ok(result);
        }

        // GET: api/users
        [HttpGet(Name = "GetUser")]
        [SwaggerResponse(200, "Get information about currently authenticated User", typeof(UserDTO))]
        [SwaggerResponse(401, "Unauthorised")]
        public async Task<IActionResult> Get() {
            if (!int.TryParse(User.FindFirst("UserID")?.Value, out int id))
                return Unauthorized();

            var user = await _userService.GetUserByID(id);
            return user is not null ? Ok(user) : Unauthorized();
        }

        // POST: api/users
        [HttpPost]
        [SwaggerResponse(201, "User created", typeof(UserDTO))]
        [SwaggerResponse(400, "User is invalid")]
        [SwaggerResponse(409, "User already exists")]
        public async Task<IActionResult> CreateUser([FromBody][Required] NewUserDTO newUser) {
            if (string.IsNullOrWhiteSpace(newUser.Email)
                || string.IsNullOrWhiteSpace(newUser.Password)
                || string.IsNullOrWhiteSpace(newUser.Name))
                return BadRequest(new { message = "Nonempty name, email and password are required" });

            if (await _userService.GetUserByEmail(newUser.Email) is not null)
                return Conflict(new { message = "User with given email address already exists" });

            var result = await _userService.CreateUser(newUser);
            return CreatedAtAction(nameof(Get), result);
        }

        // GET: api/users/profilePicture
        [HttpGet("profilePicture", Name = "profilePicture")]
        [SwaggerResponse(200, "Get profile picture")]
        [SwaggerResponse(401, "Unauthorised")]
        public Task<IActionResult> GetPicture(int id) {
            throw new NotImplementedException();
        }

        // POST: api/users/profilePicture
        [HttpPost("profilePicture", Name = "profilePicture")]
        [Authorize(Roles = "client")]
        [SwaggerResponse(200, "Profil picture updated")]
        [SwaggerResponse(401, "Unauthorised")]
        public Task<IActionResult> UpdatePicture() {
            throw new NotImplementedException();
        }

        // POST: api/users/newsletter
        [HttpPost("newsletter")]
        [Authorize(Roles = "client")]
        [SwaggerResponse(200, "OK")]
        [SwaggerResponse(401, "Unauthorized")]
        public async Task<IActionResult> SubscribeNewsletter([FromBody] NewsletterDTO newsletterDTO) {
            if (!int.TryParse(User.FindFirst("UserID")?.Value, out int id))
                return Unauthorized();

            if (await _userService.UpdateNewsletter(id, newsletterDTO.Subscribed))
                return Ok("Succesfully changed newsletter status");

            return Unauthorized();
        }
    }
}
