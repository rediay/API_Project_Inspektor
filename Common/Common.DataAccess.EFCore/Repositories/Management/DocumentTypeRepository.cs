/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using Common.Services.Infrastructure.Management;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.DataAccess.EFCore.Repositories.Management
{
    public class DocumentTypeRepository : BaseRepository<DocumentType, DataContext>, IDocumentTypeRepository<DocumentType>
    {
        public DocumentTypeRepository(DataContext context) : base(context) { }

        public override async Task<bool> Exists(DocumentType obj, ContextSession session)
        {
            var context = GetContext(session);
            return await context.DocumentTypes.Where(x => x.Id == obj.Id).AsNoTracking().CountAsync() > 0;
        }

        public async Task<ICollection<DocumentType>> Get(ContextSession session)
        {
            var context = GetContext(session);
            return await context.DocumentTypes.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<DocumentType> GetById(int id, ContextSession session)
        {
            var context = GetContext(session);
            return await context.DocumentTypes.Where(obj => obj.Id == id)                                        
                                        .AsNoTracking().FirstOrDefaultAsync();
        }

  
        public override Task<DocumentType> Edit(DocumentType obj, ContextSession session)
        {
            var context = GetContext(session);            
            return base.Edit(obj, session);
        }


    }
}
