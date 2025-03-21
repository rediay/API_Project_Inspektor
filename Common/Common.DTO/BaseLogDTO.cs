using Common.Entities;

namespace Common.DTO
{
    public class BaseLogDTO : BaseDTO
    {
        public int UserId { get; set; }
        public string Action { get; set; }
        public string ModelName { get; set; }
        public int ModelId { get; set; }   
        public User User { get; set; }
    }
}