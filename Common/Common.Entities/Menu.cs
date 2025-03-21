namespace Common.Entities
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; }
        public bool Blank { get; set; }
        public string Path { get; set; }
    }
}