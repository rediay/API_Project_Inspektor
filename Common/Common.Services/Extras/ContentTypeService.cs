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
    public class ContentTypeService : BaseService, IContentTypeService
    {
        private readonly IContentTypeRepository _contentTypeRepository;

        public ContentTypeService(ICurrentContextProvider contextProvider, IContentTypeRepository contentTypeRepository) : base(
            contextProvider)
        {
            _contentTypeRepository = contentTypeRepository;
        }

        public async Task<PagedResponseDTO<List<ContentType>>> GetAll(PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false)
        {
            var pagedResponse = await _contentTypeRepository.GetAll(Session, paginationFilterDto, includeDeleted);
            return pagedResponse;
        }

        #region CRUD TypeContent
        public async Task<List<ContentTypeDTO>> GetTypeContent()
        {
            var states = await _contentTypeRepository.GetTypeContent(Session);
            var map = states.MapTo<List<ContentTypeDTO>>();
            return map;
        }

        public async Task<ContentTypeDTO> GetTypeContentId(int id)
        {
            var contentType = await _contentTypeRepository.GetTypeContentId(id, Session);
            var map = contentType.MapTo<ContentTypeDTO>();
            return map;
        }


        public async Task<ContentTypeDTO> UpdateTypeContent(ContentTypeDTO typecontentDTO)
        {
            try
            {
                var contentType = typecontentDTO.MapTo<ContentType>();
                var contentTypeDTOs = await _contentTypeRepository.UpdateTypeContent(contentType, Session);
                return contentTypeDTOs.MapTo<ContentTypeDTO>();
            }
            catch (Exception ex)
            {
                return await new Task<ContentTypeDTO>(null);
            }
        }

        public async Task<bool> DeleteTypeContent(int id)
        {
            await _contentTypeRepository.DeleteTypeContent(id, Session);
            return true;
        }

        #endregion
    }
}