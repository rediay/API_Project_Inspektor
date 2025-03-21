using AutoMapper;
using Common.DTO.IndividualQueryExternal;
using Common.DTO.RestrictiveLists;

namespace Common.Services.Infrastructure.MappingProfiles.IndividualQueryExternal
{
    public class ListExternalEsProfile : Profile
    {
        public ListExternalEsProfile() : base()
        {
            CreateMap<ListExternalEsDTO, ListExternalDTO>().ReverseMap()
                .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Identificacion, opt => opt.MapFrom(src => src.Identification))
                .ForMember(dest => dest.DocumentoIdentidad, opt => opt.MapFrom(src => src.Document))
                .ForMember(dest => dest.FuenteConsulta, opt => opt.MapFrom(src => src.Source))
                .ForMember(dest => dest.PersonaAmanable, opt => opt.MapFrom(src => src.KindPerson))
                .ForMember(dest => dest.Alias, opt => opt.MapFrom(src => src.Alias))
                .ForMember(dest => dest.AliasDebil, opt => opt.MapFrom(src => src.WeakAlias))
                .ForMember(dest => dest.Delito, opt => opt.MapFrom(src => src.Crime))
                .ForMember(dest => dest.Peps, opt => opt.MapFrom(src => src.Peps))
                .ForMember(dest => dest.Zona, opt => opt.MapFrom(src => src.Zone))
                .ForMember(dest => dest.Link, opt => opt.MapFrom(src => src.Link))
                .ForMember(dest => dest.OtraInformacion, opt => opt.MapFrom(src => src.MoreInformation))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Resumen, opt => opt.MapFrom(src => src.Summary))
                .ForMember(dest => dest.Actos, opt => opt.MapFrom(src => src.Acts))
                .ForMember(dest => dest.Entidad, opt => opt.MapFrom(src => src.Entity))
                .ForMember(dest => dest.IdGrupoLista, opt => opt.MapFrom(src => src.ListGroupId))
                .ForMember(dest => dest.TipoPersona, opt => opt.MapFrom(src => src.NamePersonTypes))
                .ForMember(dest => dest.TipoDocumento, opt => opt.MapFrom(src => src.NameTypeDocument))
                .ForMember(dest => dest.NombreTipoLista, opt => opt.MapFrom(src => src.NameListType))
                .ForMember(dest => dest.NombreGrupoLista, opt => opt.MapFrom(src => src.NameListGroup))
                .ForMember(dest => dest.Prioridad, opt => opt.MapFrom(src => src.PriorityResult))
                .ForMember(dest => dest.Orden, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.FechaInicio, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.FechaFin, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.FechaActualizacion, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.IdTipoLista, opt => opt.MapFrom(src => src.ListTypeId))
                .ForMember(dest => dest.IdTipoPersona, opt => opt.MapFrom(src => src.PersonTypeId))
                .ForMember(dest => dest.IdTipoDocumento, opt => opt.MapFrom(src => src.DocumentTypeId));
        }
    }
}
