using AutoMapper;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.MappingProfiles
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<NombreCedula, NombreCedulaDTO>().ReverseMap();
        }
    }
}