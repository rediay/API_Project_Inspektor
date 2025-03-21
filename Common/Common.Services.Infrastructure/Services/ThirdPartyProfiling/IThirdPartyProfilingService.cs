using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.ThirdPartyProfiling;
using Microsoft.AspNetCore.Http;

namespace Common.Services.Infrastructure.Services.ThirdPartyProfiling
{
    public interface IThirdPartyProfilingService
    {
        Task<PagedResponseDTO<List<ThirdPartyProfilingDTO>>> GetAll(
            PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);

        Task<object> Import(IFormFile templateFile);
        Task<List<string>> GetExportTemplateColumns();
    }
}