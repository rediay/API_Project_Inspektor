using Common.Entities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Common.DTO
{
    public class QueryDetailAndQueryDTO
    {        
        public Query Query { get; set; }
        public List<QueryDetail> QueryDetail { get; set; }
    }
}