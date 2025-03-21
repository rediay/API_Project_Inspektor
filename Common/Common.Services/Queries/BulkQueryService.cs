/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using AutoMapper;
using Common.DTO;
using Common.DTO.Queries;
using Common.DTO.RestrictiveLists;
using Common.Entities;
using Common.Entities.SPsData;
using Common.Entities.SPsData.AditionalServices.Procuraduria;
using Common.Entities.SPsData.AditionalServices.RamaJudicial;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Queries;
using Common.Services.Infrastructure.Repositories.Files;
using Common.Services.Infrastructure.Services.Files;
using Common.Utils;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Office2013.Drawing.Chart;
using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using DocumentFormat.OpenXml.Wordprocessing;
using MailKit.Search;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DataTable = System.Data.DataTable;

namespace Common.Services
{
    public class BulkQueryService : BaseService, IBulkQueryService
    {
        protected readonly IBulkQueryRepository _ibulkQueryRepository;
        private readonly IMapper _mapper;
        IConfiguration _configuration;
        private readonly IFileShare fileShare;
        public BulkQueryService(ICurrentContextProvider contextProvider, IBulkQueryRepository bulkQueryRepository, IMapper mapper, IConfiguration configuration, IFileShare fileShare) : base(contextProvider)
        {
            this._ibulkQueryRepository = bulkQueryRepository;
            _mapper = mapper;
            _configuration = configuration;
            this.fileShare = fileShare;
        }

        public async Task<BulkQueryResponseDTO> BulkQuery(BulkQueryRequestDTO bulkQueryRequestDTO)
        {

            string[] columnNames = { "TypeId", "Nid", "Name", "Dv" };
            bulkQueryRequestDTO.dataTablefromFile = DataTables.ColumnsDataTypeToString(
                                              DataTables.ChangeColumnNames(
                                              FilesHelper.ExcelToDataSet(
                                              FilesHelper.IFormFileToByteArray(bulkQueryRequestDTO.File)).Tables[0], columnNames));
            var queryData = await _ibulkQueryRepository.BulkQuery(bulkQueryRequestDTO, Session);
            IEnumerable<ListsBulkQueryDTO> listsFinded = Enumerable.Empty<ListsBulkQueryDTO>();
            Dictionary<int, int> counterPrioritys = new Dictionary<int, int>();
            string notIn = String.Empty;
            DataTable invalidThirds = validateThridsInFiles(bulkQueryRequestDTO.dataTablefromFile);
            if (invalidThirds.Rows.Count > 0)
            {
                var invalidNames = string.Empty;
                for (int i = 0; i < invalidThirds.Rows.Count; i++)
                {
                    object[] rowData = invalidThirds.Rows[i].ItemArray;
                    invalidNames += rowData[2].ToString().Trim() + " | ";

                }
                throw new Exception($"Los siguientes nombres tienen menos de 2 palabras, por favor realice una consulta individual : {invalidNames}");
            }
            bulkQueryRequestDTO.dataSetPriorities = DataSetPriorities(bulkQueryRequestDTO.dataTablefromFile);
            //var prioritiesTables = bulkQueryRequestDTO.dataSetPriorities.Tables;
            IEnumerable<ListsBulkQueryDTO> AllListResponseSaved = Enumerable.Empty<ListsBulkQueryDTO>();
            QueryResultServiceDTO resultPriority1 = priority1(bulkQueryRequestDTO, queryData.QueryDetail);
            AllListResponseSaved = AllListResponseSaved.Concat(resultPriority1.list);
            counterPrioritys = resultPriority1.DictionaryQuantityUserPerList;
            notIn = stringNotIn(AllListResponseSaved);

            QueryResultServiceDTO resultPriority2 = priority2(bulkQueryRequestDTO, queryData.QueryDetail, notIn);
            AllListResponseSaved = AllListResponseSaved.Concat(resultPriority2.list);
            counterPrioritys = counterPrioritys.Concat(resultPriority2.DictionaryQuantityUserPerList)
                   .GroupBy(x => x.Key)
                   .ToDictionary(x => x.Key, x => x.Sum(y => y.Value));

            notIn = stringNotIn(AllListResponseSaved);
            QueryResultServiceDTO resultPriority3 = priority3(bulkQueryRequestDTO, queryData.QueryDetail, notIn);
            AllListResponseSaved = AllListResponseSaved.Concat(resultPriority3.list);
            counterPrioritys = counterPrioritys.Concat(resultPriority3.DictionaryQuantityUserPerList)
                   .GroupBy(x => x.Key)
                   .ToDictionary(x => x.Key, x => x.Sum(y => y.Value));

            if (bulkQueryRequestDTO.NWords > 0)
            {
                notIn = stringNotIn(AllListResponseSaved);
                QueryResultServiceDTO resultPriority4 = priority4(bulkQueryRequestDTO, queryData.QueryDetail, notIn);
                AllListResponseSaved = AllListResponseSaved.Concat(resultPriority4.list);
                counterPrioritys = counterPrioritys.Concat(resultPriority4.DictionaryQuantityUserPerList)
                       .GroupBy(x => x.Key)
                       .ToDictionary(x => x.Key, x => x.Sum(y => y.Value));

            }

            BulkQueryResponseDTO responseDTO = new BulkQueryResponseDTO();
            responseDTO.query = queryData.Query.MapTo<QueryDTO>();
            responseDTO.lists = AllListResponseSaved;

            if (counterPrioritys.Count > 0)
            {
                UpdateQuantityUserPerList(counterPrioritys, ref responseDTO);
            }
            responseDTO.ownLists = OwnList(bulkQueryRequestDTO).MapTo<List<OwnListBulkQueryResponseDTO>>();
            //if (FilesHelper.saveBulkQueryFile(responseDTO))
            await fileShare.FileUploadAsync<BulkQueryResponseDTO>(responseDTO, responseDTO.query.Id);
            return await Task.FromResult<BulkQueryResponseDTO>(responseDTO);

            //return null;

        }
        private void UpdateQuantityUserPerList(Dictionary<int, int> Count_Priority, ref BulkQueryResponseDTO responseDTO)
        {
            //Convert Dictionary to DataTable
            DataTable Quantitys = new DataTable();
            Quantitys.Columns.Add("Id", typeof(int));
            Quantitys.Columns.Add("Quantity", typeof(int));

            foreach (KeyValuePair<int, int> entry in Count_Priority)
            {
                DataRow row = Quantitys.NewRow();
                row["Id"] = entry.Key;
                row["Quantity"] = entry.Value;
                Quantitys.Rows.Add(row);

                //var item = responseDTO.query.QueryDetails.Find(item => item.Id == entry.Key);
                //item.ResultQuantity = entry.Value;
            }

            _ibulkQueryRepository.UpdateQuantityUserPerList(Quantitys);
        }

