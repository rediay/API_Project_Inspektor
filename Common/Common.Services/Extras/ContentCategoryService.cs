using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories.Extras;
using Common.Services.Infrastructure.Services.Extras;
using Common.Utils;

namespace Common.Services.Extras
{
    public class ContentCategoryService : BaseService, IContentCategoryService
    {
        private readonly IContentCategoryRepository _contentCategoryRepository;

        public ContentCategoryService(ICurrentContextProvider contextProvider,
            IContentCategoryRepository contentCategoryRepository) : base(contextProvider)
        {
            _contentCategoryRepository = contentCategoryRepository;
        }

        public async Task<PagedResponseDTO<List<ContentCategory>>> GetAll(PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false)
        {
            var pagedResponse = await _contentCategoryRepository.GetAll(Session, paginationFilterDto, includeDeleted);
            return pagedResponse;
        }

        #region CRUD CATEGORY
        public async Task<ContentCategoryDTO> GetCategoryId(int id)
        {
            var category = await _contentCategoryRepository.GetCategoryId(id, Session);
            var map = category.MapTo<ContentCategoryDTO>();
            return map;
        }

        public async Task<List<ContentCategoryDTO>> GetCategorys()
        {
            var categorys = await _contentCategoryRepository.GetCategorys(Session);
            var map = categorys.MapTo<List<ContentCategoryDTO>>();
            return map;
        }

        public async Task<List<ContentCategoryDTO>> GetCategorysbyType(int idtype )
        {
            var categorysfilter = await _contentCategoryRepository.GetCategorysbyType(idtype,Session);
            var map = categorysfilter.MapTo<List<ContentCategoryDTO>>();
            return map;
        }

        public async Task<ContentCategoryDTO> UpdateCategory(ContentCategoryDTO categoyDTO)
        {
            try
            {
                var category = categoyDTO.MapTo<ContentCategory>();
                var categoryDTOs = await _contentCategoryRepository.UpdateCategory(category, Session);
                return categoryDTOs.MapTo<ContentCategoryDTO>();
            }
            catch (Exception ex)
            {
                return await new Task<ContentCategoryDTO>(null);
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {
            await _contentCategoryRepository.DeleteCategory(id, Session);
            return true;
        }

    
        #endregion
    }
}