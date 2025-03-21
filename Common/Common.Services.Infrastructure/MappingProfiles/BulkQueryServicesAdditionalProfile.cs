using AutoMapper;
using Common.DTO.Queries;
using Common.Entities.BulkQuery;

namespace Common.Services.Infrastructure.MappingProfiles
{
    public class BulkQueryServicesAdditionalProfile : Profile
    {
        public BulkQueryServicesAdditionalProfile()
        {
            CreateMap<BulkQueryServicesAdditional, BulkQueryServicesAdditionalDTO>().ReverseMap();
        }
    }
}
