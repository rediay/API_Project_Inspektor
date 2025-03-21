using System.Collections.Generic;

namespace Common.DTO.ThirdPartyProfiling
{
    public class ThirdPartyProfilingDTO
    {
        public string Name { get; set; }
        public string DocumentType { get; set; }
        public string Document { get; set; }
        public string PersonType { get; set; }
        public List<int> CategoriesIds { get; set; }
    }
}