using AF.Core.Database.Entities;
using AF.Core.Database.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AF.Core.Features.Shelters;

public record CreateShelterCommand(string Name, string Address, string Phone, string Email, string BankAccount)
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

        RuleFor(x => x.BankAccount)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(5);
    }
}

internal class CreateShelterCommandHandler(IMapper mapper, IShelterRepository shelterRepository)
    : IRequestHandler<CreateShelterCommand, Shelter>
{
    public Task<Shelter> Handle(CreateShelterCommand request, CancellationToken cancellationToken)
    {
        var entry = mapper.Map<Shelter>(request);

        shelterRepository.Add(entry);
        //TODO: Get UserID from session and assign him to shelter and grant permissions
        return Task.FromResult(entry);
    }
}