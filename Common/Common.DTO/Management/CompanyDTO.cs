namespace Common.DTO
{
	public class CompanyDTO
	{
		public int? Id { get; set; }
		public string? Name { get; set; }
		public string? Identification { get; set; }
		public bool? Status { get; set; }
		public bool? AutoRenewal { get; set; }
		public string? Image { get; set; }
		public string? ContractDate { get; set; }
		public int PlanId { get; set; }	
	}
}
