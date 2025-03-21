using System.Collections.Generic;

namespace Common.DTO.IndividualQueryExternal
{
    public class IndividualQueryExternalResponseEsDTO
    {
        public string Nombre { get; set; }
        public string NumDocumento { get; set; }
        public int TipoDocumento { get; set; }
        public int CantCoincidencias { get; set; } = 0;
        public int NumeroPalabras { get; set; }
        public bool Prioridad4 { get; set; }
        public int NumConsulta { get; set; }
        public IEnumerable<ListExternalEsDTO> Listas { get; set; }
        public IEnumerable<OwnListResponseEsDTO> Listas_Propias { get; set; }
    }
}
