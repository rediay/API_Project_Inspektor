using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories.Extras;
using Common.Services.Infrastructure.Services.Extras;
using Common.Utils;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace Common.Services.Extras
{
    public class ContentService<TNews> : BaseService, IContentService where TNews : Content, new()
    {
        private readonly IContentRepository<TNews> _contentRepository;

        public ContentService(ICurrentContextProvider contextProvider, IContentRepository<TNews> contentRepository) : base(
            contextProvider)
        {
            _contentRepository = contentRepository;
        }

                public async Task<PagedResponseDTO<List<ContentDTO>>> GetAll(ContentPaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false)
        {
            var pagedResponse = await _contentRepository.GetAll(Session, paginationFilterDto, includeDeleted);
            var newslist = pagedResponse.Data.Select(news => news.MapTo<ContentDTO>()).ToList();            
            return pagedResponse.CopyWith(newslist);
        }

        public async Task <List<ContentExcelDTO>> GetAllToExcel(ContentPaginationFilterDTO paginationFilterDto)
        {
            var newsList = await _contentRepository.GetAllNews(Session, paginationFilterDto);
            var listEnd = newsList.MapTo<List<ContentDTO>>();
            var newList = listEnd.Select(p => new ContentExcelDTO
            {
                Id = p.Id,
                Title= p.Title,
                Description= p.Description,
                Source = p.Source,
                Date= p.Date,
                CreatedAt= p.CreatedAt,
                CountryId= p.CountryId,
                ContentCategoryId = p.ContentCategoryId,
                ContentTypeId= p.ContentTypeId,
                ContentStatusId= p.ContentStatusId,
                ContentCategory = p.ContentCategory.Name,
                Countries = p.Countries,
                ContentTypes= p.ContentTypes,
            }).ToList();
            
            return newList;
        }

        public async Task<ContentDTO> GetContentId(int id)
        {
            var content = await _contentRepository.GetContentId(id, Session);
            var map = content.MapTo<ContentDTO>();
            return map;
        }

        public async Task<List<ContentDTO>> GetContents()
        {
            var contents = await _contentRepository.GetContents(Session);
            var map = contents.MapTo<List<ContentDTO>>();
            return map;
        }

        public async Task<ContentDTO> UpdateContent(ContentDTO contentDTO)
        {
            try
            {
                var content = contentDTO.MapTo<Content>();
                var contentDTOs = await _contentRepository.UpdateContent(content, Session);
                return contentDTOs.MapTo<ContentDTO>();
            }
            catch (Exception ex)
            {
                return await new Task<ContentDTO>(null);
            }
        }

        public async Task<bool> DeleteContent(int id)
        {
            await _contentRepository.DeleteContent(id, Session);
            return true;
        }









        /*public async Task<PagedResponseDTO<List<ContentDTO>>> GetAllNews(ContentPaginationFilterDTO paginationFilterDto, bool includeDeleted = false)
        {
            var pagedResponse = await _contentRepository.GetAllNews(Session, paginationFilterDto, includeDeleted);
            var newslist = pagedResponse.Data.Select(news => news.MapTo<ContentDTO>()).ToList();
            return pagedResponse.CopyWith(newslist);
        }*/


    }
}