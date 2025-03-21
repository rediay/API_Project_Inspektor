using Common.DTO;
using Common.DTO.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.Services.Users
{
    public interface IUserManagementService
    {
        Task<PagedResponseDTO<List<UserManagementDto>>> GetAll(PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);
        Task<ResponseDTO<UserManagementDto>> GetById(int id, bool includeDeleted = false);
        Task<ResponseDTO<UserManagementDto>> Edit(UserManagementDto dto);
        Task<ResponseDTO<UserManagementDto>> ResetPassword(UserManagementDto dto);
        Task<ResponseDTO<UserManagementDto>> Create(UserManagementDto dto);
        Task<ResponseDTO<UserManagementDto>> CreateByAdmin(UserManagementDto dto);
        Task<bool> Delete(int id);
        Task<ResponseDTO<List<UserManagementDto>>> GetAllByCompanyId(int CompanyID, bool includeDeleted = false);
    }
}