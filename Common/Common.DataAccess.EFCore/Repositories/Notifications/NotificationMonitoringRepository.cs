using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure.Repositories.Notifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.DataAccess.EFCore.Repositories.Notifications
{
    public class NotificationMonitoringRepository : BaseRepository<Notification, DataContext>,
        INotificationMonitoringRepository<Notification>
    {
        private readonly DataContext _context;
        public NotificationMonitoringRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedResponseDTO<List<Notification>>> GetAll(ContextSession session,
            NotificationPaginationFilterDTO paginationFilterDto)
        {
            var userId = session.UserId;
            var user= _context.Users.Where(x => x.Id ==userId);
            var totalSkipped = (paginationFilterDto.PageNumber - 1) * paginationFilterDto.PerPage;

            var queryEntities = GetEntities(session)
                .Include(obj => obj.NotificationType)
                .Where(obj => obj.CompanyId == user.FirstOrDefault().CompanyId && obj.NotificationType.Id==2)
                .Where(obj => obj.Subject.Contains(paginationFilterDto.query)
                              || obj.To.Contains(paginationFilterDto.query)
                              || obj.CreatedAt.ToString().Contains(paginationFilterDto.query)
                );
            if(paginationFilterDto.StartDate!=null )
            {
                paginationFilterDto.EndDate = string.IsNullOrEmpty(paginationFilterDto.EndDate) ? DateTime.Now.ToString(): paginationFilterDto.EndDate;


                queryEntities= queryEntities.Where(ob => ob.CreatedAt >= Convert.ToDateTime(paginationFilterDto.StartDate) && ob.CreatedAt <= (Convert.ToDateTime(paginationFilterDto.EndDate).AddDays(1)));
            }

            var notificationList = await queryEntities
                .Skip(totalSkipped)
                .Take(paginationFilterDto.PerPage)
                .ToListAsync();

            var total = await queryEntities.CountAsync();
            var pageNumber = paginationFilterDto.PageNumber;
            var perPage = paginationFilterDto.PerPage;

            var pagedResponseDto =
                new PagedResponseDTO<List<Notification>>(notificationList, pageNumber, perPage, total);

            return pagedResponseDto;
        }
        public async Task<List<Notification>> GetExcelReport(ContextSession session,
           NotificationPaginationFilterDTO paginationFilterDto)
        {
            var userId = session.UserId;
            var user = _context.Users.Where(x => x.Id == userId);            

            var queryEntities = GetEntities(session)
                .Include(obj => obj.NotificationType)
                .Where(obj => obj.CompanyId == user.FirstOrDefault().CompanyId && obj.NotificationType.Id == 2)
                .Where(obj => obj.Subject.Contains(paginationFilterDto.query)
                              || obj.To.Contains(paginationFilterDto.query)
                              || obj.CreatedAt.ToString().Contains(paginationFilterDto.query)
                );
            if (paginationFilterDto.StartDate != null)
            {
                paginationFilterDto.EndDate = string.IsNullOrEmpty(paginationFilterDto.EndDate) ? DateTime.Now.ToString() : paginationFilterDto.EndDate;


                queryEntities = queryEntities.Where(ob => ob.CreatedAt >= Convert.ToDateTime(paginationFilterDto.StartDate) && ob.CreatedAt <= (Convert.ToDateTime(paginationFilterDto.EndDate).AddDays(1)));
            }

            var notificationList = await queryEntities.ToListAsync();

            return notificationList;
        }
    }
}