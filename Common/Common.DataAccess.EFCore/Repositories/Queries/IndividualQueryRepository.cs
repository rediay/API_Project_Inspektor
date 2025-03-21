/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DataAccess.EFCore.Repositories.RequestHelper;
using Common.DTO;
using Common.DTO.IndividualQueryExternal;
using Common.DTO.OwnLists;
using Common.DTO.Queries;
using Common.DTO.RestrictiveLists;
using Common.Entities;
using Common.Entities.SPsData;
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
using Common.Entities.SPsData.AditionalServices.Rues;
using Common.Entities.SPsData.AditionalServices.Simit;
using Common.Entities.SPsData.AditionalServices.Sunat;
using Common.Services.Infrastructure.Queries;
using Common.Services.Infrastructure.Services.Files;
using Common.Utils;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Common.DataAccess.EFCore.Repositories.Queries
{
    public class IndividualQueryRepository : BaseRepository<NotificationSettings, DataContext>, IIndividualQueryRepository
    {
        DataContext context;
        IConfiguration configuration;
        private readonly IFileShare fileShare;
        private readonly RequestRepository requestRepository;
        public IndividualQueryRepository(DataContext context, IConfiguration configuration, IFileShare fileShare
            , RequestRepository requestRepository) : base(context)
        {

            context.Database.SetCommandTimeout(TimeSpan.FromMinutes(10));
            this.context = context;
            this.configuration = configuration;
            this.fileShare = fileShare;
            this.requestRepository = requestRepository;
        }
        public async Task<IndividualQueryResponseDTO> getQuery(int IdQuery, ContextSession session)
        {
            var userId = session.UserId;
            var currentUSer = context.Users.Find(userId);

            IndividualQueryResponseDTO responseDto = await fileShare.FileDownloadAsync<IndividualQueryResponseDTO>(IdQuery);
            //IndividualQueryResponseDTO responseDto = FilesHelper.getIndividualQuery(IdQuery);
            if (responseDto != null && responseDto.query.CompanyId != currentUSer.CompanyId)
            {
                new ForbidResult("No tienes permiso para acceder a este recurso.");
                return null;

            }
            return await Task.FromResult<IndividualQueryResponseDTO>(responseDto);
        }
        public async Task<QueryDTO> previusQuery(IndividualQueryParamsDTO individualQueryParamsDTO, ContextSession session)
        {
            Entities.Query query = null;
            QueryDTO queryDTO = null;
            if ((!String.IsNullOrEmpty(individualQueryParamsDTO.name) || !String.IsNullOrEmpty(individualQueryParamsDTO.document)) && individualQueryParamsDTO.companyId != null)
                query = context.Queries.FromSqlInterpolated($"EXECUTE dbo.PreviusQuery {individualQueryParamsDTO.name}, {individualQueryParamsDTO.document}, {individualQueryParamsDTO.companyId}").AsNoTracking().ToList().FirstOrDefault();
            if (query != null)
                queryDTO = JsonConvert.DeserializeObject<QueryDTO>(JsonConvert.SerializeObject(query));
            return await Task.FromResult<QueryDTO>(queryDTO);
        }
        public async Task<IndividualQueryExternalResponseEsDTO> makeQueryExternal(IndividualQueryExternalParamsDTO individualQueryParamsDTO, ContextSession session)
        {
            var userId = session.UserId;
            var currentUSer = context.Users.Find(userId);
            IEnumerable<ListResponse> lists = Enumerable.Empty<ListResponse>();

            Query query = getQueryByCompany(userId, currentUSer.CompanyId, "3");
            lists = makeSearch(currentUSer, query, individualQueryParamsDTO, null);
            IEnumerable<CompanyTypeList> companyTypeList = context.CompanyTypeList.Where(c => c.CompanyId == currentUSer.CompanyId).ToList();

            IndividualQueryResponseDTO responseDto = new IndividualQueryResponseDTO();
            responseDto = JsonConvert.DeserializeObject<IndividualQueryResponseDTO>(JsonConvert.SerializeObject(individualQueryParamsDTO));
            responseDto.user = JsonConvert.DeserializeObject<UserDTO>(JsonConvert.SerializeObject(currentUSer));
            QueryDTO queryDTO = JsonConvert.DeserializeObject<QueryDTO>(JsonConvert.SerializeObject(query));

            responseDto.lists = lists.MapTo<List<ListDTO>>();

            //ownLists = context.OwnListResponse.FromSqlInterpolated($"EXECUTE dbo.IndividualQueryOwnList {individualQueryParamsDTO.name}, {individualQueryParamsDTO.document}, {currentUSer.CompanyId}").AsNoTracking().ToList();
            //responseDto.ownLists = ownLists.MapTo<List<OwnListResponseDTO>>();
            responseDto.ownLists = GetOwnList(currentUSer, individualQueryParamsDTO, null).MapTo<List<OwnListResponseDTO>>();
            responseDto.query = queryDTO;
            responseDto.heatMap = getHeatMap(lists);
            responseDto.UserId = currentUSer.Id;
            responseDto.companyId = currentUSer.CompanyId;

            //if (FilesHelper.saveIndividualQueryFile(responseDto))
            //{
            await fileShare.FileUploadAsync<IndividualQueryResponseDTO>(responseDto, responseDto.query.Id);
            IndividualQueryExternalResponseDTO externalResponseDto = JsonConvert.DeserializeObject<IndividualQueryExternalResponseDTO>(JsonConvert.SerializeObject(responseDto));
            externalResponseDto.quantityResults = responseDto.lists.Count();
            IndividualQueryExternalResponseEsDTO externalResponseEsDto = externalResponseDto.MapTo<IndividualQueryExternalResponseEsDTO>();
            externalResponseEsDto.Listas = externalResponseDto.lists.MapTo<List<ListExternalEsDTO>>();
            externalResponseEsDto.Listas_Propias = externalResponseDto.ownLists.MapTo<List<OwnListResponseEsDTO>>();
            return await Task.FromResult<IndividualQueryExternalResponseEsDTO>(externalResponseEsDto);
            //}

            //return null;

        }
        public async Task<IndividualQueryResponseDTO> makeQuery(IndividualQueryParamsDTO individualQueryParamsDTO, ContextSession session)
        {
            var userId = session.UserId;
            var currentUSer = context.Users.Find(userId);
            var listSearch = await context.CompanyTypeList.Where(obj => obj.Company.Id == currentUSer.CompanyId)
                .Include(x => x.ListType)
                .Include(x => x.User)
                .Include(x => x.ListType)
                .ThenInclude(ListType => ListType.ListGroup)
                .Include(l => l.ListType)
                .ThenInclude(c => c.Country)
                .Include(l => l.ListType)
                .ThenInclude(c => c.Periodicity)
                .AsNoTracking().ToListAsync();

            var image = (from sw in context.Companies where sw.Id == currentUSer.CompanyId select sw.Image).FirstOrDefault();

            cXsHttpResponse<Procuraduria> procuraduria = null;
            cXsHttpResponse<IEnumerable<RamaJudicial>> ramaJudicials = null;
            cXsHttpResponse<IEnumerable<RamaJudicialJEMPS>> ramaJudicialJEMPs = null;
            cXsHttpResponse<cInfoDefRegistraduria> cInfoDefRegistraduria = null;
            cXsHttpResponse<Military> military = null;
            cXsHttpResponse<List<cInfoSimitNew>> simit = null;
            cXsHttpResponse<Rues> rues = null;
            SuperSocieties superSocieties = null;
            cXsHttpResponse<Eps> eps = null;
            cXsHttpResponse<Ppt> ppt = null;
            cXsHttpResponse<CriminalRecordEcuador> criminalRecordEcuador = null;
            cXsHttpResponse<List<InfoJudicial>> infoJudicial = null;
            cXsHttpResponse<Sunat> sunat = null;
            cXsHttpResponse<Police> police = null;
            cXsHttpResponse<Bme> bme = null;

            Task<cXsHttpResponse<Procuraduria>> procuraduriaThread = Task.FromResult<cXsHttpResponse<Procuraduria>>(procuraduria);
            Task<cXsHttpResponse<IEnumerable<RamaJudicial>>> ramaJudicialThread = Task.FromResult<cXsHttpResponse<IEnumerable<RamaJudicial>>>(ramaJudicials);
            Task<cXsHttpResponse<IEnumerable<RamaJudicialJEMPS>>> ramaJudicialJEMPSThread = Task.FromResult<cXsHttpResponse<IEnumerable<RamaJudicialJEMPS>>>(ramaJudicialJEMPs);
            Task<cXsHttpResponse<cInfoDefRegistraduria>> registryDeathsThread = Task.FromResult<cXsHttpResponse<cInfoDefRegistraduria>>(cInfoDefRegistraduria);
            Task<cXsHttpResponse<Military>> militaryThread = Task.FromResult<cXsHttpResponse<Military>>(military);
            Task<cXsHttpResponse<List<cInfoSimitNew>>> simitThread = Task.FromResult<cXsHttpResponse<List<cInfoSimitNew>>>(simit);
            Task<cXsHttpResponse<Rues>> ruesThread = Task.FromResult<cXsHttpResponse<Rues>>(rues);
            Task<SuperSocieties> superSocietiesThread = Task.FromResult<SuperSocieties>(superSocieties);
            Task<cXsHttpResponse<Eps>> epsThread = Task.FromResult<cXsHttpResponse<Eps>>(eps);
            Task<cXsHttpResponse<Ppt>> pptThread = Task.FromResult<cXsHttpResponse<Ppt>>(ppt);
            Task<cXsHttpResponse<CriminalRecordEcuador>> criminalRecordEcuadorThread = Task.FromResult<cXsHttpResponse<CriminalRecordEcuador>>(criminalRecordEcuador);
            Task<cXsHttpResponse<List<InfoJudicial>>> infoJudicialEcuadorThread = Task.FromResult<cXsHttpResponse<List<InfoJudicial>>>(infoJudicial);
            Task<cXsHttpResponse<Sunat>> sunatThread = Task.FromResult<cXsHttpResponse<Sunat>>(sunat);
            Task<cXsHttpResponse<Police>> policeThread = Task.FromResult(police);
            Task<cXsHttpResponse<Bme>> bmeThread = Task.FromResult(bme);

            IEnumerable<ListResponse> lists = Enumerable.Empty<ListResponse>();

            IEnumerable<OwnListResponse> ownLists = Enumerable.Empty<OwnListResponse>();
            IndividualQueryResponseDTO responseDto = new IndividualQueryResponseDTO();
            responseDto = JsonConvert.DeserializeObject<IndividualQueryResponseDTO>(JsonConvert.SerializeObject(individualQueryParamsDTO));
            String thirdTypeName = String.Empty;
            if (individualQueryParamsDTO.thirdTypeId != null)
            {
                var thirdPartyType = context.ThirdPartyTypeList.Find(individualQueryParamsDTO.thirdTypeId);
                thirdTypeName = thirdPartyType != null ? thirdPartyType.Name : String.Empty;
                responseDto.thirdPartyType = thirdPartyType.MapTo<ThirdPartyTypeDTO>();
            }
            var webserviceConfig = configuration.GetSection("imageList");
            //Entities.Query query = context.Queries.FromSqlInterpolated($"EXECUTE dbo.addNewQuery {individualQueryParamsDTO.companyId}, {individualQueryParamsDTO.UserId},1,{thirdTypeName}").AsNoTracking().ToList().FirstOrDefault();
            Query query = getQueryByCompany(userId, currentUSer.CompanyId, "1");
            IEnumerable<CompanyTypeList> companyTypeList = context.CompanyTypeList.Where(c => c.CompanyId == individualQueryParamsDTO.companyId).ToList();

            responseDto.user = JsonConvert.DeserializeObject<UserDTO>(JsonConvert.SerializeObject(currentUSer));
            QueryDTO queryDTO = JsonConvert.DeserializeObject<QueryDTO>(JsonConvert.SerializeObject(query));


            lists = makeSearch(currentUSer, query, null, individualQueryParamsDTO);

            //responseDto.lists = lists.MapTo<List<ListDTO>>();
            //Crear url para consultar la imagen de cada item de la lista
            var lisrforUrlImage = lists.MapTo<List<ListDTO>>().ToList();
            lisrforUrlImage.ForEach(x => x.urlImage = $"{webserviceConfig["urlImage"] + x.Id}.gif");

            responseDto.lists = lisrforUrlImage;

            ownLists = context.OwnListResponse.FromSqlInterpolated($"EXECUTE dbo.IndividualQueryOwnList {individualQueryParamsDTO.name}, {individualQueryParamsDTO.document}, {individualQueryParamsDTO.companyId}").AsNoTracking().ToList();
            //responseDto.ownLists = ownLists.MapTo<List<OwnListResponseDTO>>();
            responseDto.ownLists = GetOwnList(currentUSer, null, individualQueryParamsDTO).MapTo<List<OwnListResponseDTO>>();
            responseDto.query = queryDTO;
            if (individualQueryParamsDTO.hasProcuraduria)
            {
                string digitVerification = individualQueryParamsDTO.typeDocumentProcuraduria == 2 ? individualQueryParamsDTO.digitVerification : "";
                procuraduriaThread = requestRepository.makeProcuraduriaRequest(individualQueryParamsDTO.document + digitVerification, individualQueryParamsDTO.typeDocumentProcuraduria);
            }
            if (individualQueryParamsDTO.hasRamaJudicial)
                ramaJudicialThread = requestRepository.makeRamaJudicialRequest(HttpUtility.UrlEncode(individualQueryParamsDTO.name));

            if (individualQueryParamsDTO.hasRamaJudicialJEMPS)
                ramaJudicialJEMPSThread = requestRepository.makeRamaJudicialJEMPSRequest(individualQueryParamsDTO.document);

            if (individualQueryParamsDTO.hasRegistryDeaths)
                registryDeathsThread = requestRepository.makeRegistryDeaths(individualQueryParamsDTO.document);

            if (individualQueryParamsDTO.hasMilitary)
                militaryThread = requestRepository.makeMiltaryRequest(individualQueryParamsDTO.document);

            if (individualQueryParamsDTO.hasSimit)
                simitThread = requestRepository.makeSimit(individualQueryParamsDTO.document);

            if (individualQueryParamsDTO.hasRues)
                ruesThread = requestRepository.makeRuesRequest(individualQueryParamsDTO.document);

            if (individualQueryParamsDTO.hasSuperSocieties)
                superSocietiesThread = requestRepository.makeSuperSocietiesRequest(individualQueryParamsDTO.document);

            if (individualQueryParamsDTO.hasEstadoEPS)
                epsThread = requestRepository.makeConsultaEPS(individualQueryParamsDTO.document);

            if (individualQueryParamsDTO.hasEstadoPermiso)
                pptThread = requestRepository.makeConsultaPPT(individualQueryParamsDTO.typeDocumentPpt, individualQueryParamsDTO.document);

            if (individualQueryParamsDTO.hasAntecedentesCriminales)
                criminalRecordEcuadorThread = requestRepository.makeConsultaCriminalRecordEcuador(individualQueryParamsDTO.document);

            if (individualQueryParamsDTO.hasInformacionJudicial)
                infoJudicialEcuadorThread = requestRepository.makeConsultaJudicialInformationEcuador(individualQueryParamsDTO.document);

            if (individualQueryParamsDTO.hasSunat)
                sunatThread = requestRepository.makeConsultaSunat(individualQueryParamsDTO.ruc);

            if (individualQueryParamsDTO.hasPolice)
                policeThread = requestRepository.makePoliceRequest(individualQueryParamsDTO.document, individualQueryParamsDTO.typeDocumentPolice);

            if (individualQueryParamsDTO.hasBme)
                bmeThread = requestRepository.makeBmeRequest(individualQueryParamsDTO.document, individualQueryParamsDTO.typeDocumentBme);

            await Task.WhenAll(procuraduriaThread, ramaJudicialThread, ramaJudicialJEMPSThread, registryDeathsThread, militaryThread, simitThread,
                                ruesThread, superSocietiesThread, epsThread, pptThread, criminalRecordEcuadorThread, infoJudicialEcuadorThread, sunatThread
                                , policeThread, bmeThread);

            responseDto.procuraduria = procuraduriaThread.Result;
            responseDto.ramaJudicial = ramaJudicialThread.Result;
            responseDto.ramaJudicialJEMPS = ramaJudicialJEMPSThread.Result;
            responseDto.RegistryDeaths = registryDeathsThread.Result;
            responseDto.military = militaryThread.Result;
            responseDto.simit = simitThread.Result;
            responseDto.rues = ruesThread.Result;
            responseDto.superSocieties = superSocietiesThread.Result;
            responseDto.eps = epsThread.Result;
            responseDto.Ppt = pptThread.Result;
            responseDto.criminalRecordEcuador = criminalRecordEcuadorThread.Result;
            responseDto.infoJudicialEcuador = infoJudicialEcuadorThread.Result;
            responseDto.sunat = sunatThread.Result;
            responseDto.police = policeThread.Result;
            responseDto.bme = bmeThread.Result;

            responseDto.heatMap = getHeatMap(lists);

            responseDto.listsearch = listSearch.MapTo<List<CompanyTypeListDTO>>();

            responseDto.image = image;

            await fileShare.FileUploadAsync<IndividualQueryResponseDTO>(responseDto, responseDto.query.Id);
            //if (FilesHelper.saveIndividualQueryFile(responseDto))
                return await Task.FromResult<IndividualQueryResponseDTO>(responseDto);

            return null;

        }

        private Query getQueryByCompany(int userId, int companyId, string typeQuery)
        {
            return context.Queries.FromSqlInterpolated($"EXECUTE dbo.addNewQuery {companyId}, {userId},{typeQuery},''").AsNoTracking().ToList().FirstOrDefault();

        }
        private string getHeatMap(IEnumerable<ListResponse> lists)
        {
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "Files/heatMap/heat_map.json");
            using (StreamReader r = new StreamReader(FilePath))
            {
                string json = r.ReadToEnd();
                JObject jsonHeatMap = JObject.Parse(json);
                foreach (var list in lists)
                {
                    int order = list.Order;
                    int priority = list.PriorityResult;
                    if (order > 0 && order <= 5 && priority > 0 && priority <= 4)
                    {
                        int value = Int32.Parse(jsonHeatMap["series"][order - 1]["data"][priority - 1]["text"].ToString());
                        value += 1;
                        jsonHeatMap["series"][order - 1]["data"][priority - 1]["text"] = value;
                    }
                }

                string _script = jsonHeatMap.ToString();
                return _script;

            }
        }


        //public static Procuraduria makeProcuraduriaRequest(string identification, int typeDocument, IConfigurationSection config)
        //{
        //    string url = $"{config["procUrl"]}{typeDocument}&NumeroIdentificacion={identification}";
        //    HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
        //    Procuraduria procuraduria;
        //    webReq.Method = "GET";
        //    webReq.Timeout = 45000;
        //    try
        //    {
        //        using (HttpWebResponse response = (HttpWebResponse)webReq.GetResponse())
        //        using (Stream stream = response.GetResponseStream())

        //        using (StreamReader reader = new StreamReader(stream))
        //        {
        //            var json = reader.ReadToEnd();
        //            json = json.Replace(",\"not_criminal_records\":false", "");
        //            procuraduria = JsonConvert.DeserializeObject<Procuraduria>(json);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //    return procuraduria;
        //}





        //private async Task<cXsHttpResponse<Rues>> makeRuesRequest(string document, IConfigurationSection config)
        //{
        //    var myHttpClient = new HttpClient();
        //    cXsHttpResponse<Rues> rues = new cXsHttpResponse<Rues>();
        //    string url = config["ruesUrl"];
        //    var response1 = myHttpClient.GetAsync(url);
        //    var doc = response1.Result.Content.ReadAsStringAsync().Result;
        //    var regex = new Regex("value=\"([^\"]+)");
        //    string myCapturedText = string.Empty;
        //    try
        //    {
        //        if (regex.IsMatch(doc))
        //        {
        //            myCapturedText = regex.Match(doc).Groups[1].Value;
        //        }
        //        var formContent = new FormUrlEncodedContent(new[] {

        //            new KeyValuePair<string, string>("txtNIT",document),
        //            new KeyValuePair<string, string>("__RequestVerificationToken", myCapturedText)
        //        });
        //        var response2 = myHttpClient.PostAsync(url, formContent);
        //        rues = RuesTocInfoRues(JsonConvert.DeserializeObject<cRuesJson>(response2.Result.Content.ReadAsStringAsync().Result));
        //        return rues;


        //    }
        //    catch (Exception ex)
        //    {
        //        rues.HasError = true;
        //        rues.ErrorMessage = config["serviceError"];
        //        return rues;
        //    }
        //    finally
        //    {
        //        myHttpClient.Dispose();
        //    }
        //}







        public static string stringNotIn(IEnumerable<ListResponse> list)
        {
            string stringResponse = "";
            if (list.Count() > 0)
            {
                int i = 0;
                foreach (ListResponse obj in list)
                {
                    i++;
                    stringResponse += obj.Id + (i < list.Count() ? ", " : "");
                }
            }
            return stringResponse.Trim();
        }
        private IEnumerable<ListResponse> makeSearch(User currentUSer, Query query, IndividualQueryExternalParamsDTO individualQueryExternalParamsDTO = null, IndividualQueryParamsDTO individualQueryInternalParamsDTO = null)
        {
            var name = individualQueryExternalParamsDTO != null ? individualQueryExternalParamsDTO.name : individualQueryInternalParamsDTO.name;
            var document = individualQueryExternalParamsDTO != null ? individualQueryExternalParamsDTO.document : individualQueryInternalParamsDTO.document;
            var hasPriority4 = individualQueryExternalParamsDTO != null ? individualQueryExternalParamsDTO.hasPriority4 : individualQueryInternalParamsDTO.hasPriority4;
            var numberWords = individualQueryExternalParamsDTO != null ? individualQueryExternalParamsDTO.numberWords : individualQueryInternalParamsDTO.numberWords;

            IEnumerable<ListResponse> lists = Enumerable.Empty<ListResponse>();
            IEnumerable<ListResponse> listP1 = Enumerable.Empty<ListResponse>();
            IEnumerable<ListResponse> listP2 = Enumerable.Empty<ListResponse>();
            IEnumerable<ListResponse> listP3 = Enumerable.Empty<ListResponse>();
            IEnumerable<ListResponse> listP4 = Enumerable.Empty<ListResponse>();
            IEnumerable<OwnListResponse> ownLists = Enumerable.Empty<OwnListResponse>();

            string notIn;

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(document))
                listP1 = context.ListResponse.FromSqlInterpolated($"EXECUTE dbo.IndividualQueryPriority1 {name}, {document}, {currentUSer.CompanyId}").AsNoTracking().ToList();
            lists = lists.Concat(listP1).ToList();
            notIn = stringNotIn(lists);
            if (!string.IsNullOrEmpty(document))
                listP2 = context.ListResponse.FromSqlInterpolated($"EXECUTE dbo.IndividualQueryPriority2 {document}, {notIn}, {currentUSer.CompanyId}").AsNoTracking().ToList();
            lists = lists.Concat(listP2).ToList();
            notIn = stringNotIn(lists);
            if (!string.IsNullOrEmpty(name))
                listP3 = context.ListResponse.FromSqlInterpolated($"EXECUTE dbo.IndividualQueryPriority3 {name}, {notIn}, {currentUSer.CompanyId}").AsNoTracking().ToList();
            lists = lists.Concat(listP3).ToList();
            notIn = stringNotIn(lists);
            if (hasPriority4)
                listP4 = context.ListResponse.FromSqlInterpolated($"EXECUTE dbo.IndividualQueryPriority4 {name}, {notIn}, {currentUSer.CompanyId}, {numberWords}").AsNoTracking().ToList();

            //Procuraduria procuraduria;
            lists = lists.Concat(listP4).ToList();

            context.QueryDetails.FromSqlInterpolated($"EXECUTE dbo.addIndividualQueryDetail {name}, {document}, {query.Id},{lists.Count()}").AsNoTracking().ToList().FirstOrDefault();

            return lists;
        }
        private IEnumerable<OwnListResponse> GetOwnList(User currentUSer, IndividualQueryExternalParamsDTO individualQueryExternalParamsDTO = null, IndividualQueryParamsDTO individualQueryInternalParamsDTO = null)
        {
            var name = individualQueryExternalParamsDTO != null ? individualQueryExternalParamsDTO.name : individualQueryInternalParamsDTO.name;
            var document = individualQueryExternalParamsDTO != null ? individualQueryExternalParamsDTO.document : individualQueryInternalParamsDTO.document;
            var companyId = individualQueryExternalParamsDTO != null ? currentUSer.CompanyId : individualQueryInternalParamsDTO.companyId;
            return context.OwnListResponse.FromSqlInterpolated($"EXECUTE dbo.IndividualQueryOwnList {name}, {document}, {companyId}").AsNoTracking().ToList();
        }
    }
}
