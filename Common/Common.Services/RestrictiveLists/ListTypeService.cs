using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories.RestrictiveLists;
using Common.Services.Infrastructure.Services.RestrictiveLists;
using Common.Utils;
using ListTypeDTO = Common.DTO.RestrictiveLists.ListTypeDTO;

namespace Common.Services.RestrictiveLists
{
    public class ListTypeService : BaseService, IListTypeService
    {
        private readonly IListTypeRepository _listTypeRepository;

        public ListTypeService(ICurrentContextProvider contextProvider, IListTypeRepository listTypeRepository) : base(
            contextProvider)
        {
            _listTypeRepository = listTypeRepository;
        }

        public async Task<ResponseDTO<List<ListTypeDTO>>> GetAll(bool includeDeleted = false)
        {
            var listTypes = await _listTypeRepository.GetAll(Session, includeDeleted);
            var recordMapped = listTypes.MapTo<List<ListTypeDTO>>();
            
            var response = new ResponseDTO<List<ListTypeDTO>>(recordMapped);

            return response;
        }

        public async Task<DTO.PagedResponseDTO<List<ListTypeDTO>>> GetAll(PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false)
        {
            var pagedResponse = await _listTypeRepository.GetAll(Session, paginationFilterDto, includeDeleted);
            var records = pagedResponse.Data
                .Select(listType =>
                {
                    var color = listType.ListGroup.Color;
                    var listTypeDto = listType.MapTo<ListTypeDTO>();
                    listTypeDto.Color = color;
                    return listTypeDto;
                })
                .ToList();
            return pagedResponse.CopyWith(records);
        }

        public async Task<ResponseDTO<ListTypeDTO>> GetById(int id, bool includeDeleted = false)
        {
            var result = await _listTypeRepository.Get(id, Session, includeDeleted);

            if (result == null)
            {
                return new ResponseDTO<ListTypeDTO>(null) {Succeeded = false};
            }

            var listGroupMapped = result.MapTo<ListTypeDTO>();
            var response = new ResponseDTO<ListTypeDTO>(listGroupMapped);

            return response;
        }

        public async Task<ResponseDTO<ListTypeDTO>> Edit(ListTypeDTO dto)
        {
            /*var errorsResponse = await validateFields(dto);

            if (errorsResponse != null && !errorsResponse.Succeeded)
            {
                return errorsResponse;
            }*/

            var currentStoredRecord = await _listTypeRepository.Get(dto.Id, Session, true);
            if (currentStoredRecord == null) return new ResponseDTO<ListTypeDTO>(null);

            var newData = dto.MapTo<ListType>();
            
            var updatedRecord = await _listTypeRepository.Edit(newData, Session);
            var recordMapped = updatedRecord.MapTo<ListTypeDTO>();
            var response = new ResponseDTO<ListTypeDTO>(recordMapped);

            return response;
        }

        public async Task<ResponseDTO<ListTypeDTO>> Create(ListTypeDTO dto)
        {
            /*var errorsResponse = await validateFields(dto);

            if (errorsResponse != null && !errorsResponse.Succeeded)
            {
                return errorsResponse;
            }*/

            var newData = dto.MapTo<ListType>();
            var newRecord = await _listTypeRepository.Edit(newData, Session);
            var recordMapped = newRecord.MapTo<ListTypeDTO>();
            var response = new ResponseDTO<ListTypeDTO>(recordMapped);

            return response;
        }

        public ResponseDTO<bool> Delete(int id)
        {
            var status = _listTypeRepository.Delete(id, Session);
            var response = new ResponseDTO<bool>(true);
            return response;
        }
    }
}