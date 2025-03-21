using System;

namespace Common.DTO
{
    public class QueryOwnListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Identification { get; set; }
        public string DocumentType { get; set; }
        public string Source { get; set; }
        public string KindPerson { get; set; }
        public string Alias { get; set; }
        public string WeakAlias { get; set; }
        public string Crime { get; set; }
        public string Peps { get; set; }
        public string Zone { get; set; }
        public string Link { get; set; }
        public string MoreInformation { get; set; }
        public string OwnlistTypeName { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int OwnListTypeId { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}