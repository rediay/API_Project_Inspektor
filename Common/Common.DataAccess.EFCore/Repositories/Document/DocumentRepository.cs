/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Common.DataAccess.EFCore.Repositories.Queries;
using Common.DataAccess.EFCore.Repositories.RequestHelper;
using Common.DTO;
using Common.DTO.RestrictiveLists;
using Common.Entities;
using Common.Entities.SPsData;
using Common.Entities.SPsData.AditionalServices;
using Common.Entities.SPsData.AditionalServices.Procuraduria;
using Common.Entities.SPsData.AditionalServices.RamaJudicial;
using Common.Entities.SPsData.AditionalServices.Rues;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Queries;
using Common.Utils;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.SS.Formula.Functions;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Common.DataAccess.EFCore.Repositories.Document
{
    public class DocumentRepository : IDocumentRepository
    {
        DataDocumentsContext context;
        IConfiguration configuration;
        private readonly RequestRepository requestRepository;
        public DocumentRepository(DataDocumentsContext context, IConfiguration configuration, RequestRepository requestRepository)
        {

            this.context = context;
            this.configuration = configuration;
            this.requestRepository = requestRepository;
        }

        public Task<NombreCedula> createDocument(NombreCedulaDTO nombreCedulaDTO, ContextSession session)
        {
            var document =nombreCedulaDTO.MapTo<NombreCedula>();
            //document.TipoIden = 1;
            document.IdUsuario = session.UserId.ToString();
            document.FechaReg = DateTime.Today;
            //document.Fuente = "Inspektor II";
            context.Add<NombreCedula>(document);
            context.SaveChanges();
            return Task.FromResult(document);
        }

        public Task<List<NombreCedula>> GetDocument(ContentPaginationFilterDTO paginationFilterDto, ContextSession session)
        {
            List<NombreCedula> lists;
            Expression<Func<NombreCedula, bool>> whereExpression = null;

            //var currentDocuments = context.NombreCedula;
            var query = context.NombreCedula.AsNoTracking();
            //lists = context.NombreCedula.Where(d => { d.DocumentoIdentidad == paginationFilterDto.Document}).ToList();
            if (paginationFilterDto.Name != null)
                query = query.Where(d => d.NombreCompleto == paginationFilterDto.Name);
            if (paginationFilterDto.Document != null)
                query = query.Where(d => d.DocumentoIdentidad == paginationFilterDto.Document);
            if (paginationFilterDto.Date != null)
                query = query.Where(d => d.FechaReg == paginationFilterDto.Date);
            lists = query.ToList();
            return Task.FromResult(lists);   
        }
        public async Task<NombreCedula> GetNameByDocument(string document, ContextSession session)
        {
            //var userId = session.UserId;
            var webserviceConfig = configuration.GetSection("webservicesOptions");
            var currentDocument = context.NombreCedula.Where(d => d.DocumentoIdentidad == document).First();
            if (currentDocument == null)
            {
                NombreCedula newName = new NombreCedula()
                {
                    DocumentoIdentidad = document,
                    TipoIden = 1,
                    FechaReg = DateTime.Now,
                    Fuente = "r-col",
                    IdUsuario = "1",
                };
                cXsHttpResponse<Procuraduria> procuraduriaResponse = null;
                Task<cXsHttpResponse<Procuraduria>> procuraduriaThread = Task.FromResult<cXsHttpResponse<Procuraduria>>(procuraduriaResponse);

                procuraduriaThread = requestRepository.makeProcuraduriaRequest(document,1);
                await Task.WhenAll(procuraduriaThread);
                Procuraduria procuraduria = procuraduriaThread.Result.HasError ? new Procuraduria() :procuraduriaThread.Result.Data;
                if (procuraduria.not_criminal_records != null)
                {
                    newName.NombreCompleto = procuraduria.not_criminal_records.name;
                    context.NombreCedula.Add(newName);
                    context.SaveChangesAsync();
                    currentDocument = newName;
                }
                else if (procuraduria.html_response != null)
                {
                    string strNameProcuraduria = procuraduria.html_response;
                    if (strNameProcuraduria.Contains("Señor(a)"))
                    {
                        //strInfoInspektor = strInfoInspektor.Substring(0, strInfoInspektor.IndexOf("#No:"));
                        strNameProcuraduria = strNameProcuraduria.Substring(strNameProcuraduria.IndexOf("Señor(a)") + 10, strNameProcuraduria.IndexOf("identificado") - 12);
                        strNameProcuraduria = strNameProcuraduria.Substring(0, strNameProcuraduria.IndexOf("identificado") - 9);
                        strNameProcuraduria = strNameProcuraduria.Replace("<span>", "").Replace("</span>", " ");
                        if (strNameProcuraduria != "NA" && !strNameProcuraduria.Contains("-1Se han producido uno o varios errores."))
                        {
                            newName.NombreCompleto = strNameProcuraduria;
                            context.NombreCedula.Add(newName);
                            context.SaveChangesAsync();
                            currentDocument = newName;
                        }
                    }
                }
            }
            return await Task.FromResult<NombreCedula>(currentDocument);
        }

        public Task<NombreCedula> editDocument(NombreCedulaDTO document, ContextSession session)
        {
            NombreCedula documentModified = context.NombreCedula.Find(document.IdCliente);

            
             //NombreCedula  toModifyDocument = document.MapTo<NombreCedula>();
            
            documentModified.NombreCompleto = document.NombreCompleto;
            documentModified.DocumentoIdentidad = document.DocumentoIdentidad;
            documentModified.Fuente = document.Fuente;
            documentModified.TipoIden = document.TipoIden;
            context.SaveChanges();

            return Task.FromResult(documentModified); ;
        }

        public async Task<bool> deleteDocument(int id, ContextSession session)
        {

            var document = context.NombreCedula.Find(id);
            if (document == null) return false;
            else
                context.NombreCedula.Remove(document);

            return await context.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
