using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories;
using Common.Services.Infrastructure.Repositories.Notifications;
using Common.Services.Infrastructure.Services;
using Common.Utils;

namespace Common.Services
{
    public class NotificationService : BaseService, INotificationService
    {
        private readonly IUserService _userService;
        private INotificationRepository<Notification> _notificationRepository;

        public NotificationService(ICurrentContextProvider contextProvider, IUserService userService,
            INotificationRepository<Notification> notificationRepository) : base(contextProvider)
        {
            _userService = userService;
            _notificationRepository = notificationRepository;
        }

        public async Task<PagedResponseDTO<List<NotificationSentDTO>>> GetAll(NotificationPaginationFilterDTO paginationFilterDto)
        {
            var pagedResponse = await _notificationRepository.GetAll(Session, paginationFilterDto);
            var notifications = pagedResponse.Data.Select(notification => notification.MapTo<NotificationSentDTO>()).ToList();
            return pagedResponse.CopyWith(notifications);
        }

        public async Task<ResponseDTO<NotificationSentDTO>> GetById(int id, bool includeDeleted = false)
        {
            var notification = await _notificationRepository.Get(id, Session, includeDeleted);

            if (notification == null)
            {
                return new ResponseDTO<NotificationSentDTO>(null) {Succeeded = false};
            }

            var userMapped = notification.MapTo<NotificationSentDTO>();
            var response = new ResponseDTO<NotificationSentDTO>(userMapped);

            return response;
        }
        public async Task<List<NotificationSentDTO>> GetExcelReport(NotificationPaginationFilterDTO paginationFilterDto)
        {
            var notifications = await _notificationRepository.GetExcelReport(Session, paginationFilterDto);
            return notifications.MapTo<List<NotificationSentDTO>>();
        }
    }
}