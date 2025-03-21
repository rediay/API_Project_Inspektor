using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.Users;
using Common.Entities;
using Common.Services.Infrastructure.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories.Users
{
    public class UserManagementRepository : BaseLoggableRepository<User, DataContext>, IUserManagementRepository<User>
    {
        public UserManagementRepository(DataContext context) : base(context)
        {
        }

        public async Task<PagedResponseDTO<List<User>>> GetAll(ContextSession session,
            PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false)
        {
            var userId = session.UserId;
            var currentUSer = await Get(userId, session);
            var totalSkipped = (paginationFilterDto.PageNumber - 1) * paginationFilterDto.PerPage;

            var queryEntities = GetEntities(session)
                .Where(obj => obj.CompanyId == currentUSer.CompanyId)
                .Where(obj => obj.Name.Contains(paginationFilterDto.query)
                              || obj.LastName.Contains(paginationFilterDto.query)
                              || obj.Email.Contains(paginationFilterDto.query)
                              || obj.Identification.Contains(paginationFilterDto.query)
                );

            var userList = await queryEntities
                .Skip(totalSkipped)
                .Take(paginationFilterDto.PerPage)
                .ToListAsync();

            var total = await queryEntities.CountAsync();
            var pageNumber = paginationFilterDto.PageNumber;
            var perPage = paginationFilterDto.PerPage;

            var pagedResponseDto = new PagedResponseDTO<List<User>>(userList, pageNumber, perPage, total);

            return pagedResponseDto;
        }
        public async Task<List<User>> GetAllByCompanyId(int CompanyId, ContextSession session,
           bool includeDeleted = false)
        {
            var userId = session.UserId;
            var currentUSer = await Get(userId, session);
            if (currentUSer.CompanyId == CompanyId)
            {
                var context = GetContext(session);
                var result = GetEntities(session)
                    .Where(obj => obj.CompanyId == CompanyId)
                    .Include(x => x.Company);
                return await result.ToListAsync();
            }
            return null;
        }

        public async Task<User> GetExistingUser(UserManagementDto dto, ContextSession session, bool includeExitingDto = true)
        {
            if (!includeExitingDto)
            {
                return await GetEntities(session)
                    .Where(obj => obj.Email == dto.Email
                                  || obj.Identification == dto.Identification
                                  || obj.Login == dto.Login)
                    .Where(obj => obj.Id != dto.Id)
                    .FirstOrDefaultAsync();
            }

            return await GetEntities(session)
                .Where(obj => obj.Email == dto.Email
                              || obj.Identification == dto.Identification
                              || obj.Login == dto.Login)
                .FirstOrDefaultAsync();

            // if (!includeExitingDto)
            //     query.Where(obj => obj.Id != dto.Id);

            // query.Where(obj => obj.Email == dto.Email
            //                    || obj.Identification == dto.Identification
            //                    || obj.Login == dto.Login
            // );
            // query.Where(obj => obj.Email == dto.Email);

            // return await ;
        }
    }
}