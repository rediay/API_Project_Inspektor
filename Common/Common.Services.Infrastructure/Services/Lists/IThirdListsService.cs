using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.Lists;
using Common.DTO.RestrictiveLists;
using Common.Entities;
using Common.Entities.Relations_Countrys;

namespace Common.Services.Infrastructure.Services.Lists
{
    public interface IThirdListsService
    {
        Task<PagedResponseDTO<List<ThirdListsDTO>>> GetAllQuery(ListPaginationFilterThirdDTO paginationFilterDto,
            bool includeDeleted = false);

        Task<PagedResponseDTO<List<ThirdListsDTO>>> GetAllToVerify(ListPaginationFilterThirdDTO paginationFilterDto,
            bool includeDeleted = false);
        Task<PagedResponseDTO<List<ThirdList>>> GetAll(PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);

        Task<ThirdListsDTO> GetListById(string id);
        Task<ResponseDTO<ThirdListsDTO>> Edit(ThirdListsDTO dto);
        Task<ResponseDTO<ThirdListsDTO>> ValidateRegister(List<int> ids);
        Task<ResponseDTO<bool>> BulkCreate(List<ThirdListsDTO> dtos);
        Task<ResponseDTO<bool>> Delete(int id);
    }
}