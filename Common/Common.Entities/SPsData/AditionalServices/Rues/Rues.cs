using Newtonsoft.Json;
using System.Collections.Generic;

namespace Common.Entities.SPsData.AditionalServices.Procuraduria
{
    public class Rues
    {
        public string RazonSocialONombre { get; set; }

        public string Nit { get; set; }

        public string Estado { get; set; }

        public string municipio { get; set; }

        public string Categoria { get; set; }

    }

    public class RuesPostObject
    {
        public string txtNIT { get; set; }

        public string __RequestVerificationToken { get; set; }
        
    }
}
