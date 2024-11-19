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

public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string Address,
    string Gender,
    DateTime Birthday,
    string Password,
    string PasswordConfirm) : IRequest<User>;

internal class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator(IUserRepository userRepository)
    {
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
            .GreaterThan(DateTime.Now.AddYears(100));

        RuleFor(x => x.Password)
            .Must(IsValidPassword)
            .Equal(x => x.PasswordConfirm);
    }

    private static bool IsValidPassword(string plainText)
    {
        var regex = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
        var match = regex.Match(plainText);
        return match.Success;
    }
}

internal class CreateUserCommandHandler(
    IMapperBase mapper,
    ICryptoProvider cryptoProvider,
    IUserRepository userRepository)
    : IRequestHandler<CreateUserCommand, User>
{
    public Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entry = mapper.Map<User>(request);

        entry.HashedPassword = cryptoProvider.Protect(Encoding.UTF8.GetBytes(request.Password), request.Email);

        userRepository.Add(entry);

        return Task.FromResult(entry);
    }
}