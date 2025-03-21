using System;

namespace Common.DTO
{
    public class EventRoiDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal EstimatedAmount { get; set; }
        public string Observations { get; set; }
        public string Identification { get; set; }
        public int OperationTypeId { get; set; }
        public int OperationStatusId { get; set; }
        public string OperationType { get; set; }
        public string OperationStatus { get; set; }
    }
}