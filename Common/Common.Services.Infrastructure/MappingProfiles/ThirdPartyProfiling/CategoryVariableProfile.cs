using AutoMapper;
using Common.DTO.ThirdPartyProfiling;
using Common.Entities;

namespace Common.Services.Infrastructure.MappingProfiles.ThirdPartyProfiling
{
    public class CategoryVariableProfile : Profile
    {
        public CategoryVariableProfile()
        {
            CreateMap<CategoryVariable, CategoryVariableDTO>().ReverseMap();
        }
    }
}