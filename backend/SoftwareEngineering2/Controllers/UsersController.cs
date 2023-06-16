using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftwareEngineering2.DTO;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Profiles;
using Swashbuckle.AspNetCore.Annotations;

namespace SoftwareEngineering2.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase {
    private readonly IUserService _userService;

    public UsersController(IUserService userService) {
        _userService = userService;
    }

    // POST: api/users/log_in
    [HttpPost("log_in")]
    [SwaggerOperation(Summary = "Log into system", Description = "Difference: -")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(401, "Unauthorised")]
    [SwaggerResponse(200, "OK")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto) {
        if (string.IsNullOrWhiteSpace(loginDto.Username) || string.IsNullOrWhiteSpace(loginDto.Password))
            return BadRequest(new { message = "Nonempty username and password are required" });

        var result = await _userService.CreateJwtToken(loginDto.Username, loginDto.Password);

        if (result is null)
            return Unauthorized();

        return Ok(result);
    }

    // GET: api/users
    [HttpGet(Name = "GetUser")]
    [SwaggerOperation(Summary = "Get information about currently authenticated user",
        Description = "Difference: user id")]
    [SwaggerResponse(200, "Get information about currently authenticated user", typeof(UserDto))]
    [SwaggerResponse(401, "Unauthorised")]
    public async Task<IActionResult> Get() {
        if (!int.TryParse(User.FindFirst("UserID")?.Value, out var id))
            return Unauthorized();

        var user = await _userService.GetUserById(User.FindFirstValue(ClaimTypes.Role)!, id);
        return user is not null ? Ok(user) : Unauthorized();
    }

    // POST: api/users
    [HttpPost]
    [SwaggerOperation(Summary = "Creates a user",
        Description = "Difference: role of created user (not based on email)")]
    [SwaggerResponse(201, "User created", typeof(UserDto))]
    [SwaggerResponse(400, "User is invalid")]
    [SwaggerResponse(403, "Forbidden")]
    [SwaggerResponse(409, "User already exists")]
    public async Task<IActionResult> CreateUser([FromBody] [Required] NewUserDto newUser) {
        var role = newUser.Role is null || !Roles.IsValid(newUser.Role) ? Roles.Client : newUser.Role;

        if (!User.IsInRole(Roles.Employee) && role != Roles.Client)
            return Forbid();

        if (string.IsNullOrWhiteSpace(newUser.Email)
            || string.IsNullOrWhiteSpace(newUser.Password)
            || string.IsNullOrWhiteSpace(newUser.Name))
            return BadRequest(new { message = "Nonempty name, email and password are required" });

        if (await _userService.GetUserByEmail(newUser.Email) is not null)
            return Conflict(new { message = "User with given email address already exists" });

        var result = await _userService.CreateUser(newUser with { Role = role });
        return CreatedAtAction(nameof(Get), result);
    }

    // POST: api/users/newsletter
    [HttpPost("newsletter")]
    [SwaggerOperation(Summary = "Update newsletter preference", Description = "Difference: -")]
    [Authorize(Roles = Roles.Client)]
    [SwaggerResponse(200, "OK")]
    [SwaggerResponse(401, "Unauthorized")]
    public async Task<IActionResult> SubscribeNewsletter([FromBody] NewsletterDto newsletterDto) {
        if (!int.TryParse(User.FindFirst("UserID")?.Value, out var id))
            return Unauthorized();

        if (await _userService.UpdateNewsletter(id, newsletterDto.Subscribed))
            return Ok("Successfully changed newsletter status");

        return Unauthorized();
    }
}