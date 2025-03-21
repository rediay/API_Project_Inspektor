using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.Users;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories.Users
{
    public interface IUserManagementRepository<TUser> where TUser : User
    {
        Task<PagedResponseDTO<List<TUser>>> GetAll(ContextSession session, PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);
        Task<TUser> Get(int id, ContextSession session, bool includeDeleted = false);
        Task<TUser> GetExistingUser(UserManagementDto dto, ContextSession session, bool includeExitingDto);
        Task<TUser> Edit(TUser user, ContextSession session);
        Task Delete(int id, ContextSession session);
        Task<List<TUser>> GetAllByCompanyId(int CompanyId, ContextSession session, bool includeDeleted = false);
    }
}