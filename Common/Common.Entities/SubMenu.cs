namespace Common.Entities
{
    public class SubMenu : BaseEntity
    {
        public string Name { get; set; }
        public bool Blank { get; set; }
        public string Path { get; set; }
        
        public virtual Menu Menu { get; set; }
    }
}