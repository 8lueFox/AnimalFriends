using AF.Core.Database.Entities;
using AF.Core.Database.Repositories;
using AF.Core.Extensions;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AF.Core.Features.Animals;

//TODO: Fetch ShelterId from session
public record CreateAnimalCommand(
    Guid ShelterId,
    string Name,
    Gender Gender,
    string Species,
    string Breed,
    DateTime ArrivalDate,
    string Age,
    string HealthStatus,
    bool VaccinationStatus) : IRequest<Animal>;

internal class CreateAnimalCommandValidator : AbstractValidator<CreateAnimalCommand>
{
    public CreateAnimalCommandValidator(IShelterRepository shelterRepository, IAnimalRepository animalRepository)
    {
        RuleFor(x => x.ShelterId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
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

internal class CreateAnimalCommandHandler(IMapperBase mapper, IAnimalRepository animalRepository)
    : IRequestHandler<CreateAnimalCommand, Animal>
{
    public Task<Animal> Handle(CreateAnimalCommand request, CancellationToken cancellationToken)
    {
        var entry = mapper.Map<Animal>(request);

        animalRepository.Add(entry);

        return Task.FromResult(entry);
    }
}