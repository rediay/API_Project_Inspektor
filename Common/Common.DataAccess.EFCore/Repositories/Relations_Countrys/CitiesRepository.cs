using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Microsoft.EntityFrameworkCore;
using Common.Services.Infrastructure.Repositories.Relations_Countrys;
using Common.Entities;
using Common.Entities.Relations_Countrys;

namespace Common.DataAccess.EFCore.Repositories
{
    public class CitiesRepository : ICitiesRepository
    {
        private DataContext _dataContext;
        public CitiesRepository(DataContext context)
        {
            _dataContext = context;
        }
        
        public async Task<PagedResponseDTO<List<Entities.Relations_Countrys.Cities>>> GetAll(ContextSession session,
            PaginationFilterDTO paginationFilterDto, bool includeDeleted = false)
        {
            var totalSkipped = (paginationFilterDto.PageNumber - 1) * paginationFilterDto.PerPage;

            var queryEntities = _dataContext.Cities
                .Where(obj => obj.Name.Contains(paginationFilterDto.query));

            var total = await queryEntities.CountAsync();

            var newsList = await queryEntities
              .Skip(totalSkipped)
              .Take(paginationFilterDto.PerPage)
              .ToListAsync(); // Materializa solo la página actual de resultados

            var pageNumber = paginationFilterDto.PageNumber;
            var perPage = paginationFilterDto.PerPage;


            var pagedResponseDto = new PagedResponseDTO<List<Entities.Relations_Countrys.Cities>>(newsList, pageNumber, perPage, total);

            return pagedResponseDto;
        }

        public async Task<IEnumerable<Entities.Relations_Countrys.Cities>> GetCitiesbyId(int idcountry ,int stateid,ContextSession session)
        {
            var cities = await _dataContext.Cities.Where(x => x.CountryId == idcountry && x.StateId == stateid).ToListAsync();
            return cities;
        }
        
        public async Task<bool> BulkCreate(List<Entities.Relations_Countrys.Cities> lists, ContextSession session)
        {
            await _dataContext.Cities.AddRangeAsync(lists);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<Entities.Relations_Countrys.Cities> Edit(Entities.Relations_Countrys.Cities states, ContextSession session)
        {
            var state = await _dataContext.Cities.FirstOrDefaultAsync(x => x.Id == states.Id);
            if (state == null)
            {
                return null;
            }
            else
            {
                _dataContext.Entry(state).CurrentValues.SetValues(states);

                await _dataContext.SaveChangesAsync();
                return state;
            }
        }

        public async Task<bool> Delete(int id, ContextSession session)
        {
            var state = await _dataContext.Cities.FirstOrDefaultAsync(x => x.Id == id);
            if (state != null)
            {
                _dataContext.Cities.Remove(state);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}