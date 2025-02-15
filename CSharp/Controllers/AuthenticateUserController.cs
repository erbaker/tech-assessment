using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CSharp.Models;
using CSharp.Services;
namespace CSharp.Controllers;


[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthenticateUserController : ControllerBase
{
    private IUserAuthenticateService _userService;

    public AuthenticateUserController(IUserAuthenticateService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateUser model)
    {
        var user = await _userService.Authenticate(model.Username, model.Password);

        if (user == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAll();
        return Ok(users);
    }
}

