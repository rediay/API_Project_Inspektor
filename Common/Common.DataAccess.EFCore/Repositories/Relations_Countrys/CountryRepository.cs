using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Common.Entities.Relations_Countrys;
using Common.Services.Infrastructure.Repositories.Relations_Countrys;
using Common.Services.Infrastructure.Repositories;
using System;
using DocumentFormat.OpenXml.InkML;

namespace Common.DataAccess.EFCore.Repositories.Cities
{
    public class CountryRepository : BaseSoftDeletableRepository<Countries, DataContext>, ICountryRepository
    {
        public CountryRepository(DataContext context) : base(context)
        {

        }

        public async Task<PagedResponseDTO<List<Countries>>> GetAll(ContextSession session,
            PaginationFilterDTO paginationFilterDto, bool includeDeleted = false)
        {
            var totalSkipped = (paginationFilterDto.PageNumber - 1) * paginationFilterDto.PerPage;

            var queryEntities = GetEntities(session)
                .Where(obj => obj.Name.Contains(paginationFilterDto.query));

            var total = await queryEntities.CountAsync();

            var newsList = await queryEntities
              .Skip(totalSkipped)
              .Take(paginationFilterDto.PerPage)
              .ToListAsync(); // Materializa solo la página actual de resultados

            var pageNumber = paginationFilterDto.PageNumber;
            var perPage = paginationFilterDto.PerPage;


            var pagedResponseDto = new PagedResponseDTO<List<Countries>>(newsList, pageNumber, perPage, total);

            return pagedResponseDto;
        }

        public async Task<IEnumerable<Countries>> GetCountries(ContextSession session)
        {
            var countries = await GetEntities(session).ToListAsync();
            return countries;
        }

        public async Task<bool> BulkCreate(List<Countries> lists, ContextSession session)
        {
            var context = GetContext(session);
            await context.AddRangeAsync(lists);
            await context.SaveChangesAsync();
            return true;
        }

     

        public async Task<bool> DeleteItem(int id, ContextSession session)
        {
            try
            {
                var context = GetContext(session);
                var itemToDelete = context.Countries.FirstOrDefault(c => c.Id == id);
                if (itemToDelete != null)
                {
                    context.Countries.Remove(itemToDelete);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {

                return false;
            }
        }
    }
}