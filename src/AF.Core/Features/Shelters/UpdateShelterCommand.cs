using AF.Core.Database.Entities;
using AF.Core.Database.Repositories;
using AF.Core.Extensions;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AF.Core.Features.Shelters;

public record UpdateShelterCommand(Guid Id, string Name, string Address, string Phone, string Email, string? BankAccount)
    : IRequest<Shelter>;

public class UpdateShelterCommandValidator : AbstractValidator<UpdateShelterCommand>
{
    public UpdateShelterCommandValidator(IShelterRepository shelterRepository)
    {
        RuleFor(x => x.Id)
            .EntityExists(shelterRepository);
        
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

internal class UpdateShelterCommandHandler(IMapper mapper, IShelterRepository shelterRepository)
    : IRequestHandler<UpdateShelterCommand, Shelter>
{
    public Task<Shelter> Handle(UpdateShelterCommand request, CancellationToken cancellationToken)
    {
        var entry = mapper.Map<Shelter>(request);

        shelterRepository.Update(entry);

        return Task.FromResult(entry);
    }
}