using Newtonsoft.Json;
using System.Collections.Generic;

namespace Common.Entities.SPsData.AditionalServices.Procuraduria
{
    public class SuperSocietiesData
    {
        public string ActividadCIIU { get; set; }

        public string VersionCIIU { get; set; }

        public string DescripcionCIIU { get; set; }

        public string DireccionJudicial { get; set; }

        public string CiudadJudicial { get; set; }

        public string DepartamentoJudicial { get; set; }

        public string DireccionDomicilio { get; set; }

        public string CiudadDomicilio { get; set; }

        public string DepartamentoDomicilio { get; set; }

        public string CorreoElectronico { get; set; }

        public string Nit { get; set; }

        public string RazonSocial { get; set; }

        public string Expediente { get; set; }

        public string Sigla { get; set; }

        public string ObjetoSocial { get; set; }

        public string TipoSociedad { get; set; }

        public string Estado { get; set; }

        public string FechaEstado { get; set; }

        public string EtapaSituacion { get; set; }

        public string FechaSituacion { get; set; }

        public string FechaEtapa { get; set; }

        public string Causal { get; set; }

        public List<string> JuntaDirectivaPrincipal { get; set; }

        public List<string> JuntaDirectivaSuplente { get; set; }

        public string Contador { get; set; }

        public string RevisorFiscal { get; set; }

        public string RepresentanteLegal { get; set; }

        public string RepresentanteLegalPrimerSuplente { get; set; }

    }
}
