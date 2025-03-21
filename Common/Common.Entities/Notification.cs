namespace Common.Entities

{
    public class Notification : BaseEntity
    {
        public string Subject { get; set; }
        public string To { get; set; }
        public string Detail { get; set; }
        public bool Status { get; set; }
        public string json { get; set; }
        public int CompanyId { get; set; }
        public int? UserId { get; set; }
        public int? NotificationTypeId { get; set; }

        public virtual Company Company { get; set; }
        public virtual User User { get; set; }
        public virtual NotificationType NotificationType { get; set; }

    }
}
