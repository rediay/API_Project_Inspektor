using System.Collections.Generic;

namespace Common.DTO.Queries
{
    public class BulkQueryServicesAdditionalResponseDTO
    {
        public BulkQueryServicesAdditionalDTO QueryServiceAdditional { get; set; }
        public List<BulkQueryThirdTypeServicesAdditionalResponseDTO> DataThirdType { get; set; }
        public List<ListDataExcel> ListDataExcels { get; set; }
        public string Image { get; set; }
    }
}
