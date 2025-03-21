/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;

namespace Common.DTO.Queries
{
    public class BulkQueryRequestDTO
    {
        public int? Id { get; set; }
        public IFormFile File { get; set; }
        public DataTable dataTablefromFile { get; set; }
        public DataSet dataSetPriorities { get; set; }
        public List<string> StringNamesP1 { get; set; }
        public List<string> StringNamesP3 { get; set; }
        public int CompanyId { get; set; }
        public int? NWords { get; set; }
    } 
}
