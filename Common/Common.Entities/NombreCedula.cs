using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    [Table("NombreCedula", Schema = "Data")]
    public class NombreCedula 
    {
        [Key]
        public int IdCliente { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string NombreCompleto { get; set; }
        public int TipoIden { get; set; }
        public DateTime FechaReg { get; set; }
        public string Fuente { get; set; }
        public string IdUsuario { get; set; }
       
    }
}