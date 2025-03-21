/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using System;

namespace Common.DTO
{
    public class NotificationSettingsDTO
    {
        public int Id { get; set; }
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
        //public CompanyDto Company { get; set; }
        public  UserDTO User { get; set; }
    }
}
