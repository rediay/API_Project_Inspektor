using System;
using System.Collections.Generic;
using Common.Entities;
using Common.Entities.Relations_Countrys;

namespace Common.DTO
{
    public class ContentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CountryId { get; set; }
        public int ContentCategoryId { get; set; }
        public int ContentTypeId { get; set; }

        public int ContentStatusId { get; set; }
        public ContenCategoryDTO ContentCategory { get; set; }

        public List<Countries> Countries { get; set; }
        public List<ContentCategory> ContentCategories { get; set; }
        public List<ContentType> ContentTypes { get; set; }
    }
}