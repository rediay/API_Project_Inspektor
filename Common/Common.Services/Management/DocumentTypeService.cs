/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Management;
using Common.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Services.Management
{
    public class DocumentTypeService<TDocumentType> : BaseService, IDocumentTypeService where TDocumentType : DocumentType, new()
    {
        protected readonly IDocumentTypeRepository<TDocumentType> _DocumentTypeRepository;

        public DocumentTypeService(ICurrentContextProvider contextProvider, IDocumentTypeRepository<TDocumentType> DocumentTypeRepository) : base(contextProvider)
        {
            this._DocumentTypeRepository = DocumentTypeRepository;
        }

        public async Task<bool> Delete(int id)
        {
            await _DocumentTypeRepository.Delete(id, Session);
            return true;
        }

        public async Task<DocumentTypeDTO> Edit(DocumentTypeDTO dto)
        {
            var DocumentType = dto.MapTo<TDocumentType>();
            var result = await _DocumentTypeRepository.Edit(DocumentType, Session);

            return result.MapTo<DocumentTypeDTO>();
        }

        public async Task<ICollection<DocumentTypeDTO>> Get()
        {
            var ls = await _DocumentTypeRepository.Get(Session);
            return ls.MapTo<ICollection<DocumentTypeDTO>>();
        }

        public async Task<DocumentTypeDTO> GetById(int id)
        {
            var user = await _DocumentTypeRepository.GetById(id, Session);
            return user.MapTo<DocumentTypeDTO>();
        }

     


    }
}
