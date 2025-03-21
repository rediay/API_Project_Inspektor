using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.RestrictiveLists;
using Common.Entities;
using Common.Services.Infrastructure.Services;
using Common.Services.Infrastructure.Services.Lists;
using Common.Services.Infrastructure.Services.RestrictiveLists;
using Common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Common.WebApiCore.Controllers.RestrictiveLists
{
    [Route("restrictive-lists/lists")]
    public class ListController : BaseApiController
    {
        private readonly IListService _listService;
        private readonly IListTypeService _listTypeService;
        private readonly IPersonTypeService _personTypeService;
        private readonly IDocumentTypeService _documentTypeService;
        private readonly IThirdListsService _thirdLists;

        public ListController(IListService listService, IListTypeService listTypeService,
            IPersonTypeService personTypeService, IDocumentTypeService documentTypeService, IThirdListsService thirdLists)
        {
            _listService = listService;
            _listTypeService = listTypeService;
            _personTypeService = personTypeService;
            _documentTypeService = documentTypeService;
            _thirdLists = thirdLists;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] ListPaginationFilterDTO paginationFilter)
        {
            // return Ok(paginationFilter);
            var results = await _listService.GetAll(paginationFilter);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var listResponse = await _listTypeService.GetAll();
            var personTypesResponse = await _personTypeService.GetAll();
            var documentTypesResponse = await _documentTypeService.GetAll();


            var listTypes = listResponse.Data;
            var personTypes = personTypesResponse.Data;
            var documentTypes = documentTypesResponse.Data;

            var result = await _listService.GetById(id);

            result.Data.ListTypes = listTypes;
            result.Data.PersonTypes = personTypes;
            result.Data.DocumentTypes = documentTypes;


            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Create(ListDTO dto)
        {
            var currentUserId = User.GetUserId();
            dto.UserId = currentUserId;
            dto.Validated = false;

            var result = await _listService.Create(dto);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Edit(int id, ListDTO dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var currentUserId = User.GetUserId();
            dto.UserId = currentUserId;

            var currentListResponse = await _listService.GetById(dto.Id);
            var currentListDto = currentListResponse.Data;

            var listTypeId = dto.ListTypeId;
            var personTypeId = dto.PersonTypeId;
            var documentTypeId = dto.DocumentTypeId;

            var listTypeResponse = await _listTypeService.GetById(listTypeId);
            var personTypesResponse = await _personTypeService.GetById(personTypeId);
            var documentTypesResponse = await _documentTypeService.GetById(documentTypeId);

            var listType = listTypeResponse.Data;
            var personType = personTypesResponse.Data;
            var documentType = documentTypesResponse.Data;

            dto.ListType = listType.MapTo<ListType>();
            dto.PersonType = personType;
            dto.DocumentType = documentType;

            currentListDto.TempData = dto;
            // return Ok(currentListDto);

            var result = await _listService.Edit(currentListDto);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet]
        [Route("create")]
        //[Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> GetCreateFormData()
        {
            var listResponse = await _listTypeService.GetAll();
            var personTypesResponse = await _personTypeService.GetAll();
            var documentTypesResponse = await _documentTypeService.GetAll();

            var listTypes = listResponse.Data;
            var personTypes = personTypesResponse.Data;
            var documentTypes = documentTypesResponse.Data;

            var emptyRecord = new ListDTO
            {
                ListTypes = listTypes,
                PersonTypes = personTypes,
                DocumentTypes = documentTypes,
            };

            var response = new ResponseDTO<ListDTO>(emptyRecord);

            return Ok(response);
        }

        [HttpGet]
        [Route("filters")]
        [Authorize]
        public async Task<IActionResult> GetSearchFormFilters()
        {
            var listResponse = await _listTypeService.GetAll();
            /*var personTypesResponse = await _personTypeService.GetAll();
            var documentTypesResponse = await _documentTypeService.GetAll();*/

            var listTypes = listResponse.Data;
            /*var personTypes = personTypesResponse.Data;
            var documentTypes = documentTypesResponse.Data;*/

            var emptyRecord = new ListDTO
            {
                ListTypes = listTypes,
            };

            var response = new ResponseDTO<ListDTO>(emptyRecord);

            return Ok(response);
        }

        [HttpPost]
        [Route("upload")]
        [Authorize]
        public async Task<IActionResult> Upload(List<ListDTO> dtos)
        {
            var currentUserId = User.GetUserId();
            var newDtos = dtos.Select(obj =>
            {
                obj.UserId = currentUserId;
                obj.Validated = false;
                return obj;
            }).ToList();

            var result = await _listService.BulkCreate(newDtos);
            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}