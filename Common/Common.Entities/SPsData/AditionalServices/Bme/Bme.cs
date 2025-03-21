using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities.SPsData.AditionalServices.Bme
{
    public class Bme
    {

        public string bme_text { get; set; }
        public string incumplimiento_acuerdos { get; set; }
        public IEnumerable<BmeData> bme_data { get; set; }
    }

    public class BmeData
    {
        public string name_reported { get; set; }
        public string no_obligation { get; set; }
        public string state { get; set; }
        public string date { get; set; }
    }
}
