using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Common.Services.Infrastructure.Repositories.Relations_Countrys;
using Common.Entities.Relations_Countrys;
using System;

namespace Common.DataAccess.EFCore.Repositories
{
    public class StatesRepository : IStatesRepository
    {
        private DataContext _dataContext;
        public StatesRepository(DataContext context)
        {
            _dataContext = context;
        }
        
        public async Task<PagedResponseDTO<List<States>>> GetAll(ContextSession session,
            PaginationFilterDTO paginationFilterDto, bool includeDeleted = false)
        {
            var totalSkipped = (paginationFilterDto.PageNumber - 1) * paginationFilterDto.PerPage;

            var queryEntities = _dataContext.States
                .Where(obj => obj.Name.Contains(paginationFilterDto.query));

            var total = await queryEntities.CountAsync();

            var newsList = await queryEntities
              .Skip(totalSkipped)
              .Take(paginationFilterDto.PerPage)
              .ToListAsync(); // Materializa solo la página actual de resultados

            var pageNumber = paginationFilterDto.PageNumber;
            var perPage = paginationFilterDto.PerPage;


            var pagedResponseDto = new PagedResponseDTO<List<States>>(newsList, pageNumber, perPage, total);

            return pagedResponseDto;
        }

        public async Task<IEnumerable<States>>GetStatesById(int idcountry ,ContextSession session)
        {
            var states = await _dataContext.States.Where(x => x.CountryId == idcountry).ToListAsync();
            return states;
        }
        
        public async Task<bool> BulkCreate(List<States> lists, ContextSession session)
        {
            
            await _dataContext.States.AddRangeAsync(lists);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<States> Edit(States states, ContextSession session)
        {
            var state = await _dataContext.States.FirstOrDefaultAsync(x => x.Id == states.Id);
            if (state == null)
            {
                return null;
            }
            else
            {
                states.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                states.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                _dataContext.Entry(state).CurrentValues.SetValues(states);

                _dataContext.SaveChanges();
                return state;
            }
        }

        public async Task<bool> Delete(int id, ContextSession session)
        {
            var state = await _dataContext.States.FirstOrDefaultAsync(x => x.Id == id);
            if (state != null)
            {
                _dataContext.States.Remove(state);
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