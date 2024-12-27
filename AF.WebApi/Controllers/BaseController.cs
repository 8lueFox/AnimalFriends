using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AF.WebApi.Controllers;

public class BaseController(IMediator mediator) : ControllerBase
{
    protected readonly IMediator Mediator = mediator;
}