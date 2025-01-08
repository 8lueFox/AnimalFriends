using AF.Core.Database.Repositories;
using AutoMapper;
using MediatR;

namespace AF.Core.Features.Users;

public class GetCurrentUserQuery : IRequest<UserDto>;

internal class GetCurrentUserQueryHandler(IUserRepository userRepository,
    RequestContext requestContext,
    IMapper mapper) : IRequestHandler<GetCurrentUserQuery, UserDto>
{
    public Task<UserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var userId = requestContext.UserId;
        if (userId is null)
            return null;
        
        var entry = userRepository.GetById((Guid)userId);
        return Task.FromResult(mapper.Map<UserDto>(entry));
    }
}