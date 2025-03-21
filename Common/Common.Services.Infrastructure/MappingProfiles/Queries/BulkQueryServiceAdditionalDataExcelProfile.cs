using AutoMapper;
using Common.DTO.Queries;
using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.MappingProfiles.Queries
{
    public class BulkQueryServiceAdditionalDataExcelProfile : Profile
    {
        public BulkQueryServiceAdditionalDataExcelProfile() : base()
        {
            CreateMap<BulkQueryServiceAdditionalDataExcelDTO, ListDataExcel>().ReverseMap()
                .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.Document))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Procuraduria, opt => opt.MapFrom(src => src.Attorney))
                .ForMember(dest => dest.Rama_judicial, opt => opt.MapFrom(src => src.JudicialBranch))
                .ForMember(dest => dest.Rama_judicial_JEPMS, opt => opt.MapFrom(src => src.JEPMSJudicialBranch));
        }
    }
}
