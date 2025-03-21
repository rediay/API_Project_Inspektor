using AutoMapper;
using Common.DTO;
using Common.DTO.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.MappingProfiles.Queries
{
    public class BulkQueryListExcelCoincidence : Profile
    {
        public BulkQueryListExcelCoincidence() : base()
        {
            CreateMap<BulkQueryListExcelCoincidenceDTO, QueryDetailDTO>().ReverseMap()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Identificacion, opt => opt.MapFrom(src => src.Identification))
                .ForMember(dest => dest.Cantidad_coincidencias, opt => opt.MapFrom(src => src.ResultQuantity));
        }
    }
}
