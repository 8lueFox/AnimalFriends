using AF.Core.Database.Entities;
using AutoMapper;

namespace AF.Core.Features.Adoptions;

public class AdoptionProfile : Profile
{
    public AdoptionProfile()
    {
        CreateMap<CreateAdoptionCommand, Adoption>();
    }
}