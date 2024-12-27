using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AF.Core.Database.Repositories;
using AF.Core.Settings;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AF.Core.Features.Users;

public record SignInCommand(string EmailOrUserName, string HashedPassword) : IRequest<string>;

public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(x => x.EmailOrUserName).NotEmpty();
        RuleFor(x => x.HashedPassword).NotEmpty();
    }
}

internal sealed class SignInCommandHandler(IUserRepository userRepository, IOptions<JwtConfiguration> jwtConfig) : IRequestHandler<SignInCommand, string>
{
    public async Task<string> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = userRepository.Items.FirstOrDefault(x => x.Email == request.EmailOrUserName || x.UserName == request.HashedPassword);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(jwtConfig.Value.Key);
        
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, request.EmailOrUserName),
            new(JwtRegisteredClaimNames.Email, request.EmailOrUserName),
            new("userid", user!.Id.ToString())
        };
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(TimeSpan.FromHours(1)),
            Issuer = jwtConfig.Value.Issuer,
            Audience = jwtConfig.Value.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}