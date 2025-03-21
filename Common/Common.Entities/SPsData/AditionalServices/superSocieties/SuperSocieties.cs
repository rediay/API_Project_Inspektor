using Newtonsoft.Json;
using System.Collections.Generic;

namespace Common.Entities.SPsData.AditionalServices.Procuraduria
{
    public class SuperSocieties
    {
        public string ErrorMessage { get; set; }

        public bool HasError { get; set; }

        public SuperSocietiesData data { get; set; }

       

    }
}
