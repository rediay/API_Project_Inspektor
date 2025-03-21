using AutoMapper;
using Common.DTO.Queries;
using Common.DTO.RestrictiveLists;
using Common.Entities;
using Common.Entities.SPsData;

namespace Common.Services.Infrastructure.MappingProfiles.Queries
{
    public class ListBulkQueryProfile : Profile
    {
        public ListBulkQueryProfile()
        {
            CreateMap< ListResponse, ListsBulkQueryDTO>().ReverseMap();
            CreateMap<OwnListResponse, OwnListBulkQueryResponseDTO>().ReverseMap();

        }
    }
}