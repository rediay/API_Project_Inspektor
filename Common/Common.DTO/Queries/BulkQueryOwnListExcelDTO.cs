namespace Common.DTO.Queries
{
    public class BulkQueryOwnListExcelDTO
    {
        public string Nombre_consulta { get; set; }
        public string Identificacion_consulta { get; set; }

        public string Nombre { get; set; }
        public string Identificacion { get; set; }
        public string Tipo_identificacion { get; set; }
        public string Tipo_documento { get; set; }
        public string Fuente { get; set; }
        public string Alias { get; set; }
        public string Delito { get; set; }
        public string Enlace { get; set; }
        public string Mas_informacion { get; set; }
        public string Zona { get; set; }
        public string Creado_en { get; set; }
        public string Actualizado_en { get; set; }
        public int Compania_id { get; set; }
        public int Usuario_id { get; set; }
        public int Tipo_lista_propia_id { get; set; }
        public string Nombre_tipo_lista_propia { get; set; }
    }
}
