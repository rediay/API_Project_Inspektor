using System;

namespace Common.Entities
{
    public class EventRoi : BaseEntity
    {
        public string Title { get; set; }
        public DateTime TransactionDate { get; set; }
        public float EstimatedAmount { get; set; }
        public string Observations { get; set; }
        public string Identification { get; set; }
        public int OperationTypeId { get; set; }
        public int OperationStatusId { get; set; }
        public virtual User User { get; set; }
        public virtual EventRoiOperationType OperationType { get; set; }
        public virtual EventRoiOperationStatus OperationStatus { get; set; }
    }
}