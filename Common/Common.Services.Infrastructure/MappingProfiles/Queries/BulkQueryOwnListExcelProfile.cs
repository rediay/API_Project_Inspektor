using AutoMapper;
using Common.DTO.Queries;

namespace Common.Services.Infrastructure.MappingProfiles.Queries
{
    public class BulkQueryOwnListExcelProfile : Profile
    {
        public BulkQueryOwnListExcelProfile() : base()
        {
            CreateMap<BulkQueryOwnListExcelDTO, OwnListBulkQueryResponseDTO>().ReverseMap()
                .ForMember(dest => dest.Nombre_consulta, opt => opt.MapFrom(src => src.NameQuery))
                .ForMember(dest => dest.Identificacion_consulta, opt => opt.MapFrom(src => src.IdentificationQuery))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Identificacion, opt => opt.MapFrom(src => src.Identification))
                .ForMember(dest => dest.Tipo_identificacion, opt => opt.MapFrom(src => src.TypeIdentification))
                .ForMember(dest => dest.Tipo_documento, opt => opt.MapFrom(src => src.DocumentType))
                .ForMember(dest => dest.Fuente, opt => opt.MapFrom(src => src.Source))
                .ForMember(dest => dest.Alias, opt => opt.MapFrom(src => src.Alias))
                .ForMember(dest => dest.Delito, opt => opt.MapFrom(src => src.Crime))
                .ForMember(dest => dest.Enlace, opt => opt.MapFrom(src => src.Link))
                .ForMember(dest => dest.Mas_informacion, opt => opt.MapFrom(src => src.MoreInformation))
                .ForMember(dest => dest.Zona, opt => opt.MapFrom(src => src.Zone))
                .ForMember(dest => dest.Creado_en, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Actualizado_en, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.Compania_id, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.Usuario_id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Tipo_lista_propia_id, opt => opt.MapFrom(src => src.OwnListTypeId))
                .ForMember(dest => dest.Nombre_tipo_lista_propia, opt => opt.MapFrom(src => src.OwnlistTypeName));
        }
    }
}
