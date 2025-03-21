using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.Services
{
    public interface IDocumentTypeService
    {
        Task<ResponseDTO<List<DocumentType>>> GetAll(bool includeDeleted = false);
        Task<ResponseDTO<DocumentType>> GetById(int id, bool includeDeleted = false);
    }
}