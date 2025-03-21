using System;
using System.Collections.Generic;

namespace Common.Entities
{
    public class NotificationSettings : BaseEntity
    {
        public Boolean SendMailPriority1 { get; set; }
        public string MailPriority1 { get; set; }
        public Boolean SendMailPriority2 { get; set; }
        public string MailPriority2 { get; set; }
        public Boolean SendMailPriority3 { get; set; }
        public string MailPriority3 { get; set; }
        public Boolean SendMailPriority4 { get; set; }
        public string MailPriority4 { get; set; }
        public Boolean SendMailCoincidences { get; set; }
        public string MailCoincidences { get; set; }
        public Boolean SendMailAdditionalServices { get; set; }
        public string MailAdditionalServices { get; set; }
        public string MailAministrative { get; set; }
        public Boolean SendMailAdministrative { get; set; }
        public string MailRoi { get; set; }
        public Boolean SendMailRoi { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public virtual User User { get; set; }
      
    }
}