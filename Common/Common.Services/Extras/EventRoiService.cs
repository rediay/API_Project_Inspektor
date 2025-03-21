using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories;
using Common.Services.Infrastructure.Repositories.Extras;
using Common.Services.Infrastructure.Repositories.Notifications;
using Common.Services.Infrastructure.Services;
using Common.Services.Infrastructure.Services.Extras;
using Common.Utils;

namespace Common.Services.Extras
{
    public class EventRoiService<TNotification> : BaseService, IEventRoiService
        where TNotification : Notification, new()
    {
        //private readonly IEventRoiRepository<TEvents> _eventRoiRepository;
        private INotificationRepository<TNotification> _notificationRepository;
        private readonly IEventRoiOperationStatusService _eventRoiOperationStatusService;
        private readonly IEventRoiOperationTypeService _eventRoiOperationTypeService;
        protected readonly INotificationSettingsRepository _notificationSettingsRepository;
        private readonly IUserService _userService;
        private readonly ISendEmail _sendEmail;
        public EventRoiService(ICurrentContextProvider contextProvider,
            INotificationRepository<TNotification> notificationRepository,
            IEventRoiOperationStatusService eventRoiOperationStatusService,
            INotificationSettingsRepository notificationSettingsRepository,
            ISendEmail sendEmail,
            IUserService userService,
            IEventRoiOperationTypeService eventRoiOperationTypeService) : base(contextProvider)
        {
            _notificationRepository = notificationRepository;
            _eventRoiOperationStatusService = eventRoiOperationStatusService;
            _eventRoiOperationTypeService = eventRoiOperationTypeService;
            _sendEmail = sendEmail;
            _userService = userService;
            this._notificationSettingsRepository = notificationSettingsRepository;
        }

        public async Task<ResponseDTO<EventRoiDTO>> Create(EventRoiDTO roiDto)
        {
            var notification = new Notification();
            var operationStatusId = roiDto.OperationStatusId;
            var operationTypeId = roiDto.OperationTypeId;

            var operationStatus = await _eventRoiOperationStatusService.GetById(operationStatusId);
            var operationType = await _eventRoiOperationTypeService.GetById(operationTypeId);
            var currentUserId = Session.UserId;
            var currentUser = await _userService.GetById(currentUserId);
            var companyId = currentUser.CompanyId;
            NotificationSettings notificationSettings = await _notificationSettingsRepository.GetByCompanyId(companyId, Session);
            if (notificationSettings != null && notificationSettings.SendMailPriority1)
            {
                var subject = "Notificación de registro de ROI/Denuncias";
                var to = notificationSettings.MailPriority1.Trim();
                var body = $"{DateTime.Now.ToString("dd/MM/yyyy")} Buen día<br /><br /><br />Se ha registrado un ROI/Denuncia con los siguientes datos<br /><br /><table style\"width:100%\"><tr><td>Titulo:</td><td>{roiDto.Title}</td></tr><tr><td>Tipo Operación:</td><td>{roiDto.OperationType}</td><td>Fecha de los hechos:</td><td>{roiDto.TransactionDate}</td></tr><tr><td>Estado Operacion:</td><td>{roiDto.OperationStatus}</td><td>Fecha Registro:</td><td>{DateTime.Now}</td></tr><tr><td>Documento:</td><td>{roiDto.Identification}</td><td>Monto Estimado:</td><td>{roiDto.EstimatedAmount}</td></tr><tr><td>Observaciones:</td></tr><tr><td colspan\"2\">{roiDto.Observations}</td></tr></table><br /><br />Este correo ha sido enviado automaticamente, favor no responder a esta direccion de correo, ya que no se encuentra habilitada para recibir mensajes. Si requiere mayor informacion sobre el contenido de este mensaje, contacte a su Oficial de Cumplimiento.<br /><br /> Cordialmente,<br /><br /><B>Inspektor®</B><br />Software para la administración y gestión de listas";

                roiDto.OperationStatus = operationStatus.Name;
                roiDto.OperationType = operationType.Name;
                bool isMailSended= _sendEmail.Send(new EmailMessageRequestDto()
                {
                    To = to.Split(",").ToList(),
                    Subject = subject,
                    Body = body
                });
                if (isMailSended)
                {
                    var eventRoiJson = Newtonsoft.Json.JsonConvert.SerializeObject(roiDto);

                    notification.json = eventRoiJson;
                    notification.Subject = subject;
                    notification.To = to;
                    notification.Detail = body;
                    notification.Status = true;
                    notification.CompanyId = companyId;
                    notification.UserId = currentUserId;
                    notification.NotificationTypeId = 6;

                    var newNotification = await _notificationRepository.Edit(notification, Session);

                    var response = new ResponseDTO<EventRoiDTO>(roiDto);
                    return response;
                }
            }
            return null;
        }

        public async Task<ResponseDTO<List<EventRoiOperationType>>> GetAllOperationTypes()
        {
            var types = await _eventRoiOperationTypeService.GetAll(Session);
            var response = new ResponseDTO<List<EventRoiOperationType>>(types);
            return response;
        }

        public async Task<ResponseDTO<List<EventRoiOperationStatus>>> GetAllOperationStatuses()
        {
            var statuses = await _eventRoiOperationStatusService.GetAll(Session);
            var response = new ResponseDTO<List<EventRoiOperationStatus>>(statuses);
            return response;
        }
    }
}