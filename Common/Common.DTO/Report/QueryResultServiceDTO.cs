using Common.DTO.Queries;
using Common.Entities;
using Common.Entities.SPsData;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Common.DTO
{
    public class QueryResultServiceDTO
    {        
        public IEnumerable<ListsBulkQueryDTO> list { get; set; }
        public Dictionary<int, int> DictionaryQuantityUserPerList { get; set; }
    }
}