using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities.SPsData.AditionalServices.EPS
{
    public class Eps
    {
        public string Entity { get; set; }
        public string Regime { get; set; }
        public string EffectiveDate { get; set; }
        public string EndDate { get; set; }
        public string AffiliateType { get; set; }
        public string State { get; set; }
    }
}
