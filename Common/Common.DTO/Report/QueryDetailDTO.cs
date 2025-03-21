using System;
using Common.DTO.Queries;

namespace Common.DTO
{
    public class QueryDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Identification { get; set; }
        public int ResultQuantity { get; set; }
        public int QueryId { get; set; }
        public QueryDTO Query { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}