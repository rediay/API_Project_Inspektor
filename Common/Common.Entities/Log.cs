namespace Common.Entities
{
    public class Log : BaseEntity
    {
        public int UserId { get; set; }
        public string Json { get; set; }
        public string Action { get; set; }
        public string ModelName{ get; set; }
        public int ModelId{ get; set; }
        //public int LogTypeId { get; set; }
        
        public virtual User User { get; set; }
        //public virtual LogType LogType { get; set; }
    }
}