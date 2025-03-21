/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DTO.OwnLists;
using Common.DTO.Queries;
using Common.DTO.RestrictiveLists;
using Common.Entities.SPsData.AditionalServices;
using Common.Entities.SPsData.AditionalServices.Bme;
using Common.Entities.SPsData.AditionalServices.CriminalRecordEcuador;
using Common.Entities.SPsData.AditionalServices.EPS;
using Common.Entities.SPsData.AditionalServices.JudicialInformationEcuador;
using Common.Entities.SPsData.AditionalServices.Police;
using Common.Entities.SPsData.AditionalServices.PPT;
using Common.Entities.SPsData.AditionalServices.Procuraduria;
using Common.Entities.SPsData.AditionalServices.RamaJudicial;
using Common.Entities.SPsData.AditionalServices.RegistryDeaths;
using Common.Entities.SPsData.AditionalServices.Simit;
using Common.Entities.SPsData.AditionalServices.Sunat;
using System;
using System.Collections.Generic;

namespace Common.DTO
{
    public class IndividualQueryResponseDTO:IndividualQueryParamsDTO
    {
        public QueryDTO query { get; set; }
        public IEnumerable<ListDTO> lists { get; set; }
        public IEnumerable<OwnListResponseDTO> ownLists { get; set; }        
        public cXsHttpResponse<Procuraduria> procuraduria { get; set; }
        public cXsHttpResponse<Bme> bme { get; set; }
        public cXsHttpResponse<IEnumerable<RamaJudicial>> ramaJudicial { get; set; }
        public cXsHttpResponse<IEnumerable<RamaJudicialJEMPS>> ramaJudicialJEMPS { get; set; }
        public cXsHttpResponse<cInfoDefRegistraduria> RegistryDeaths { get; set; }
        public cXsHttpResponse<Military> military { get; set; }
        public SuperSocieties superSocieties { get; set; }
        public cXsHttpResponse<Rues> rues { get; set; }
        public cXsHttpResponse<List<cInfoSimitNew>> simit { get; set; }
        public cXsHttpResponse<Eps> eps { get; set; }
        public cXsHttpResponse<Ppt> Ppt { get; set; }
        public cXsHttpResponse<List<InfoJudicial>> infoJudicialEcuador { get; set; }
        public cXsHttpResponse<CriminalRecordEcuador> criminalRecordEcuador { get; set; }
        public cXsHttpResponse<Sunat> sunat { get; set; }
        public cXsHttpResponse<Police> police { get; set; }
        public string heatMap { get; set; }
        public string image { get; set; }
        public UserDTO user { get; set; }
        public List<CompanyTypeListDTO> listsearch { get; set; }
        public ThirdPartyTypeDTO thirdPartyType { get; set; }


    }
}
