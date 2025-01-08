using AF.Core.Database.Repositories;
using AF.Core.Extensions;
using FluentValidation;
using MediatR;

namespace AF.Core.Features.Shelters;

public record ChangeUserPermissionsToShelterCommand(Guid UserId, Guid ShelterId, bool IsAdmin, bool IsOwner) : IRequest;

public class ChangeUserPermissionsToShelterCommandValidator : AbstractValidator<ChangeUserPermissionsToShelterCommand>
{
    public ChangeUserPermissionsToShelterCommandValidator(IShelterUserRepository shelterUserRepository)
    {
        When(x => x.IsOwner, () =>
        {
            RuleFor(x => x.IsAdmin)
                .Must(x => x)
                .WithMessage("Owner must to be a admin.");
        });
    }
}

internal class ChangeUserPermissionsToShelterCommandHandler(IShelterUserRepository shelterUserRepository) : IRequestHandler<ChangeUserPermissionsToShelterCommand>
{
    public Task Handle(ChangeUserPermissionsToShelterCommand request, CancellationToken cancellationToken)
    {
        var entry = shelterUserRepository.GetById(request.UserId, request.ShelterId);
        
        entry.IsAdmin = request.IsAdmin;
        entry.IsOwner = request.IsOwner;
        
        shelterUserRepository.Update(entry);
        
        return Task.CompletedTask;
    }
}