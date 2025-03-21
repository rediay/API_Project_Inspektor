using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.Lists
{
    public  class ListPaginationFilterThirdDTO : PaginationFilterDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string DocumentTypeId { get; set; }
        public bool Validated { get; set; }
    }
}
