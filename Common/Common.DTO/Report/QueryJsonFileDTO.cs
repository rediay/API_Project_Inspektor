using System.Collections.Generic;
using Common.DTO.Queries;
using Common.DTO.RestrictiveLists;

namespace Common.DTO
{
    public class QueryJsonFileDTO
    {
        public QueryDTO Query { get; set; }
        public List<ListDTO> Lists { get; set; }
        public List<QueryOwnListDTO> ownLists { get; set; }
    }
}