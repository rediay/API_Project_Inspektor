using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities.SPsData.AditionalServices.Simit
{
    public class cInfoSimitNew
    {
        public string type { get; set; }

        public string notification { get; set; }

        public string plate { get; set; }

        public string secretaryship { get; set; }

        public string infringement { get; set; }

        public string state { get; set; }

        public string amount { get; set; }

        public string amountToPaid { get; set; }
    }
    public class SimitPostObject
    {
        public string cedula { get; set; }


    }
}
