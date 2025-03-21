using System;
using System.Collections.Generic;

namespace Common.Entities
{
    public class ThirdList : BaseEntity
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public int DocumentTypeId { get; set; }
        public bool Validated { get; set; }
        public string TempData { get; set; }
        public virtual DocumentType DocumentType { get; set; }


    }
}
