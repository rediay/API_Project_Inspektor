using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entities.SPsData.AditionalServices
{
    public class cXsHttpResponse<T>
    {
        public bool HasError { get; set; } = false;

        public string ErrorMessage { get; set; }

        public T Data { get; set; }
        public List<T> ListData { get; set; }
    }
}
