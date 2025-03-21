using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure.Repositories.RestrictiveLists;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories.RestrictiveLists
{
    public class ListTypeRepository : BaseSoftDeletableRepository<ListType, DataContext>, IListTypeRepository
    {
        public ListTypeRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<ListType>> GetAll(ContextSession session, bool includeDeleted = false)
        {
            return await GetEntities(session).ToListAsync();
        }

        public async Task<PagedResponseDTO<List<ListType>>> GetAll(ContextSession session,
            PaginationFilterDTO paginationFilterDto, bool includeDeleted = false)
        {
            var totalSkipped = (paginationFilterDto.PageNumber - 1) * paginationFilterDto.PerPage;

            var queryEntities = GetEntities(session)
                .Where(obj => obj.Name.Contains(paginationFilterDto.query)
                              || obj.Description.Contains(paginationFilterDto.query)
                )
                .Include(p => p.ListGroup)
                .Include(p => p.Periodicity);

            var newsList = await queryEntities
                .Skip(totalSkipped)
                .Take(paginationFilterDto.PerPage)
                .ToListAsync();

            var total = await queryEntities.CountAsync();
            var pageNumber = paginationFilterDto.PageNumber;
            var perPage = paginationFilterDto.PerPage;

            var pagedResponseDto = new PagedResponseDTO<List<ListType>>(newsList, pageNumber, perPage, total);

            return pagedResponseDto;
        }

        public async Task<ListType> Get(int id, ContextSession session, bool includeDeleted = false)
        {
            return await GetEntities(session)
                .Where(obj => obj.Id == id)
                .Include(p => p.ListGroup)
                .Include(p => p.Country)
                .Include(p => p.Periodicity)
                .FirstOrDefaultAsync();
        }
    }
}