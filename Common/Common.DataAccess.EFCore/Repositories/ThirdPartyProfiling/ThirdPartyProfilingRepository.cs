using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure.Repositories.ThirdPartyProfiling;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories.ThirdPartyProfiling
{
    public class ThirdPartyProfilingRepository : BaseRepository<Entities.ThirdPartyProfiling, DataContext>, IThirdPartyProfilingRepository
    {
        public ThirdPartyProfilingRepository(DataContext context) : base(context)
        {
        }

        public async Task<PagedResponseDTO<List<Entities.ThirdPartyProfiling>>> GetAll(ContextSession session, PaginationFilterDTO paginationFilterDto, bool includeDeleted = false)
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

            var pagedResponseDto = new PagedResponseDTO<List<Entities.ThirdPartyProfiling>>(list, pageNumber, perPage, total);

            return pagedResponseDto;
        }

        public async Task<bool> Import(ContextSession session, List<Entities.ThirdPartyProfiling> items)
        {
            var context = GetContext(session);
            // await context.AddAsync(items);
            await context.AddRangeAsync(items);
            //context.ThirdPartyProfiling.AddAsync(items);
            return await context.SaveChangesAsync() > 0;
        }
    }
}