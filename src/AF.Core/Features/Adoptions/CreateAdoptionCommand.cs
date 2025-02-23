﻿using AF.Core.Database.Entities;
using AF.Core.Database.Repositories;
using AF.Core.Extensions;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AF.Core.Features.Adoptions;

public record CreateAdoptionCommand(
    Guid AnimalId,
    Guid? AdopterId,
    string? FirstName,
    string? LastName,
    string? Address,
    string? Email,
    string? Phone,
    DateTime? AdoptionDate) : IRequest<Adoption>;

public class CreateAdoptionCommandValidator : AbstractValidator<CreateAdoptionCommand>
{
    public CreateAdoptionCommandValidator(IAnimalRepository animalRepository, IUserRepository userRepository)
    {
        RuleFor(x => x.AnimalId)
            .EntityExists(animalRepository);

        When(x => x.AdopterId != Guid.Empty && x.AdopterId != null, () =>
        {
            RuleFor(x => x.AdopterId!.Value)
                .EntityExists(userRepository);
            RuleFor(x => x.FirstName).Null();
            RuleFor(x => x.LastName).Null();
            RuleFor(x => x.Address).Null();
            RuleFor(x => x.Email).Null();
            RuleFor(x => x.Phone).Null();
        }).Otherwise(() =>
        {
            RuleFor(x => x.AdopterId).Null();
            RuleFor(x => x.FirstName).NotNull().MinimumLength(3);
            RuleFor(x => x.LastName).NotNull().MinimumLength(3);
            RuleFor(x => x.Address).NotNull().MinimumLength(3);
            RuleFor(x => x.Email).NotNull().MinimumLength(3).EmailAddress();
            RuleFor(x => x.Phone).NotNull().MinimumLength(9);
        });

        When(x => x.AdoptionDate != null, () =>
        {
            RuleFor(x => x.AdoptionDate)
                .Must(x => x!.Value.ToOADate() >= DateTime.Today.ToOADate())
                .WithMessage("Cannot be adopted backward.");
        });
    }
}

internal class CreateAdoptionCommandHandler(IMapper mapper, IAdoptionRepository adoptionRepository)
    : IRequestHandler<CreateAdoptionCommand, Adoption>
{
    public Task<Adoption> Handle(CreateAdoptionCommand request, CancellationToken cancellationToken)
    {
        var entry = mapper.Map<Adoption>(request);

        entry.AdoptionStatus = AdoptionStatus.Created;

        adoptionRepository.Add(entry);

        return Task.FromResult(entry);
    }
} 