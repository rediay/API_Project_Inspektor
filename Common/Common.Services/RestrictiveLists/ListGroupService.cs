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
    public class ListGroupService : BaseService, IListGroupService
    {
        private readonly IListGroupRepository _listGroupRepository;

        public ListGroupService(ICurrentContextProvider contextProvider, IListGroupRepository listGroupRepository) :
            base(contextProvider)
        {
            _listGroupRepository = listGroupRepository;
        }

        public async Task<PagedResponseDTO<List<ListGroupDTO>>> GetAll(PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false)
        {
            var pagedResponse = await _listGroupRepository.GetAll(Session, paginationFilterDto, includeDeleted);
            var listGroups = pagedResponse.Data.Select(listGroup => listGroup.MapTo<ListGroupDTO>()).ToList();
            return pagedResponse.CopyWith(listGroups);
        }

        public async Task<ResponseDTO<ListGroupDTO>> GetById(int id, bool includeDeleted = false)
        {
            var result = await _listGroupRepository.Get(id, Session, includeDeleted);

            if (result == null)
            {
                return new ResponseDTO<ListGroupDTO>(null) {Succeeded = false};
            }

            var listGroupMapped = result.MapTo<ListGroupDTO>();
            var response = new ResponseDTO<ListGroupDTO>(listGroupMapped);

            return response;
        }

        public async Task<ResponseDTO<ListGroupDTO>> Edit(ListGroupDTO dto)
        {
            /*var errorsResponse = await validateFields(dto);

            if (errorsResponse != null && !errorsResponse.Succeeded)
            {
                return errorsResponse;
            }*/

            var currentStoredRecord = await _listGroupRepository.Get(dto.Id, Session, true);
            if (currentStoredRecord == null) return new ResponseDTO<ListGroupDTO>(null);

            var newData = dto.MapTo<ListGroup>();
            var updatedRecord = await _listGroupRepository.Edit(newData, Session);
            var recordMapped = updatedRecord.MapTo<ListGroupDTO>();
            var response = new ResponseDTO<ListGroupDTO>(recordMapped);

            return response;
        }

        public async Task<ResponseDTO<ListGroupDTO>> Create(ListGroupDTO dto)
        {
            /*var errorsResponse = await validateFields(dto);

            if (errorsResponse != null && !errorsResponse.Succeeded)
            {
                return errorsResponse;
            }*/

            var newData = dto.MapTo<ListGroup>();
            var newRecord = await _listGroupRepository.Edit(newData, Session);
            var recordMapped = newRecord.MapTo<ListGroupDTO>();
            var response = new ResponseDTO<ListGroupDTO>(recordMapped);

            return response;
        }

        public ResponseDTO<bool> Delete(int id)
        {
            var status = _listGroupRepository.Delete(id, Session);
            var response = new ResponseDTO<bool>(true);
            return response;
        }

        private async Task<ResponseDTO<ListGroupDTO>> validateFields(ListGroupDTO dto,
            bool includeExitingDto = true)
        {
            var response = new ResponseDTO<ListGroupDTO>(null);

            if (dto == null ||
                string.IsNullOrEmpty(dto.Name) ||
                string.IsNullOrEmpty(dto.Description) ||
                string.IsNullOrEmpty(dto.Color)
            ) return null;

            response = new ResponseDTO<ListGroupDTO>(null);
            var errorsDictionary = new Dictionary<string, dynamic>();

            /*const string validation = "Compo requerido";
            const string identification = "Esta identification ya ha sido registrada";
            const string login = "Este nombre de usuario ya ha sido registrado";

            if (string.Equals(existingUser.Email, dto.Email, StringComparison.CurrentCultureIgnoreCase))
                errorsDictionary["email"] = email;

            if (string.Equals(existingUser.Identification, dto.Identification,
                StringComparison.CurrentCultureIgnoreCase))
                errorsDictionary["identification"] = identification;

            if (string.Equals(existingUser.Login, dto.Login, StringComparison.CurrentCultureIgnoreCase))
                errorsDictionary["login"] = login;*/

            response.Errors = errorsDictionary;
            response.Succeeded = false;

            return response;
        }
    }
}