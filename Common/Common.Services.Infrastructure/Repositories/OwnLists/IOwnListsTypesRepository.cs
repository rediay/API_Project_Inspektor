using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories.OwnLists
{
    public interface IOwnListsTypesRepository
    {
        Task<List<OwnListType>> GetOwnListTypes(int companyId, ContextSession session);
        Task<bool> UpdateOwnListType(OwnListType ownListType, ContextSession session);
        Task<bool> CreateOwnListType(OwnListType ownListType, ContextSession session);
        Task<bool> DeleteOwnListType(int  id, ContextSession session);
        Task<bool> ImportOwnLists(List<OwnList> ownLists, ContextSession session);
        Task<bool> DeleteImportedOwnListsByType(int ownListTypeId, ContextSession session);
    }
}