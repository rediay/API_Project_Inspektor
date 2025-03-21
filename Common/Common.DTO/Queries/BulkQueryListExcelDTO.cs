using System;

namespace Common.DTO.Queries
{
    public class BulkQueryListExcelDTO
    {
        public string Nombre_consulta { get; set; }
        public string Consulta_identificacion { get; set; }

        public string Consulta_empresa { get; set; }

        public string Nombre { get; set; }
        public string Identificacion { get; set; }
        public string Documento { get; set; }
        public string Fuente { get; set; }
        public string Persona_amable { get; set; }
        public string Alias { get; set; }
        public string Alias_debil { get; set; }
        public string Delito { get; set; }
        public string Peps { get; set; }
        public string Zona { get; set; }
        public string Enlace { get; set; }
        public string Mas_informacion { get; set; }
        public bool Estado { get; set; }
        public string Resumen { get; set; }
        public string Hechos { get; set; }
        public string Entidad { get; set; }
        public bool Activado { get; set; }
        public bool Validado { get; set; }

        public int Tipo_grupo_lista { get; set; }
        public string Nombre_tipo_documento { get; set; }
        public string Nombre_tipo_lista { get; set; }
        public string Nombre_grupo_lista { get; set; }
        public int Resultado_prioridad { get; set; }
        public int Orden { get; set; }

        public DateTime Fecha_inicio { get; set; }
        public DateTime Fecha_finalización { get; set; }

        public int Identificacion_tipo_lista { get; set; }
        public int Tipo_persona_ID { get; set; }
        public int Tipo_Documento { get; set; }
        public int ID_usuario { get; set; }

        public DateTime Creado_en { get; set; }
        public DateTime? Actualizado_en { get; set; }
        public DateTime? Eliminado_en { get; set; }
    }
}
