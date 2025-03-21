namespace Common.Entities
{
    public class ListGroup : BaseEntity
    {
        public string Name { get; set; }
        /*public int Priority { get; set; }*/
        public int Order { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
    }
}