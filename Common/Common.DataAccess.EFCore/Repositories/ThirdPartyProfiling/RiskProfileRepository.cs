using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure.Repositories.ThirdPartyProfiling;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories.ThirdPartyProfiling
{
    public class RiskProfileRepository : BaseRepository<RiskProfile, DataContext>, IRiskProfileRepository
    {
        public RiskProfileRepository(DataContext context) : base(context)
        {
        }

        public async Task<PagedResponseDTO<List<RiskProfile>>> GetAll(ContextSession session,
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

            var pagedResponseDto = new PagedResponseDTO<List<RiskProfile>>(list, pageNumber, perPage, total);

            return pagedResponseDto;
        }

        public async Task<RiskProfile> Get(int id, ContextSession session, bool includeDeleted = false)
        {
            return await GetEntities(session).Where(obj => obj.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<RiskProfile>> GetAll(ContextSession session, int companyId, bool includeDeleted = false)
        {
            var riskProfiles = await GetEntities(session)
                .Where(p => p.CompanyId == companyId)
                .ToListAsync();
            return riskProfiles;
        }
    }
}