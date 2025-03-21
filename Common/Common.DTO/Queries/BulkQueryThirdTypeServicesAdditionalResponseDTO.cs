using Common.Entities.SPsData.AditionalServices;
using Common.Entities.SPsData.AditionalServices.Procuraduria;
using Common.Entities.SPsData.AditionalServices.RamaJudicial;
using System.Collections.Generic;

namespace Common.DTO.Queries
{
    public class BulkQueryThirdTypeServicesAdditionalResponseDTO
    {
        public cXsHttpResponse<Procuraduria> Procuraduria { get; set; }
        public cXsHttpResponse<IEnumerable<RamaJudicial>> RamaJudicial { get; set; }
        public cXsHttpResponse<IEnumerable<RamaJudicialJEMPS>> RamaJudicialJEMPS { get; set; }
        public string? IdentificationType { get; set; }
        public string? Document { get; set; }
        public string? Name { get; set; }
        public string? CheckDigit { get; set; }
    }
}
