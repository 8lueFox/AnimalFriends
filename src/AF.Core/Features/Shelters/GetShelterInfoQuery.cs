using AF.Core.Database.Repositories;
using AutoMapper;
using MediatR;

namespace AF.Core.Features.Shelters;

public record GetShelterInfoQuery(Guid ShelterId) : IRequest<ShelterDto>;

internal class GetShelterInfoQueryHandler(IShelterRepository shelterRepository, IMapper mapper) : IRequestHandler<GetShelterInfoQuery, ShelterDto>
{
    public async Task<ShelterDto> Handle(GetShelterInfoQuery request, CancellationToken cancellationToken)
    {
        var shelter = shelterRepository.GetById(request.ShelterId, "Animals", "Users", "Users.User");
        
        return mapper.Map<ShelterDto>(shelter);
    }
}