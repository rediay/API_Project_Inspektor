
using Common.Entities;
using System;
using System.Collections.Generic;

namespace Common.DTO.Lists
{
    public class ThirdListsDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public int DocumentTypeId { get; set; }
        public string ?TempData { get; set; }
        public bool Validated { get; set; }
        public string DocumentTypename { get; set; }
    }
}
