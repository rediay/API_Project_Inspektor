using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure.Repositories.ThirdPartyProfiling;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories.ThirdPartyProfiling
{
    public class CategoryVariableRepository : BaseRepository<CategoryVariable, DataContext>, ICategoryVariableRepository
    {
        public CategoryVariableRepository(DataContext context) : base(context)
        {
        }

        public async Task<PagedResponseDTO<List<CategoryVariable>>> GetAll(ContextSession session,
            PaginationFilterDTO paginationFilterDto, bool includeDeleted = false)
        {
            var totalSkipped = (paginationFilterDto.PageNumber - 1) * paginationFilterDto.PerPage;

            var queryEntities = GetEntities(session)
                .Where(obj => obj.Name.Contains(paginationFilterDto.query));

            var list = await queryEntities
                .Skip(totalSkipped)
                .Take(paginationFilterDto.PerPage)
                .ToListAsync();

            var total = await queryEntities.CountAsync();
            var pageNumber = paginationFilterDto.PageNumber;
            var perPage = paginationFilterDto.PerPage;

            var pagedResponseDto = new PagedResponseDTO<List<CategoryVariable>>(list, pageNumber, perPage, total);

            return pagedResponseDto;
        }

        public async Task<PagedResponseDTO<List<CategoryVariable>>> GetAllCategoryVariablesByRiskProfileVariableId(
            ContextSession session, PaginationFilterDTO paginationFilterDto, int riskProfileVariableId,
            int personTypeId, bool includeDeleted = false)
        {
            var totalSkipped = (paginationFilterDto.PageNumber - 1) * paginationFilterDto.PerPage;

            var queryEntities = GetEntities(session)
                .Include(x => x.RiskProfileVariable)
                .Where(obj => obj.RiskProfileVariableId == riskProfileVariableId)
                .Where(obj => obj.PersonTypeId == personTypeId)
                .Where(obj => obj.Name.Contains(paginationFilterDto.query));

            var list = await queryEntities
                .Skip(totalSkipped)
                .Take(paginationFilterDto.PerPage)
                .ToListAsync();

            var total = await queryEntities.CountAsync();
            var pageNumber = paginationFilterDto.PageNumber;
            var perPage = paginationFilterDto.PerPage;

            var pagedResponseDto = new PagedResponseDTO<List<CategoryVariable>>(list, pageNumber, perPage, total);

            return pagedResponseDto;
        }

        public async Task<CategoryVariable> Get(int id, ContextSession session, bool includeDeleted = false)
        {
            return await GetEntities(session).Where(obj => obj.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<CategoryVariable>> Get(ContextSession session, int companyId, List<int> categoryIds,
            bool includeDeleted = false)
        {
            var categories = await GetEntities(session)
                .Where(obj => categoryIds.Contains(obj.Id))
                //.Where(obj => obj.CompanyId == companyId)
                .Include(p => p.RiskProfileVariable)
                .Where(p => p.RiskProfileVariable.CompanyId == companyId)
                .ToListAsync();
            return categories;
        }
    }
}