using AutoMapper;
using Common.DTO.RestrictiveLists;
using Common.Entities;

namespace Common.Services.Infrastructure.MappingProfiles.RestrictiveLists
{
    public class ListGroupProfile : Profile
    {
        public ListGroupProfile()
        {
            CreateMap<ListGroup, ListGroupDTO>().ReverseMap();
        }
    }
}