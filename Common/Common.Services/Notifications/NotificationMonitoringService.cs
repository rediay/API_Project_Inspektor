using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories.Notifications;
using Common.Services.Infrastructure.Services.Notifications;
using Common.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Services.Notifications
{
    public class NotificationMonitoringService : BaseService, INotificationMonitoringService
    {
        private readonly IUserService _userService;
        private INotificationMonitoringRepository<Notification> _notificationMonitoringRepository;

        public NotificationMonitoringService(ICurrentContextProvider contextProvider, IUserService userService,
            INotificationMonitoringRepository<Notification> notificationMonitoringRepository) : base(contextProvider)
        {
            _userService = userService;
            _notificationMonitoringRepository = notificationMonitoringRepository;
        }

        public async Task<PagedResponseDTO<List<NotificationsMonitoringDTO>>> GetAll(NotificationPaginationFilterDTO paginationFilterDto)
        {
            var pagedResponse = await _notificationMonitoringRepository.GetAll(Session, paginationFilterDto);
            var notifications = pagedResponse.Data.Select(notification => notification.MapTo<NotificationsMonitoringDTO>()).ToList();
            return pagedResponse.CopyWith(notifications);
        }

        public async Task<List<NotificationsMonitoringDTO>> GetExcelReport(NotificationPaginationFilterDTO paginationFilterDto)
        {
            var notifications = await _notificationMonitoringRepository.GetExcelReport(Session, paginationFilterDto);
            return notifications.MapTo<List<NotificationsMonitoringDTO>>();
        }
    }
}