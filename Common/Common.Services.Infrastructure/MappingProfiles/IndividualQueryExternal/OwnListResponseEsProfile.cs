using AutoMapper;
using Common.DTO.IndividualQueryExternal;
using Common.DTO.OwnLists;

namespace Common.Services.Infrastructure.MappingProfiles.IndividualQueryExternal
{
    public class OwnListResponseEsProfile : Profile
    {
        public OwnListResponseEsProfile() : base()
        {
            CreateMap<OwnListResponseEsDTO, OwnListResponseDTO>().ReverseMap()
                .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DocumentoIdentidad, opt => opt.MapFrom(src => src.Identification))
                .ForMember(dest => dest.TipoIdentificacion, opt => opt.MapFrom(src => src.TypeIdentification))
                .ForMember(dest => dest.TipoDocumento, opt => opt.MapFrom(src => src.DocumentType))
                .ForMember(dest => dest.FuenteConsulta, opt => opt.MapFrom(src => src.Source))
                .ForMember(dest => dest.Alias, opt => opt.MapFrom(src => src.Alias))
                .ForMember(dest => dest.Delito, opt => opt.MapFrom(src => src.Crime))
                .ForMember(dest => dest.Link, opt => opt.MapFrom(src => src.Link))
                .ForMember(dest => dest.OtraInformacion, opt => opt.MapFrom(src => src.MoreInformation))
                .ForMember(dest => dest.Zona, opt => opt.MapFrom(src => src.Zone))
                .ForMember(dest => dest.FechaRegistro, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.FechaActualizacion, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.NombreTipoLista, opt => opt.MapFrom(src => src.OwnlistTypeName));
        }
    }
}
