using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories;
using Common.Services.Infrastructure.Services;

namespace Common.Services
{
    public class DocumentTypeService : BaseService, IDocumentTypeService
    {
        private readonly IDocumentTypeRepository _documentTypeRepository;

        public DocumentTypeService(ICurrentContextProvider contextProvider, IDocumentTypeRepository documentTypeRepository) : base(contextProvider)
        {
            _documentTypeRepository = documentTypeRepository;
        }

        public async Task<ResponseDTO<List<DocumentType>>> GetAll(bool includeDeleted = false)
        {
            var records = await _documentTypeRepository.GetAll(Session, includeDeleted);
            var response = new ResponseDTO<List<DocumentType>>(records);
            return response;
        }

        public async Task<ResponseDTO<DocumentType>> GetById(int id, bool includeDeleted = false)
        {
            var result = await _documentTypeRepository.Get(id, Session, includeDeleted);

            if (result == null)
            {
                return new ResponseDTO<DocumentType>(null) {Succeeded = false};
            }

            var response = new ResponseDTO<DocumentType>(result);

            return response;
        }
    }
}