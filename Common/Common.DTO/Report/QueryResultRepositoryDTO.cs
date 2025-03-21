using Common.Entities;
using Common.Entities.SPsData;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Common.DTO
{
    public class QueryResultRepositoryDTO
    {
        public IEnumerable<ListResponse> list { get; set; }
        public Dictionary<int, int> DictionaryQuantityUserPerList { get; set; }
    }
}