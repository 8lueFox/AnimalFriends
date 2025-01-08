using AF.Core.Database.Entities;
using AutoMapper;

namespace AF.Core.Features.Shelters;

public class ShelterProfile : Profile
{
    public ShelterProfile()
    {
        CreateMap<CreateShelterCommand, Shelter>();
        CreateMap<UpdateShelterCommand, Shelter>();
        
        CreateMap<Shelter, ShelterDto>()
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Users.FirstOrDefault(x => x.IsOwner == true).User.UserName)) 
            .ForMember(dest => dest.CountOfAnimals, opt => opt.MapFrom(src => src.Animals.Count));
    }
}