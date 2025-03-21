using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories
{
    public interface IDocumentTypeRepository
    {
        Task<List<DocumentType>> GetAll(ContextSession session,
            bool includeDeleted = false);
        Task<DocumentType> Get(int id, ContextSession session, bool includeDeleted = false);
    }
}