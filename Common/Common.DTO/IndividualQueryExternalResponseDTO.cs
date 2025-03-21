/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DTO.OwnLists;
using Common.DTO.Queries;
using Common.DTO.RestrictiveLists;
using Common.Entities.SPsData.AditionalServices;
using Common.Entities.SPsData.AditionalServices.Procuraduria;
using Common.Entities.SPsData.AditionalServices.RamaJudicial;
using System;
using System.Collections.Generic;

namespace Common.DTO
{
    public class IndividualQueryExternalResponseDTO : IndividualQueryExternalParamsDTO
    {
        public QueryExternalDTO query { get; set; }
        public IEnumerable<ListExternalDTO> lists { get; set; }
        public IEnumerable<OwnListResponseDTO> ownLists { get; set; }

    }
}
