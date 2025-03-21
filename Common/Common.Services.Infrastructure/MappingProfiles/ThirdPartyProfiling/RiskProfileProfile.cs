using AutoMapper;
using Common.DTO.ThirdPartyProfiling;
using Common.Entities;

namespace Common.Services.Infrastructure.MappingProfiles.ThirdPartyProfiling
{
    public class RiskProfileProfile : Profile
    {
        public RiskProfileProfile()
        {
            CreateMap<RiskProfile, RiskProfileDTO>().ReverseMap();
        }
    }
}