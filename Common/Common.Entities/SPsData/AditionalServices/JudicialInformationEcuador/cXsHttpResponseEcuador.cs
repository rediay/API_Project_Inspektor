using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities.SPsData.AditionalServices.JudicialInformationEcuador
{
    public class cXsHttpResponseEcuador<T>
    {
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }

        public T Data { get; set; }
    }
}
