using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.Services.Notifications
{
    public interface INotificationMonitoringService
    {
        Task<PagedResponseDTO<List<NotificationsMonitoringDTO>>> GetAll(NotificationPaginationFilterDTO paginationFilterDto);
        Task<List<NotificationsMonitoringDTO>> GetExcelReport(NotificationPaginationFilterDTO paginationFilterDto);
    }
}