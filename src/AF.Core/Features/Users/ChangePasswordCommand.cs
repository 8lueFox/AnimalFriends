﻿using System.Text;
using AF.Core.Database.Repositories;
using AF.Core.Extensions;
using AF.Core.Services.Interfaces;
using FluentValidation;
using MediatR;

namespace AF.Core.Features.Users;

public record ChangePasswordCommand(string OldPassword, string NewPassword, string ConfirmPassword)
    : IRequest;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator(IUserRepository userRepository,
        ICryptoProvider cryptoProvider, RequestContext requestContext)
    {

        RuleFor(x => x.NewPassword)
            .Must(CreateUserCommandValidator.IsValidPassword)
            .NotEqual(x => x.OldPassword)
            .Equal(x => x.ConfirmPassword);

        RuleFor(x => x)
            .Must(x => IsOldPasswordValid(x, userRepository, cryptoProvider, requestContext))
            .WithMessage("Old password is not valid");
    }

    private static bool IsOldPasswordValid(ChangePasswordCommand cmd, IUserRepository userRepository,
        ICryptoProvider cryptoProvider, RequestContext requestContext)
    {
        var entry = userRepository.GetById((Guid)requestContext.UserId);
        var unprotectedPasswordBytes = cryptoProvider.Unprotect(entry.HashedPassword, entry.Email);
        var unprotectedPassword = Encoding.UTF8.GetChars(unprotectedPasswordBytes).Aggregate(string.Empty, (current, @char) => current + @char);
        return unprotectedPassword.Equals(cmd.OldPassword);
    }
}

internal class ChangePasswordCommandHandler(
    IUserRepository userRepository,
    ICryptoProvider cryptoProvider,
    RequestContext requestContext) : IRequestHandler<ChangePasswordCommand>
{
    public Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var entry = userRepository.GetById((Guid)requestContext.UserId);

        entry.HashedPassword = cryptoProvider.Protect(Encoding.UTF8.GetBytes(request.NewPassword), entry.Email);

        userRepository.SaveChanges();

        return Task.CompletedTask;
    }
}