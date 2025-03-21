using AutoMapper;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.MappingProfiles
{
    public class ContentProfile : Profile
    {
        public ContentProfile()
        {
            CreateMap<Content, ContentDTO>().ReverseMap();
            CreateMap<Content, ContentExcelDTO>().ReverseMap();
                
        }
    }
}