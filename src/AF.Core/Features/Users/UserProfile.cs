using AF.Core.Database.Entities;
using AutoMapper;

namespace AF.Core.Features.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserCommand, User>();
        CreateMap<UpdateUserCommand, User>();

        CreateMap<User, UserDto>();
    }
}