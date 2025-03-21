using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public class DictionariesExcel
    {
        public static Dictionary<int, string> Monitoring()
        {
            var dictionary = new Dictionary<int, string>();
            dictionary.Add(0, "Fecha");
            dictionary.Add(1, "Detalle");
            dictionary.Add(2, "Id");
            dictionary.Add(3, "Número de Identificacion");
            dictionary.Add(4, "Json");            
            dictionary.Add(5, "Justificación");
            dictionary.Add(6, "Documento en Lista");
            dictionary.Add(7, "Nombre de Lista");
            dictionary.Add(8, "Fecha de Consulta");
            dictionary.Add(9, "Nombre De Consulta");
            dictionary.Add(10, "Número De Consulta");
            dictionary.Add(11, "Usuario consulta");
            dictionary.Add(12, "Estado");
            dictionary.Add(13, "Asunto");
            dictionary.Add(14, "Para");
            dictionary.Add(15, "Tipo de Lista");
            dictionary.Add(16, "Usuario");
            dictionary.Add(17, "Nombre Usuario");
            return dictionary;
        }

        public static Dictionary<int, string> NoticationSent()
        {
            var dictionary = new Dictionary<int, string>();
            dictionary.Add(0, "Fecha");
            dictionary.Add(1, "Detalle");
            dictionary.Add(2, "Id");            
            dictionary.Add(3, "Json");       
            dictionary.Add(4, "Asunto");
            dictionary.Add(5, "Para");          
            dictionary.Add(6, "Usuario");
            dictionary.Add(7, "Nombre Usuario");
            return dictionary;
        }
    }
}
