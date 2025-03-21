/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DTO;
using Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.Queries
{
    public interface IDocumentRepository
    {
    
        Task<NombreCedula> GetNameByDocument(string document, ContextSession session);
        Task<List<NombreCedula>> GetDocument(ContentPaginationFilterDTO paginationFilterDto, ContextSession session);
        Task<NombreCedula> createDocument(NombreCedulaDTO nombreCedulaDTO, ContextSession session);
        Task<NombreCedula> editDocument(NombreCedulaDTO document, ContextSession session);
        Task<bool> deleteDocument(int id, ContextSession session);
    }
}