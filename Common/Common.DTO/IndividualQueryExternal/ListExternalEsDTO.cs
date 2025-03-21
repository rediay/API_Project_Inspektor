using System;

namespace Common.DTO.IndividualQueryExternal
{
    public class ListExternalEsDTO
    {
        public string NombreCompleto { get; set; }
        public string Identificacion { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string FuenteConsulta { get; set; }
        public string PersonaAmanable { get; set; }
        public string Alias { get; set; }
        public string AliasDebil { get; set; }
        public string Delito { get; set; }
        public string Peps { get; set; }
        public string Zona { get; set; }
        public string Link { get; set; }
        public string OtraInformacion { get; set; }
        public bool Estado { get; set; }
        public string Resumen { get; set; }
        public string Actos { get; set; }
        public string Entidad { get; set; }
        public int IdGrupoLista { get; set; }
        public string TipoPersona { get; set; }
        public string TipoDocumento { get; set; }
        public string NombreTipoLista { get; set; }
        public string NombreGrupoLista { get; set; }
        public int Prioridad { get; set; }
        public int Orden { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public int IdTipoLista { get; set; }
        public int IdTipoPersona { get; set; }
        public int IdTipoDocumento { get; set; }
    }
}
