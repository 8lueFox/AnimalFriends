using System.Text;
using System.Text.RegularExpressions;
using AF.Core.Database.Entities;
using AF.Core.Database.Repositories;
using AF.Core.Extensions;
using AF.Core.Services.Interfaces;
using AutoMapper;
using FluentValidation;
using LinqToDB;
using MediatR;

namespace AF.Core.Features.Users;

public record UpdateUserCommand(
    string UserName,
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string Address,
    string Gender,
    DateOnly Birthday) : IRequest;
    
    
public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.UserName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(3);
        
        RuleFor(x => x.FirstName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(x => x.LastName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(5)
            .EmailAddress()
            .MustAsync((email, token) => userRepository.Items.AllAsync(x => x.Email != email, token))
            .WithMessage("User with passed email exists.");

        RuleFor(x => x.Phone)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(9);

        RuleFor(x => x.Gender)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(x => x.Address)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(x => x.Birthday)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .GreaterThan(DateOnly.FromDateTime(DateTime.Now.AddYears(100)));
    }
}

internal class UpdateUserCommandHandler(
    IMapper mapper,
    IUserRepository userRepository,
    RequestContext requestContext)
    : IRequestHandler<UpdateUserCommand>
{
    public Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entry = mapper.Map<User>(request);
        entry.Id = (Guid)requestContext.UserId;
        
        userRepository.Update(entry);

        return Task.FromResult(entry);
    }
}