        public async Task<BulkQueryResponseDTO> getQuery(int QueryId)
        {
            return await _ibulkQueryRepository.getBulkQuery(QueryId, Session);
        }
        public DataTable validateThridsInFiles(DataTable dataTableThirdsInformation)
        {


            var filter = dataTableThirdsInformation.AsEnumerable().Where(row =>
            {
                string name = row[2].ToString().Trim();
                var quantityWords = name.Split(" ").Length;
                if (!String.IsNullOrEmpty(name) && quantityWords <= 2)
                    return true;

                return false;
            });
            DataTable invalidThirds = filter.Count() > 0 ? filter.CopyToDataTable() : new DataTable();
            return invalidThirds;
        }
        private DataSet DataSetPriorities(DataTable dataTableThirdsInformation)
        {

            try
            {
                DataSet dataSetThirdsInformation = new DataSet();
                DataTable dataSettPriority1 = dataTableThirdsInformation.AsEnumerable().Count(row => !String.IsNullOrEmpty(row.Field<String>("Nid")) &&
                                                        !String.IsNullOrEmpty(row.Field<String>("Name"))) > 0 ? dataTableThirdsInformation.AsEnumerable().Where
                                                        (row => !String.IsNullOrEmpty(row.Field<String>("Nid")) &&
                                                        !String.IsNullOrEmpty(row.Field<String>("Name")))
                                                        .CopyToDataTable() : new DataTable();
                DataTable dataSettPriority2 = dataTableThirdsInformation.AsEnumerable().Count(row => !String.IsNullOrEmpty(row.Field<String>("Nid"))) > 0 ? dataTableThirdsInformation.AsEnumerable().Where
                                                        (row => !String.IsNullOrEmpty(row.Field<String>("Nid"))).CopyToDataTable()
                                                        : new DataTable();
                DataTable dataSettPriority3 = dataTableThirdsInformation.AsEnumerable().Count(row => !String.IsNullOrEmpty(row.Field<String>("Name"))) > 0 ? dataTableThirdsInformation.AsEnumerable().Where
                                                        (row => !String.IsNullOrEmpty(row.Field<String>("Name")))
                                                        .CopyToDataTable() : new DataTable();

                dataSetThirdsInformation.Tables.Add(dataSettPriority1);
                dataSetThirdsInformation.Tables.Add(dataSettPriority2);
                dataSetThirdsInformation.Tables.Add(dataSettPriority3);
                dataSetThirdsInformation.Tables.Add(dataTableThirdsInformation);//original para la consulta en listas propias
                return dataSetThirdsInformation;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public QueryResultServiceDTO priority1(BulkQueryRequestDTO bulkQueryRequestDTO, List<QueryDetail> queryDetail)
        {
            var responseLists = _ibulkQueryRepository.Priority1(bulkQueryRequestDTO);
            var list = responseLists.MapTo<IEnumerable<ListsBulkQueryDTO>>();
            Dictionary<int, int> counterPrioritys = new Dictionary<int, int>();
            var tempDataSetPriority1 = bulkQueryRequestDTO.dataSetPriorities.Tables[0];
            for (int i = 0; i < tempDataSetPriority1.Rows.Count; i++)
            {
                object[] rowData = tempDataSetPriority1.Rows[i].ItemArray;
                string identification = rowData[1].ToString().Trim();
                string name = rowData[2].ToString().Trim();

                var queryDetailsFounded = queryDetail.Where(p => p.Identification?.Trim() == identification && p.Name?.Trim() == name).ToList();

                int QuantityPerRecord = list.Where(p => p.Document.Trim() == identification).ToList().Count();

                foreach (var item in queryDetailsFounded)
                {
                    if (!counterPrioritys.ContainsKey(item.Id))
                    {
                        counterPrioritys.Add(item.Id, QuantityPerRecord);
                    }
                }


            }
            return new QueryResultServiceDTO()
            {
                list = list,
                DictionaryQuantityUserPerList = counterPrioritys
            };
        }
        public QueryResultServiceDTO priority2(BulkQueryRequestDTO bulkQueryRequestDTO, List<QueryDetail> queryDetail, string notIn)
        {
            var responseLists = _ibulkQueryRepository.Priority2(bulkQueryRequestDTO, notIn);
            var list = responseLists.MapTo<IEnumerable<ListsBulkQueryDTO>>();
            Dictionary<int, int> counterPrioritys = new Dictionary<int, int>();
            var tempDataSetPriority2 = bulkQueryRequestDTO.dataSetPriorities.Tables[1];
            for (int i = 0; i < tempDataSetPriority2.Rows.Count; i++)
            {
                object[] rowData = tempDataSetPriority2.Rows[i].ItemArray;
                string identification = rowData[1].ToString().Trim();
                var queryDetailsFounded = queryDetail.Where(p => p.Identification?.Trim() == identification).ToList();

                int QuantityPerRecord = list.Where(p => p.Document.Trim() == identification).ToList().Count();

                foreach (var item in queryDetailsFounded)
                {
                    if (!counterPrioritys.ContainsKey(item.Id))
                    {
                        counterPrioritys.Add(item.Id, QuantityPerRecord);
                    }
                }


            }
            return new QueryResultServiceDTO()
            {
                list = list,
                DictionaryQuantityUserPerList = counterPrioritys
            };
        }
        public QueryResultServiceDTO priority3(BulkQueryRequestDTO bulkQueryRequestDTO, List<QueryDetail> queryDetail, string notIn)
        {
            var responseLists = _ibulkQueryRepository.Priority3(bulkQueryRequestDTO, notIn);
            var list = responseLists.MapTo<IEnumerable<ListsBulkQueryDTO>>();
            IEnumerable<ListsBulkQueryDTO> list_filtered = new List<ListsBulkQueryDTO>();
            Dictionary<int, int> counterPrioritys = new Dictionary<int, int>();
            var tempDataSetPriority3 = bulkQueryRequestDTO.dataSetPriorities.Tables[2];
            for (int i = 0; i < tempDataSetPriority3.Rows.Count; i++)
            {
                object[] rowData = tempDataSetPriority3.Rows[i].ItemArray;
                string name = rowData[2].ToString().Trim();
                string document = rowData[1].ToString().Trim();

                var queryDetailsFounded = queryDetail.Where(p => p.Name.Trim().Equals(name)).ToList();
                //castear los nombres en arreglos, ordenarlos alfanumericamente y comparar si son iguales
                int QuantityPerRecord = list.Count(p => p.Name.Trim().Split(" ").OrderBy(s => s).SequenceEqual(name.Split(" ").OrderBy(s => s)));
                var newList = list.Where(p =>
                {
                    if (p.Name.Trim().Split(" ").OrderBy(s => s).SequenceEqual(name.Split(" ").OrderBy(s => s)))
                    {
                        p.NameQuery = name;
                        p.IdentificationQuery = document;
                        return true;
                    }

                    return false;
                }).ToList();
                list_filtered = list_filtered.Union(newList);
                //int QuantityPerRecord = list.Where(p => p.Name.Trim().Equals(name)).ToList().Count();

                foreach (var item in queryDetailsFounded)
                {
                    if (!counterPrioritys.ContainsKey(item.Id))
                    {
                        counterPrioritys.Add(item.Id, QuantityPerRecord);
                    }
                }


            }
            return new QueryResultServiceDTO()
            {
                list = list_filtered,
                DictionaryQuantityUserPerList = counterPrioritys
            };
        }
        public QueryResultServiceDTO priority4(BulkQueryRequestDTO bulkQueryRequestDTO, List<QueryDetail> queryDetail, string notIn)
        {
            var responseLists = _ibulkQueryRepository.Priority4(bulkQueryRequestDTO, notIn);
            var list = responseLists.MapTo<IEnumerable<ListsBulkQueryDTO>>();
            IEnumerable<ListsBulkQueryDTO> list_filtered = new List<ListsBulkQueryDTO>();
            Dictionary<int, int> counterPrioritys = new Dictionary<int, int>();
            var tempDataSetPriority4 = bulkQueryRequestDTO.dataSetPriorities.Tables[2];
            for (int i = 0; i < tempDataSetPriority4.Rows.Count; i++)
            {
                object[] rowData = tempDataSetPriority4.Rows[i].ItemArray;
                string name = rowData[2].ToString().Trim();
                string document = rowData[1].ToString().Trim();
                var searchedNameWords = name.Split(' ');
                var queryDetailsFounded = queryDetail.Where(p => p.Name.Trim().Equals(name)).ToList();


                var newList = list.Where(p =>
                {
                    var nameWords = p.Name.Trim().Split(' ');
                    int initialQuantityWords = nameWords.Length + (bulkQueryRequestDTO.NWords != null && bulkQueryRequestDTO.NWords > 0 ? (int)bulkQueryRequestDTO.NWords : 0);

                    if (searchedNameWords.All(word => nameWords.Contains(word)) && searchedNameWords.Length <= nameWords.Length)
                    {
                        p.NameQuery = name;
                        p.IdentificationQuery = document;
                        return true;
                    }

                    return false;
                }).ToList();

                int QuantityPerRecord = newList.Count(p =>
                {
                    var nameWords = p.Name.Trim().Split(' ');
                    int initialQuantityWords = searchedNameWords.Length + (bulkQueryRequestDTO.NWords != null && bulkQueryRequestDTO.NWords > 0 ? (int)bulkQueryRequestDTO.NWords : 0);
                    return searchedNameWords.All(word => nameWords.Contains(word));
                });
                list_filtered = list_filtered.Union(newList);
                foreach (var item in queryDetailsFounded)
                {
                    if (!counterPrioritys.ContainsKey(item.Id))
                    {
                        counterPrioritys.Add(item.Id, QuantityPerRecord);
                    }
                }


            }
            return new QueryResultServiceDTO()
            {
                list = list_filtered,
                DictionaryQuantityUserPerList = counterPrioritys
            };
        }

        public IEnumerable<OwnListBulkQueryResponseDTO> OwnList(BulkQueryRequestDTO bulkQueryRequestDTO)
        {

            IEnumerable<OwnListResponse> lists = _ibulkQueryRepository.OwnLists(bulkQueryRequestDTO.dataSetPriorities.Tables[3], bulkQueryRequestDTO.CompanyId.ToString());

            var listsDTO = lists.MapTo<List<OwnListBulkQueryResponseDTO>>();
            return listsDTO;
        }

        public static string stringNotIn(IEnumerable<ListsBulkQueryDTO> list)
        {
            string stringResponse = "";
            if (list.Count() > 0)
            {
                int i = 0;
                foreach (ListsBulkQueryDTO obj in list)
                {
                    i++;
                    stringResponse += obj.Id + (i < list.Count() ? ", " : "");
                }
            }
            return stringResponse.Trim();
        }

    }
}
