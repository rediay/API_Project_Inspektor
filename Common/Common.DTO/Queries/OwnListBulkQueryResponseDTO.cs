using Common.DTO.OwnLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.Queries
{
    public class OwnListBulkQueryResponseDTO: OwnListResponseDTO
    {
        public string NameQuery { get; set; }
        public string IdentificationQuery { get; set; }
    }
}
