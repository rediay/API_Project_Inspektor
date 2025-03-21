using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories.OwnLists
{
    public interface IOwnListsRepository
    {
        Task<List<OwnList>> GetOwnLists(int companyId, ContextSession session);
        Task<bool> UpdateOwnList(OwnList ownList, ContextSession session);
        Task<bool> CreateOwnList(OwnList ownListType, ContextSession session);
        Task<bool> DeleteOwnList(int  id, ContextSession session);
    }
}