using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.Queries
{
    public class BulkQueryServicesAdditionalRequestDTO
    {

        public int? Id { get; set; }
        public IFormFile File { get; set; }
        public DataTable dataTableBulkQuery { get; set; }
        public DataSet dataSetPriorities { get; set; }
        public int CompanyId { get; set; }
        public Boolean hasProcuraduria { get; set; }
        public Boolean hasRamaJudicial { get; set; }
        public Boolean hasRamaJudicialJEMPS { get; set; }
    }
}
