using AF.Core.Database.Entities;
using AF.Core.Database.Repositories;
using AF.Core.Extensions;
using FluentValidation;
using LinqToDB;
using MediatR;

namespace AF.Core.Features.Shelters;

public record AssignUserToShelterCommand(Guid UserId, Guid ShelterId) : IRequest<ShelterUser>;

public class AssignUserToShelterCommandValidator : AbstractValidator<AssignUserToShelterCommand>
{
    public AssignUserToShelterCommandValidator(IUserRepository userRepository, IShelterRepository shelterRepository,
        IShelterUserRepository shelterUserRepository)
    {
        RuleFor(x => x.UserId)
            .EntityExists(userRepository);

        RuleFor(x => x.ShelterId)
            .EntityExists(shelterRepository);

        RuleFor(x => x)
            .MustAsync(async (command, ct) =>
                !await shelterUserRepository.Items.AnyAsync(
                    x => x.ShelterId == command.ShelterId && x.UserId == command.UserId, ct))
            .WithMessage("Entities are already connected.");
    }
}