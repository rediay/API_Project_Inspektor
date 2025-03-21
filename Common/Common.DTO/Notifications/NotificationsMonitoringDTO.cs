using Newtonsoft.Json.Linq;

namespace Common.DTO
{
    public class NotificationsMonitoringDTO : NotificationSentDTO
    {

        public string QueryNumber { get { return JObject.Parse(json).Property("QueryNumber").Value.ToString(); } }
        public string QueryName { get { return JObject.Parse(json).Property("QueryName").Value.ToString(); } }
        public string IdentificationNumber { get { return JObject.Parse(json).Property("IdentificationNumber").Value.ToString(); } }
        public string QueryDate { get { return JObject.Parse(json).Property("QueryDate").Value.ToString(); } }        
        public string QueryUser { get { return JObject.Parse(json).Property("QueryUser").Value.ToString(); } }
        public string Status { get { return JObject.Parse(json).Property("Status").Value.ToString(); } }
        public string Justification { get { return JObject.Parse(json).Property("Justification").Value.ToString(); } }
        public string TypeList { get { return JObject.Parse(json).Property("TypeList").Value.ToString(); } }
        public string ListName { get { return JObject.Parse(json).Property("ListName").Value.ToString(); } }
        public string ListDocument { get { return JObject.Parse(json).Property("ListDocument").Value.ToString(); } }




    }
}