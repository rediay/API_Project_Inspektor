/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using System;

namespace Common.DTO
{
    public class IndividualQueryParamsDTO
    {
        public string name { get; set; }
        public string document { get; set; }
        public int companyId { get; set; }
        public int UserId { get; set; }
        public int typeDocument { get; set; }
        public int typeDocumentPpt { get; set; }
        public int typeDocumentPolice { get; set; }
        public string ruc { get; set; }
        public int thirdTypeId { get; set; }
        public ThirdPartyTypeDTO thirdType { get; set; }
        public int typeDocumentProcuraduria { get; set; }
        public string digitVerification { get; set; }
        public int typeDocumentMilitary { get; set; }
        public int typeDocumentBme { get; set; }
        public int numberWords { get; set; }
        public Boolean hasProcuraduria { get; set; }
        public Boolean hasBme { get; set; }
        public Boolean hasRamaJudicial { get; set; }
        public Boolean hasRamaJudicialJEMPS { get; set; }
        public Boolean hasRegistryDeaths { get; set; }
        public Boolean hasPriority4 { get; set; }
        public Boolean hasMilitary { get; set; }
        public Boolean hasSimit { get; set; }
        public Boolean hasRues { get; set; }
        public Boolean hasSuperSocieties { get; set; }
        public Boolean hasEstadoEPS { get; set; }
        public Boolean hasSuperFinanciera { get; set; }
        public Boolean hasEstadoPermiso { get; set; }
        public Boolean hasAntecedentesCriminales { get; set; }
        public Boolean hasInformacionJudicial { get; set; }
        public Boolean hasSunat { get; set; }        
        public Boolean hasPolice { get; set; }        

    }
}
