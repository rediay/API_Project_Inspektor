using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.RestrictiveLists;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories.RestrictiveLists;
using Common.Services.Infrastructure.Services.RestrictiveLists;
using Common.Utils;

namespace Common.Services.RestrictiveLists
{
    public class ListService : BaseService, IListService
    {
        private readonly IListRepository _listRepository;

        public ListService(ICurrentContextProvider contextProvider, IListRepository listRepository) : base(
            contextProvider)
        {
            _listRepository = listRepository;
        }

        public async Task<PagedResponseDTO<List<ListDTO>>> GetAll(ListPaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false)
        {
            
            var pagedResponse = await _listRepository.GetAll(Session, paginationFilterDto, includeDeleted);
            var listGroups = pagedResponse.Data.Select(list => list.MapTo<ListDTO>()).ToList();
            return pagedResponse.CopyWith(listGroups);
            
        }

        public async Task<PagedResponseDTO<List<ListDTO>>> GetAllByValidation(
            ListPaginationFilterDTO paginationFilterDto, bool includeDeleted = false)
        {
            var pagedResponse = await _listRepository.GetAllByValidation(Session, paginationFilterDto, includeDeleted);
            var records = pagedResponse.Data.Select(list => list.MapTo<ListDTO>()).ToList();
            //var records = pagedResponse.Data.Select(list => list.TempData.MapTo<ListDTO>()).ToList();
            return pagedResponse.CopyWith(records);
        }

        public async Task<ResponseDTO<ListDTO>> GetById(int id, bool includeDeleted = false)
        {
            var result = await _listRepository.Get(id, Session, includeDeleted);

            if (result == null)
            {
                return new ResponseDTO<ListDTO>(null) {Succeeded = false};
            }

            var recordMapped = result.MapTo<ListDTO>();
            var response = new ResponseDTO<ListDTO>(recordMapped);

            return response;
        }

        public async Task<ResponseDTO<ListDTO>> Edit(ListDTO dto)
        {
            var currentStoredRecord = await _listRepository.Get(dto.Id, Session, true);
            if (currentStoredRecord == null) return new ResponseDTO<ListDTO>(null);

            var newData = dto.MapTo<List>();

            var updatedRecord = await _listRepository.Edit(newData, Session);
            var recordMapped = updatedRecord.MapTo<ListDTO>();
            var response = new ResponseDTO<ListDTO>(recordMapped);

            return response;
        }

        public async Task<ResponseDTO<ListDTO>> Create(ListDTO dto)
        {
            var newData = dto.MapTo<List>();
            var newRecord = await _listRepository.Edit(newData, Session);
            var recordMapped = newRecord.MapTo<ListDTO>();
            var response = new ResponseDTO<ListDTO>(recordMapped);

            return response;
        }

        public async Task<ResponseDTO<bool>> BulkCreate(List<ListDTO> dtos)
        {
            var records = dtos.Select(list => list.MapTo<List>()).ToList();
            var status = await _listRepository.BulkCreate(records, Session);
            var response = new ResponseDTO<bool>(status);
            return response;
        }

        public async Task<bool> ValidateRecords(IEnumerable<int> listId)
        {
            return await _listRepository.ValidateRecords(listId, Session);           
        }
    }
}