using System;
using System.Collections.Generic;
using Common.DTO.Lists;
using Common.Entities;

namespace Common.DTO.RestrictiveLists
{
    public class ListDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Identification { get; set; }
        public string Document { get; set; }
        public string Source { get; set; }
        public string KindPerson { get; set; }
        public string Alias { get; set; }
        public string WeakAlias { get; set; }
        public string Crime { get; set; }
        public string Peps { get; set; }
        public string Zone { get; set; }
        public string Link { get; set; }
        public string MoreInformation { get; set; }
        public bool Status { get; set; }
        public string Summary { get; set; }
        public string Acts { get; set; }
        public string Entity { get; set; }
        public bool Activated { get; set; }
        public bool Validated { get; set; }
        public string urlImage { get; set; }
        public int ListGroupId { get; set; }
        public string NamePersonTypes { get; set; }
        public string NameTypeDocument { get; set; }
        public string NameListType { get; set; }
        public string NameListGroup { get; set; }
        public int PriorityResult { get; set; }
        public int Priority { get; set; }
        public int Order { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ListDTO TempData { get; set; }

        public int ListTypeId { get; set; }
        public int PersonTypeId { get; set; }
        public int DocumentTypeId { get; set; }
        public int UserId { get; set; }
        public int ThirdListId { get; set; }
        public string ThirdListDocument { get; set; }
        public string ThirdListName { get; set; }

        public int CountryId { get; set; }

        public ListType ListType { get; set; }
        public PersonType PersonType { get; set; }
        public DocumentType DocumentType { get; set; }
        public ThirdList ThirdList { get; set; }
        public List<ListTypeDTO> ListTypes { get; set; }
        public List<PersonType> PersonTypes { get; set; }
        public List<DocumentType> DocumentTypes { get; set; }
        public List<ParameterDTO> Users { get; set; }
        public List<ThirdListsDTO> ThirdLists { get; set; }
    }
}
