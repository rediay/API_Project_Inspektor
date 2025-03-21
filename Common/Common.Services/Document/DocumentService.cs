/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using AutoMapper;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Document;
using Common.Services.Infrastructure.Queries;
using Common.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Services.Document
{
    public class DocumentService : BaseService, IDocumentService
    {
        protected readonly IDocumentRepository documentRepository;
        private readonly IMapper _mapper;
        public DocumentService(ICurrentContextProvider contextProvider, IDocumentRepository documentRepository, IMapper mapper) : base(contextProvider)
        {
            this.documentRepository = documentRepository;
            _mapper = mapper;
        }

        public async Task<NombreCedulaDTO> Create(NombreCedulaDTO nombreCedulaDTO)
        {
            var documentResult = await documentRepository.createDocument(nombreCedulaDTO, Session);
            return documentResult.MapTo<NombreCedulaDTO>();
        }

        public async Task<NombreCedulaDTO> Edit(NombreCedulaDTO nombreCedulaDTO)
        {
            var documentResult = await documentRepository.editDocument(nombreCedulaDTO, Session);
            return documentResult.MapTo<NombreCedulaDTO>();
        }

        public async Task<List<NombreCedulaDTO>> GetDocument(ContentPaginationFilterDTO paginationFilterDto)
        {
            var documents = await documentRepository.GetDocument(paginationFilterDto, Session);
            return documents.MapTo<List<NombreCedulaDTO>>();
        }

        public async Task<bool> DeleteDocument(int id)
        {
            return await documentRepository.deleteDocument(id, Session);
        }
        public async Task<NombreCedulaDTO> GetNameByDocument(string document)
        {
            var documentResult = await documentRepository.GetNameByDocument(document, Session);
            return documentResult.MapTo<NombreCedulaDTO>();
        }

      



        //public async Task<NotificationSettingsDTO> Edit(NotificationSettingsDTO dto)
        //{
        //    var settings = dto.MapTo<NotificationSettings>();
        //    await notificationSettingsRepository.Edit(settings, Session);
        //    return settings.MapTo<NotificationSettingsDTO>();
        //}

        //public async Task<NotificationSettingsDTO> GetByCompanyId(int CompanyId)
        //{

        //    var settings = await notificationSettingsRepository.GetByCompanyId(CompanyId, Session);
        //    var map = settings.MapTo<NotificationSettingsDTO>();
        //    return map;
        //}


    }
}
