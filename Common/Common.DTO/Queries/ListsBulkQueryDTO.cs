using Common.DTO.RestrictiveLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.Queries
{
    public class ListsBulkQueryDTO: ListDTO
    {
        public string NameQuery { get; set; }
        public string IdentificationQuery { get; set; }
    }
}
