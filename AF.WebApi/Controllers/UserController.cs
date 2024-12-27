using AF.Core.Features.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AF.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateUserCommand command)
    {
        var user = await mediator.Send(command);
        
        return Ok(user.Id);
    }

    [HttpPut]
    public async Task<ActionResult<Guid>> Update(UpdateUserCommand command)
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
}