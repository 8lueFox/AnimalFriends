using AF.Core.Database.Entities;
using AutoMapper;

namespace AF.Core.Features.Animals;

public class AnimalProfile : Profile
{
    public AnimalProfile()
    {
        CreateMap<CreateAnimalCommand, Animal>();
        CreateMap<UpdateAnimalCommand, Animal>();
    }
}