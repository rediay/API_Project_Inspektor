using AutoMapper;
using Common.DTO.ThirdPartyProfiling;
using Common.Entities;

namespace Common.Services.Infrastructure.MappingProfiles.ThirdPartyProfiling
{
    public class RiskProfileVariableProfile : Profile
    {
        public RiskProfileVariableProfile()
        {
            CreateMap<RiskProfileVariable, RiskProfileVariableDTO>().ReverseMap();
        }
    }
}