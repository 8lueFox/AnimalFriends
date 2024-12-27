using AF.Core.Database.Repositories;
using AF.Core.Extensions;
using FluentValidation;
using MediatR;

namespace AF.Core.Features.Shelters;

public record ChangeUserPermissionsToShelterCommand(Guid Id, bool IsAdmin, bool IsOwner) : IRequest;

public class ChangeUserPermissionsToShelterCommandValidator : AbstractValidator<ChangeUserPermissionsToShelterCommand>
{
    public ChangeUserPermissionsToShelterCommandValidator(IShelterUserRepository shelterUserRepository)
    {
        RuleFor(x => x.Id)
            .EntityExists(shelterUserRepository);

        When(x => x.IsOwner, () =>
        {
            RuleFor(x => x.IsAdmin)
                .Must(x => x)
                .WithMessage("Owner must to be a admin.");
        });
    }
}