using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities.SPsData.AditionalServices.RegistryDeaths
{
    public class cInfoDefRegistraduria
    {
        public int codigo { get; set; }

        public string vigencia { get; set; }

        public string fecha { get; set; }

    }

    public class DefRegistraduriaPostObject
    {
        public string nuip { get; set; }
        public string ip { get; set; }


    }
}
