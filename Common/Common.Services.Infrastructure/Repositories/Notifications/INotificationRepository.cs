using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories.Notifications
{
    public interface INotificationRepository<TNotification> where TNotification : Notification
    {
        Task<PagedResponseDTO<List<TNotification>>> GetAll(ContextSession session,
            NotificationPaginationFilterDTO paginationFilterDto);
        
        Task<Notification> Get(int id, ContextSession session, bool includeDeleted = false);
        
        Task<Notification> Edit(Notification notification, ContextSession session);
        Task<List<TNotification>> GetExcelReport(ContextSession session,
            NotificationPaginationFilterDTO paginationFilterDto);
    }
}