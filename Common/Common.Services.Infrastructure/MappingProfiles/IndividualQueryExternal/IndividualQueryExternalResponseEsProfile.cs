using AutoMapper;
using Common.DTO;
using Common.DTO.IndividualQueryExternal;

namespace Common.Services.Infrastructure.MappingProfiles.IndividualQueryExternal
{
    public class IndividualQueryExternalResponseEsProfile : Profile
    {
        public IndividualQueryExternalResponseEsProfile() : base()
        {
            CreateMap<IndividualQueryExternalResponseEsDTO, IndividualQueryExternalResponseDTO>().ReverseMap()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.NumDocumento, opt => opt.MapFrom(src => src.document))
                .ForMember(dest => dest.CantCoincidencias, opt => opt.MapFrom(src => src.quantityResults))
                .ForMember(dest => dest.NumeroPalabras, opt => opt.MapFrom(src => src.numberWords))
                .ForMember(dest => dest.Prioridad4, opt => opt.MapFrom(src => src.hasPriority4))
                .ForMember(dest => dest.NumConsulta, opt => opt.MapFrom(src => src.query.IdQueryCompany));
        }
    }

}
