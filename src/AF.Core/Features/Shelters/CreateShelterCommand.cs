using AF.Core.Database.Entities;
using AF.Core.Database.Repositories;
using AF.Core.Exceptions;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AF.Core.Features.Shelters;

public record CreateShelterCommand(string Name, string Address, string Phone, string Email, string? BankAccount)
    : IRequest<Shelter>;

public class CreateShelterCommandValidator : AbstractValidator<CreateShelterCommand>
{
    public CreateShelterCommandValidator()
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(3);
        
        RuleFor(x => x.Address)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(x => x.Phone)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(9);

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress();
    }
}

internal class CreateShelterCommandHandler(IMapper mapper, IShelterRepository shelterRepository,
    IUserRepository userRepository, IMediator mediator,
    RequestContext requestContext)
    : IRequestHandler<CreateShelterCommand, Shelter>
{
    public async Task<Shelter> Handle(CreateShelterCommand request, CancellationToken cancellationToken)
    {
        var entry = mapper.Map<Shelter>(request);
        
        var userId = requestContext.UserId;
        var user = userRepository.GetById((Guid)userId);
        if(user == null)
            throw new EntityDoesNotExistException($"User with id {userId} not found");
        
        shelterRepository.Add(entry);

        var shelterUser = await mediator.Send(new AssignUserToShelterCommand((Guid)userId, entry.Id), cancellationToken);
        
        await mediator.Send(new ChangeUserPermissionsToShelterCommand(shelterUser.UserId, shelterUser.ShelterId, true, true), cancellationToken);
        
        return entry;
    }
}