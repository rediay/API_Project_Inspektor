using AutoMapper;
using Common.DataAccess.EFCore.Repositories.RequestHelper;
using Common.DTO.Queries;
using Common.Entities.BulkQuery;
using Common.Entities.SPsData.AditionalServices;
using Common.Entities.SPsData.AditionalServices.Procuraduria;
using Common.Entities.SPsData.AditionalServices.RamaJudicial;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories.Queries;
using Common.Services.Infrastructure.Services.Files;
using Common.Services.Infrastructure.Services.Queries;
using Common.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Common.Services.Queries
{
    public class BulkQueryAdditionalService : BaseService, IBulkQueryAdditionalService
    {
        protected readonly IBulkQueryAdditionalRepository _bulkQueryAdditionalRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BulkQueryService> _logger;
        private readonly RequestRepository _requestRepository;
        private readonly IFileShare fileShare;

        IConfiguration _configuration;

        public BulkQueryAdditionalService(ICurrentContextProvider contextProvider, IBulkQueryAdditionalRepository bulkQueryAdditionalRepository,
            ILogger<BulkQueryService> logger, RequestRepository requestRepository, IMapper mapper, IConfiguration configuration, IFileShare fileShare) : base(contextProvider)
        {
            _bulkQueryAdditionalRepository = bulkQueryAdditionalRepository;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
            _requestRepository = requestRepository;
            this.fileShare = fileShare;
        }

        public async Task BulkQueryAdditionalAsync(BulkQueryServicesAdditionalRequestDTO bulkQueryRequestDTO)
        {
            try
            {
                var userId = Session.UserId;
                string[] columnNames = { "TypeId", "Nid", "Name", "Dv" };

                bulkQueryRequestDTO.dataTableBulkQuery = DataTables.ColumnsDataTypeToString(
                                                  DataTables.ChangeColumnNames(
                                                  FilesHelper.ExcelToDataSet(
                                                  FilesHelper.IFormFileToByteArray(bulkQueryRequestDTO.File)).Tables[0], columnNames));
                bool result = validationDataNullOrVoid(bulkQueryRequestDTO.dataTableBulkQuery, bulkQueryRequestDTO);

                if (!result)
                {
                    Task.Run(async () =>
                    {
                        var webserviceConfig = _configuration.GetSection("webservicesOptions");
                        int count = 0;
                        int limit = bulkQueryRequestDTO.dataTableBulkQuery.Rows.Count;

                        var responseDTO = new BulkQueryServicesAdditionalResponseDTO();
                        BulkQueryServicesAdditional bulkQueryServicesAdditional = new BulkQueryServicesAdditional
                        {
                            attorneyService = bulkQueryRequestDTO.hasProcuraduria,
                            judicialBranchService = bulkQueryRequestDTO.hasRamaJudicial,
                            jempsJudicialBranchService = bulkQueryRequestDTO.hasRamaJudicialJEMPS,
                            ConsultingStatus = false,
                            CurrentConsulting = count,
                            TotalConsulting = limit,
                            CreatedAt = DateTime.Now
                        };

                        // Lógica de trabajo en segundo plano
                        cXsHttpResponse<Procuraduria> procuraduria = null;
                        cXsHttpResponse<IEnumerable<RamaJudicial>> ramaJudicials = null;
                        cXsHttpResponse<IEnumerable<RamaJudicialJEMPS>> ramaJudicialJEMPs = null;

                        string IdentificationType = "";
                        string name = "";
                        string document = "";
                        string CheckDigit = "";

                        foreach (DataRow row in bulkQueryRequestDTO.dataTableBulkQuery.Rows)
                        {
                            BulkQueryThirdTypeServicesAdditionalResponseDTO DataThirdType = new BulkQueryThirdTypeServicesAdditionalResponseDTO();
                            ListDataExcel listDataExcel = new ListDataExcel();
                            if (bulkQueryServicesAdditional.Id != 0)
                            {
                                var responseFile = getBulkQueryServiceAdditional(bulkQueryServicesAdditional.Id);
                                responseDTO = responseFile.Result.MapTo<BulkQueryServicesAdditionalResponseDTO>();
                                //FilesHelper.deleteQuery(bulkQueryServicesAdditional.Id);
                            }

                            Task<cXsHttpResponse<Procuraduria>> procuraduriaThread = Task.FromResult<cXsHttpResponse<Procuraduria>>(procuraduria);
                            Task<cXsHttpResponse<IEnumerable<RamaJudicialJEMPS>>> ramaJudicialJEMPSThread = Task.FromResult<cXsHttpResponse<IEnumerable<RamaJudicialJEMPS>>>(ramaJudicialJEMPs);
                            Task<cXsHttpResponse<IEnumerable<RamaJudicial>>> ramaJudicialThread = Task.FromResult<cXsHttpResponse<IEnumerable<RamaJudicial>>>(ramaJudicials);

                            IdentificationType = row["TypeId"].ToString().Trim().Replace(".", "");
                            name = row["Name"].ToString();
                            document = row["Nid"].ToString();
                            CheckDigit = IdentificationType.Equals('2') ? row["Dv"].ToString() : "";

                            if (bulkQueryRequestDTO.hasProcuraduria)
                            {
                                if (!string.IsNullOrEmpty(IdentificationType))
                                {
                                    procuraduriaThread = _requestRepository.makeProcuraduriaRequest(document + CheckDigit, Convert.ToInt32(IdentificationType));
                                }
                                else
                                {
                                    listDataExcel.Attorney = "No tiene tipo de identificación";
                                }

                            }

                            if (bulkQueryRequestDTO.hasRamaJudicialJEMPS)
                                ramaJudicialJEMPSThread = _requestRepository.makeRamaJudicialJEMPSRequest(document);

                            if (bulkQueryRequestDTO.hasRamaJudicial)
                                ramaJudicialThread = _requestRepository.makeRamaJudicialRequest(HttpUtility.UrlEncode(name));

                            await Task.WhenAll(procuraduriaThread, ramaJudicialJEMPSThread, ramaJudicialThread);

                            if (procuraduriaThread.Result != null)
                            {
                                String html_contaduria = procuraduriaThread.Result.Data.html_response != null ? procuraduriaThread.Result.Data.html_response.ToString() : "";
                                if (!html_contaduria.Equals("") && html_contaduria.Contains("Datos del ciudadano") && !html_contaduria.Contains("El ciudadano no presenta antecedentes"))
                                    listDataExcel.Attorney = "Con coincidencia";
                                else
                                    listDataExcel.Attorney = "Sin coincidencia";

                                if (procuraduriaThread.Result.HasError != false)
                                    listDataExcel.Attorney = ramaJudicialThread.Result.ErrorMessage;
                            }

                            if (ramaJudicialJEMPSThread.Result != null)
                            {
                                if (ramaJudicialJEMPSThread.Result.HasError != true)
                                    listDataExcel.JEPMSJudicialBranch = ramaJudicialJEMPSThread.Result.Data != null ? "Con coincidencia" : "Sin coincidencia";
                                else
                                    listDataExcel.JEPMSJudicialBranch = ramaJudicialJEMPSThread.Result.ErrorMessage;
                            }

                            if (ramaJudicialThread.Result != null)
                            {
                                if (ramaJudicialThread.Result.HasError != true)
                                    listDataExcel.JudicialBranch = ramaJudicialThread.Result.Data != null ? "Con coincidencia" : "Sin coincidencia";
                                else
                                    listDataExcel.JudicialBranch = ramaJudicialThread.Result.ErrorMessage;
                            }

                            count++;

                            bulkQueryServicesAdditional.CurrentConsulting = count;
                            bulkQueryServicesAdditional = await _bulkQueryAdditionalRepository.AddFileTable(bulkQueryServicesAdditional, userId);

                            responseDTO.QueryServiceAdditional = bulkQueryServicesAdditional.MapTo<BulkQueryServicesAdditionalDTO>();
                            DataThirdType.IdentificationType = IdentificationType;
                            DataThirdType.Name = name;
                            DataThirdType.Document = document;
                            DataThirdType.CheckDigit = CheckDigit;
                            listDataExcel.Name = name;
                            listDataExcel.Document = document;
                            DataThirdType.Procuraduria = procuraduriaThread.Result;
                            DataThirdType.RamaJudicial = ramaJudicialThread.Result;
                            DataThirdType.RamaJudicialJEMPS = ramaJudicialJEMPSThread.Result;

                            if (responseDTO.DataThirdType == null)
                            {
                                responseDTO.DataThirdType = new List<BulkQueryThirdTypeServicesAdditionalResponseDTO>();
                            }
                            responseDTO.DataThirdType.Add(DataThirdType);

                            if (responseDTO.ListDataExcels == null)
                            {
                                responseDTO.ListDataExcels = new List<ListDataExcel>();
                            }

                            responseDTO.ListDataExcels.Add(listDataExcel);
                            await fileShare.FileUploadAsync<BulkQueryServicesAdditionalResponseDTO>(responseDTO, responseDTO.QueryServiceAdditional.Id, true);
                            //FilesHelper.saveBulkQueryServiceAdditionalFile(responseDTO);

                        }
                    });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private bool validationDataNullOrVoid(DataTable dt, BulkQueryServicesAdditionalRequestDTO bulkQueryRequestDTO)
        {

            try
            {
                int rowVoidOrNullRamaJudicial = 0;
                int rowVoidOrNullProcuraduria = 0;
                int rowVoidOrNullRamaJudicialJEMPS = 0;

                if (bulkQueryRequestDTO.hasRamaJudicial)
                {
                    rowVoidOrNullRamaJudicial = dt.AsEnumerable().Count(fila => fila.Field<string>("Name") == ""
                                                                                 || fila.Field<string>("Name") == null);
                }

                if (bulkQueryRequestDTO.hasProcuraduria)
                {
                    rowVoidOrNullProcuraduria = dt.AsEnumerable().Count(fila => fila.Field<string>("TypeId") == ""
                                                                                 || fila.Field<string>("TypeId") == null
                                                                                 || fila.Field<string>("Nid") == null
                                                                                 || fila.Field<string>("Nid") == "");
                }

                if (bulkQueryRequestDTO.hasRamaJudicialJEMPS)
                {
                    rowVoidOrNullRamaJudicialJEMPS = dt.AsEnumerable().Count(fila => fila.Field<string>("Nid") == null
                                                                                 || fila.Field<string>("Nid") == "");
                }

                return (bulkQueryRequestDTO.hasRamaJudicial && rowVoidOrNullRamaJudicial > 0) ||
                       (bulkQueryRequestDTO.hasProcuraduria && rowVoidOrNullProcuraduria > 0) ||
                       (bulkQueryRequestDTO.hasRamaJudicialJEMPS && rowVoidOrNullRamaJudicialJEMPS > 0);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public async Task<BulkQueryServicesAdditionalResponseDTO> getBulkQueryServiceAdditional(int QueryId)
        {
            return await _bulkQueryAdditionalRepository.getBulkQueryServiceAdditional(QueryId, Session);
        }
        public async Task<List<BulkQueryServicesAdditionalDTO>> getBulkQueryServiceAdditionalTable()
        {
            return await _bulkQueryAdditionalRepository.getBulkQueryServiceAdditionalTable(Session);
        }
    }
}
