using AF.Core.Features.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AF.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UserController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<Guid>> Create(CreateUserCommand command)
    {
        var user = await mediator.Send(command);
        
        return Ok(user.Id);
    }
    
    [HttpPost("signIn")]
    [AllowAnonymous]
    public async Task<ActionResult<string>> SignIn(SignInCommand command)
        => await mediator.Send(command);

    [HttpPut]
    public async Task<ActionResult> Update(UpdateUserCommand command)
    {
        await mediator.Send(command);
        
        return NoContent();
    }

    [HttpPut("ChangePassword")]
    public async Task<ActionResult<Guid>> ChangePassword(ChangePasswordCommand command)
    {
        await mediator.Send(command);
        
        return NoContent();
    }
    
    [HttpGet]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
        => await mediator.Send(new GetCurrentUserQuery());
}