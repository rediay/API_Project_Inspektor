/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities.SPsData.AditionalServices.Procuraduria;
using Common.Entities.SPsData.AditionalServices.RamaJudicial;
using System;
using System.Collections.Generic;

namespace Common.DTO.Queries
{
    public class BulkQueryResponseDTO
    {
        public QueryDTO query { get; set; }
        public IEnumerable<ListsBulkQueryDTO> lists { get; set; }
        public IEnumerable<OwnListBulkQueryResponseDTO> ownLists { get; set; }
        public List<Procuraduria> procuraduria { get; set; }
        public IEnumerable<RamaJudicial> ramaJudicial { get; set; }
        public IEnumerable<RamaJudicialJEMPS> ramaJudicialJEMPS { get; set; }
        public List<CompanyTypeListDTO> listsearch { get; set; }
        public List<QueryDetailDTO> QueryDetails { get; set; }
        public string image { get; set; }


    }
}
