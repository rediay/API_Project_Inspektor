using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DataAccess.EFCore.Repositories.Lists;
using Common.DTO;
using Common.DTO.Lists;
using Common.DTO.RestrictiveLists;
using Common.Entities;
using Common.Entities.Relations_Countrys;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories.Lists;
using Common.Services.Infrastructure.Services.Lists;
using Common.Utils;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Office2010.Excel;
using Newtonsoft.Json;

namespace Common.Services
{
    public class ThirdListsService : BaseService, IThirdListsService
    {
        private readonly IThirdListRepository _thirdlist;

        public ThirdListsService(ICurrentContextProvider contextProvider, IThirdListRepository thirdlistRepository) : base(
            contextProvider)
        {
            _thirdlist = thirdlistRepository;
        }

        public async Task<PagedResponseDTO<List<ThirdList>>> GetAll(PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false)
        {
            var pagedResponse = await _thirdlist.GetAll(Session,paginationFilterDto, includeDeleted);
            return pagedResponse;
        }

        public async Task<PagedResponseDTO<List<ThirdListsDTO>>> GetAllQuery(ListPaginationFilterThirdDTO paginationFilterDto,
            bool includeDeleted = false)
        {
            var pagedResponse = await _thirdlist.GetAllQuery(Session, paginationFilterDto, includeDeleted);
            var listGroups = pagedResponse.Data.Select(list => list.MapTo<ThirdListsDTO>()).ToList();
            return pagedResponse.CopyWith(listGroups);
        }

        public async Task<PagedResponseDTO<List<ThirdListsDTO>>> GetAllToVerify(ListPaginationFilterThirdDTO paginationFilterDto,
            bool includeDeleted = false)
        {
            var pagedResponse = await _thirdlist.GetAllToVerify(Session, paginationFilterDto, includeDeleted);
            var listGroups = pagedResponse.Data.Select(list => list.MapTo<ThirdListsDTO>()).ToList();
            return pagedResponse.CopyWith(listGroups);
        }

        public async Task<ThirdListsDTO> GetListById(string id)
        {
            var list = await _thirdlist.GetListById(id,Session);            
            return list.MapTo<ThirdListsDTO>(); ;
        }

        public async Task<ResponseDTO<ThirdListsDTO>> Edit(ThirdListsDTO dto)
        {

            ThirdList listactual = await _thirdlist.GetListById( dto.Id.ToString(), Session);
            listactual.TempData = JsonConvert.SerializeObject(dto);
            listactual.Validated = true;
            dto = listactual.MapTo<ThirdListsDTO>();
            dto.Validated = false;
            var record = dto.MapTo<ThirdList>();
            var newRecord = await _thirdlist.Edit(record,Session);
            var recordMapped = newRecord.MapTo<ThirdListsDTO>();
            var response = new ResponseDTO<ThirdListsDTO>(recordMapped);
            return response;
        }

        public async Task<ResponseDTO<bool>> BulkCreate(List<ThirdListsDTO> dtos)
        {
            var records = dtos.Select(list => list.MapTo<ThirdList>()).ToList();
            var status = await _thirdlist.BulkCreate(records, Session);
            var response = new ResponseDTO<bool>(status);
            return response;
        }

        public async Task<ResponseDTO<bool>> Delete(int id)
        {
            var status = await _thirdlist.Delete(id, Session);
            var response = new ResponseDTO<bool>(status);
            return response;
        }

        public async Task<ResponseDTO<ThirdListsDTO>> ValidateRegister(List<int> ids)
        {
            try{
                for (int i = 0; i < ids.Count; i++)
                {

                    var id = ids[i];
                    ThirdList listactual = await _thirdlist.GetListById(id.ToString(), Session);

                    listactual = JsonConvert.DeserializeObject<ThirdList>(listactual.TempData);
                    listactual.Validated = false;
                    listactual.TempData = null;
                    var newRecord = await _thirdlist.Edit(listactual, Session);
                    //await _thirdlist.monitorlog(id);
                    
                }


                var response = new ResponseDTO<ThirdListsDTO>(null);
                return response;
            }
            catch
            {
                var response = new ResponseDTO<ThirdListsDTO>(null);
                return response;
            }
            
            
        }
    }
}