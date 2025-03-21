namespace Common.Entities
{
    public class QueryDetail : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Identification { get; set; }
        public int ResultQuantity { get; set; }
        public int QueryId { get; set; }
        public virtual Query Query { get; set; }
    }
}