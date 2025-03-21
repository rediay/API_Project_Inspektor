using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories
{
    public class ListPeriodicityRepository : BaseRepository<Periodicity, DataContext>, IListPeriodicityRepository
    {
        public ListPeriodicityRepository(DataContext context) : base(context)
        {
        }
        
        public async Task<PagedResponseDTO<List<Periodicity>>> GetAll(ContextSession session,
            PaginationFilterDTO paginationFilterDto, bool includeDeleted = false)
        {
            var totalSkipped = (paginationFilterDto.PageNumber - 1) * paginationFilterDto.PerPage;

            var queryEntities = GetEntities(session)
                .Where(obj => obj.Name.Contains(paginationFilterDto.query));

            var newsList = await queryEntities
                .Skip(totalSkipped)
                .Take(paginationFilterDto.PerPage)
                .ToListAsync();

            var total = await queryEntities.CountAsync();
            var pageNumber = paginationFilterDto.PageNumber;
            var perPage = paginationFilterDto.PerPage;

            var pagedResponseDto = new PagedResponseDTO<List<Periodicity>>(newsList, pageNumber, perPage, total);

            return pagedResponseDto;
        }
    }
}