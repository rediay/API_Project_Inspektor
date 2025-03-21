using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.Services
{
    public interface INotificationService
    {
        Task<PagedResponseDTO<List<NotificationSentDTO>>> GetAll(NotificationPaginationFilterDTO paginationFilterDto);
        Task<ResponseDTO<NotificationSentDTO>> GetById(int id, bool includeDeleted = false);
        Task<List<NotificationSentDTO>> GetExcelReport(NotificationPaginationFilterDTO paginationFilterDto);
    }
}