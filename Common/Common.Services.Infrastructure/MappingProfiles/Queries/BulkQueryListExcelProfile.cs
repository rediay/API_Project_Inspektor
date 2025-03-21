using AutoMapper;
using Common.DTO.Queries;

namespace Common.Services.Infrastructure.MappingProfiles.Queries
{
    public class BulkQueryListExcelProfile : Profile
    {
        public BulkQueryListExcelProfile() : base()
        {
            CreateMap<BulkQueryListExcelDTO, ListsBulkQueryDTO>().ReverseMap()
                .ForMember(dest => dest.Activado, opt => opt.MapFrom(src => src.Activated))
                .ForMember(dest => dest.Hechos, opt => opt.MapFrom(src => src.Acts))
                .ForMember(dest => dest.Alias, opt => opt.MapFrom(src => src.Alias))
                .ForMember(dest => dest.Creado_en, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Delito, opt => opt.MapFrom(src => src.Crime))
                .ForMember(dest => dest.Eliminado_en, opt => opt.MapFrom(src => src.DeletedAt))
                .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.Document))
                .ForMember(dest => dest.Tipo_Documento, opt => opt.MapFrom(src => src.DocumentTypeId))
                .ForMember(dest => dest.Fecha_finalización, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Entidad, opt => opt.MapFrom(src => src.Entity))
                .ForMember(dest => dest.Identificacion, opt => opt.MapFrom(src => src.Identification))
                .ForMember(dest => dest.Consulta_identificacion, opt => opt.MapFrom(src => src.IdentificationQuery))
                .ForMember(dest => dest.Persona_amable, opt => opt.MapFrom(src => src.KindPerson))
                .ForMember(dest => dest.Enlace, opt => opt.MapFrom(src => src.Link))
                .ForMember(dest => dest.Tipo_grupo_lista, opt => opt.MapFrom(src => src.ListGroupId))
                .ForMember(dest => dest.Identificacion_tipo_lista, opt => opt.MapFrom(src => src.ListTypeId))
                .ForMember(dest => dest.Mas_informacion, opt => opt.MapFrom(src => src.MoreInformation))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Nombre_grupo_lista, opt => opt.MapFrom(src => src.NameListGroup))
                .ForMember(dest => dest.Nombre_tipo_lista, opt => opt.MapFrom(src => src.NameListType))
                .ForMember(dest => dest.Nombre_consulta, opt => opt.MapFrom(src => src.NameQuery))
                .ForMember(dest => dest.Nombre_tipo_documento, opt => opt.MapFrom(src => src.NameTypeDocument))
                .ForMember(dest => dest.Orden, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.Peps, opt => opt.MapFrom(src => src.Peps))
                .ForMember(dest => dest.Tipo_persona_ID, opt => opt.MapFrom(src => src.PersonTypeId))
                .ForMember(dest => dest.Resultado_prioridad, opt => opt.MapFrom(src => src.PriorityResult))
                .ForMember(dest => dest.Fuente, opt => opt.MapFrom(src => src.Source))
                .ForMember(dest => dest.Fecha_inicio, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Resumen, opt => opt.MapFrom(src => src.Summary))
                .ForMember(dest => dest.Actualizado_en, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.ID_usuario, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Validado, opt => opt.MapFrom(src => src.Validated))
                .ForMember(dest => dest.Alias_debil, opt => opt.MapFrom(src => src.WeakAlias))
                .ForMember(dest => dest.Zona, opt => opt.MapFrom(src => src.Zone));
        }
    }
}
