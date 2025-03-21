using Common.DTO.Relations_Countrys;

namespace Common.DTO
{
    public class PlanDTO : BaseDTO
	{
		public string Name { get; set; }
		public bool Type { get; set; }
		public int QtyQueries { get; set; }
		public float Price { get; set; }
		public bool Status { get; set; }		
		public int CountryId { get; set; }
		public CountryDTO Country { get; set; }	
		public int UserId { get; set; }
	}
}
