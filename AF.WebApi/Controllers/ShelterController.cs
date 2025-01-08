using AF.Core.Features.Shelters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AF.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ShelterController(IMediator mediator) : BaseController(mediator)
{

    [HttpGet]
    public async Task<ActionResult<ShelterDto>> GetShelter([FromQuery]GetShelterInfoQuery query)
        => Ok(await mediator.Send(query));
    
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateShelterCommand command)
    {
        var shelter = await mediator.Send(command);

        return Ok(shelter.Id);
    }
    
}