using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure.Repositories.ThirdPartyProfiling;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories.ThirdPartyProfiling
{
    public class RiskProfileVariableRepository : BaseRepository<RiskProfileVariable, DataContext>,
        IRiskProfileVariableRepository
    {
        public RiskProfileVariableRepository(DataContext context) : base(context)
        {
        }

        public async Task<PagedResponseDTO<List<RiskProfileVariable>>> GetAll(ContextSession session,
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

            var pagedResponseDto = new PagedResponseDTO<List<RiskProfileVariable>>(list, pageNumber, perPage, total);

            return pagedResponseDto;
        }

        public async Task<RiskProfileVariable> Get(int id, ContextSession session, bool includeDeleted = false)
        {
            return await GetEntities(session).Where(obj => obj.Id == id).FirstOrDefaultAsync();
        }

        public Task<float> GetTotalWeight(ContextSession session, int? variableId = null)
        {
            if (variableId != null)
            {
                return GetEntities(session).Where(variable => variable.Id != variableId)
                    .SumAsync(variable => variable.Weight);
            }

            return GetEntities(session).SumAsync(variable => variable.Weight);
        }

        public async Task<List<RiskProfileVariable>> GetAll(ContextSession session, int companyId,
            bool includeDeleted = false)
        {
            var riskProfileVariable = await GetEntities(session)
                .Where(p => p.CompanyId == companyId)
                .ToListAsync();
            return riskProfileVariable;
        }
    }
}