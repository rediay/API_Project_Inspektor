using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories;
using Common.Services.Infrastructure.Services;

namespace Common.Services
{
    public class ListPeriodicityService : BaseService, IListPeriodicityService
    {
        private readonly IListPeriodicityRepository _listPeriodicityRepository;

        public ListPeriodicityService(ICurrentContextProvider contextProvider,
            IListPeriodicityRepository listPeriodicityRepository) : base(contextProvider)
        {
            _listPeriodicityRepository = listPeriodicityRepository;
        }

        public async Task<PagedResponseDTO<List<Periodicity>>> GetAll(PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false)
        {
            var pagedResponse = await _listPeriodicityRepository.GetAll(Session, paginationFilterDto, includeDeleted);
            return pagedResponse;
        }
    }
}