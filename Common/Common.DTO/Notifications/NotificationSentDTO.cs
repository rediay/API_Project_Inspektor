using Common.DTO;


namespace Common.DTO
{
    public class NotificationSentDTO
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        public string Detail { get; set; }
        public string CreatedAt { get; set; }
        public string UserName { get; set; }
        public string json { get; set; }
        public string User { get; set; }
    }
}
