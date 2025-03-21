using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories.Notifications
{
    public interface INotificationMonitoringRepository<TNotificationMonitoring> where TNotificationMonitoring : Notification
    {
        Task<PagedResponseDTO<List<TNotificationMonitoring>>> GetAll(ContextSession session,
            NotificationPaginationFilterDTO paginationFilterDto);
        Task<List<TNotificationMonitoring>> GetExcelReport(ContextSession session,
            NotificationPaginationFilterDTO paginationFilterDto);
    }
}