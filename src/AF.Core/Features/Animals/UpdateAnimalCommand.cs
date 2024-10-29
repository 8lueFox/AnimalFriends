using AF.Core.Database.Entities;
using AF.Core.Database.Repositories;
using AF.Core.Extensions;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AF.Core.Features.Animals;

public record UpdateAnimalCommand(
    Guid Id,
    Guid ShelterId,
    string Name,
    Gender Gender,
    string Species,
    string Breed,
    DateTime ArrivalDate,
    string Age,
    string HealthStatus,
    bool VaccinationStatus) : IRequest<Animal>;

internal class UpdateAnimalCommandValidator : AbstractValidator<UpdateAnimalCommand>
{
    public UpdateAnimalCommandValidator(IShelterRepository shelterRepository, IAnimalRepository animalRepository)
    {
        RuleFor(x => x.Id)
            .EntityExists(shelterRepository);

        RuleFor(x => x.ShelterId)
            .EntityExists(shelterRepository);

        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(3)
            .IsUnique(animalRepository);

        RuleFor(x => x.Gender)
            .IsInEnum();

        RuleFor(x => x.Species)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(x => x.Breed)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(x => x.ArrivalDate)
            .NotEmpty();

        RuleFor(x => x.Age)
            .NotEmpty();

        RuleFor(x => x.HealthStatus)
            .NotEmpty();
    }
}

internal class UpdateAnimalCommandHandler(IMapperBase mapper, IAnimalRepository animalRepository)
    : IRequestHandler<UpdateAnimalCommand, Animal>
{
    public Task<Animal> Handle(UpdateAnimalCommand request, CancellationToken cancellationToken)
    {
        var entry = mapper.Map<Animal>(request);

        animalRepository.Update(entry);

        return Task.FromResult(entry);
    }
}