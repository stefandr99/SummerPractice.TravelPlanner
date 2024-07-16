namespace TravelPlanner.API.Controllers;

using Application.Models.User.Requests;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

public class UserController(IUserService userService) : BaseController
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestModel model)
    {
        try
        {
            await userService.RegisterAsync(model.Username, model.Password, model.Email);

            return this.Ok();
        }
        catch (ArgumentException ex)
        {
            return this.BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
    {
        var result = await userService.AuthenticateAsync(model.Username, model.Password);

        if (!result.IsSuccess)
            return this.Unauthorized();

        return this.Ok();
    }

    [HttpPost("verify-email")]
    public async Task<IActionResult> VerifyEmail(int userId, [FromBody] VerifyEmailRequestModel model)
    {
        try
        {
            await userService.VerifyEmailAsync(userId, model.VerificationToken);

            return this.Ok();
        }
        catch (ArgumentException ex)
        {
            return this.BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var result = await userService.GetUserByIdAsync(id);

        if (!result.IsSuccess)
        {
            return this.NotFound();
        }

        return this.Ok(result.Data);
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetUserByUsernameOrEmail(string query)
    {
        var result = await userService.GetUserByUsernameOrEmailAsync(query);

        if (!result.IsSuccess)
        {
            return this.NotFound();
        }

        return this.Ok(result.Data);
    }
}