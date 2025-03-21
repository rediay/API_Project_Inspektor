using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories
{
    public class DocumentTypeRepository : BaseRepository<DocumentType, DataContext>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<DocumentType>> GetAll(ContextSession session, bool includeDeleted = false)
        {
            return await GetEntities(session).ToListAsync();
        }

        public async Task<DocumentType> Get(int id, ContextSession session, bool includeDeleted = false)
        {
            return await GetEntities(session)
                .Where(obj => obj.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}