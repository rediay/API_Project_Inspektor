/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using System;

namespace Common.DTO
{
    public class NombreCedulaDTO
    {
        public int IdCliente { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string NombreCompleto { get; set; }
        public int TipoIden { get; set; }
        public DateTime FechaReg { get; set; }
        public string Fuente { get; set; }
    }
}
