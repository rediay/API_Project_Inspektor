using Common.DTO;
using Common.DTO.Users;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories.Management;
using Common.Services.Infrastructure.Repositories.Notifications;
using Common.Services.Infrastructure.Repositories.Users;
using Common.Services.Infrastructure.Services;
using Common.Services.Infrastructure.Services.Users;
using Common.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Common.Services.Users
{
    public class UserManagementService<TUser> : BaseService, IUserManagementService where TUser : User, new()
    {
        private readonly IUserService _userService;
        private readonly IUserManagementRepository<TUser> _userManagementRepository;        
        private readonly IAuthService _authenticationService;
        private readonly ISendEmail _sendEmail;
        private readonly INotificationRepository<Notification> _notificationRepository;
        private readonly EmailSettings _emailSettings;

        public UserManagementService(ICurrentContextProvider contextProvider,
            IUserManagementRepository<TUser> userManagementRepository,
            IUserService userService,
            IAuthService authenticationService,            
            ISendEmail sendEmail,
            INotificationRepository<Notification> notificationRepository, EmailSettings emailSettings) : base(contextProvider)
        {
            _userManagementRepository = userManagementRepository;
            _userService = userService;
            _authenticationService = authenticationService;
            _sendEmail = sendEmail;
            _notificationRepository = notificationRepository;
            _emailSettings = emailSettings;
        }

        public async Task<PagedResponseDTO<List<UserManagementDto>>> GetAll(PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false)
        {
            var pagedResponse = await _userManagementRepository.GetAll(Session, paginationFilterDto, includeDeleted);
            var users = pagedResponse.Data.Select(user => user.MapTo<UserManagementDto>()).ToList();
            return pagedResponse.CopyWith(users);
        }

        public async Task<ResponseDTO<UserManagementDto>> GetById(int id, bool includeDeleted = false)
        {
            var user = await _userManagementRepository.Get(id, Session, includeDeleted);

            if (user == null)
            {
                return new ResponseDTO<UserManagementDto>(null) { Succeeded = false };
            }

            var userMapped = user.MapTo<UserManagementDto>();
            var response = new ResponseDTO<UserManagementDto>(userMapped);

            return response;
        }

        public async Task<ResponseDTO<UserManagementDto>> Create(UserManagementDto dto)
        {
            var errorsResponse = await validateFields(dto);

            if (errorsResponse != null && !errorsResponse.Succeeded)
            {
                return errorsResponse;
            }
            var currentUserId = Session.UserId;
            var currentUser = await _userService.GetById(currentUserId);
            var newUserData = dto.MapTo<TUser>();
            newUserData.CompanyId = currentUser.CompanyId;            

            var newUser = await _userManagementRepository.Edit(newUserData, Session);                        
            string Password = Strings.CreateRandomString(12);

            //Asign Password to User
            String PasswordHash = await _authenticationService.GetHashPassword(newUser.Email, Password);
            newUserData.Password = PasswordHash;
            var newUser1 = await _userManagementRepository.Edit(newUserData, Session);

            //Send Email to User Created
            _sendEmail.Send(new EmailMessageRequestDto()
            {
                To = new List<string>()
                {
                    newUser1.Email
                },
                Subject = "Nuevo Usuario Creado",
                Body = _emailSettings.PlantillaUserCreated.Replace("{newUser1.Name}", newUser1.Name).Replace("{newUser1.Email}",newUser1.Email).Replace("{Password}",Password).Replace("{url}", _emailSettings.Url)
            }) ;

            var userMapped = newUser1.MapTo<UserManagementDto>();

            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(userMapped);

            var notification = new Notification()
            {
                To = newUser1.Email,
                Subject = "Nuevo Usuario Creado",
                Detail = $"Hola {newUser1.Name}, <br><br><br>Se ha creado exitosamente su usuario para el ingreso a nuestra aplicación Inspektor®. <br><br> A continuación enviamos su contraseña:<br><br> • Email : <B>{newUser1.Email}</B><br> • Contraseña : <B>{Password}</B><br> • URL de acceso:  {_emailSettings.Url} </B><br><br> <B>Importante</B><br> Este correo ha sido enviado automáticamente, favor no responder a esta dirección de correo, ya que no se encuentra habilitada para recibir mensajes. Si requiere mayor información sobre el contenido de este mensaje, contacte a su Oficial de Cumplimiento.<br><br><br> Cordialmente, <br><br> <B>Admin User Inspektor®</B><br> Software para la administración y gestión de listas",
                CompanyId = newUser1.CompanyId,
                Status = true,
                UserId = currentUserId,
                NotificationTypeId = 3,
                CreatedAt = DateTime.Now,
                json = jsonString
            };

            var newNotification = await _notificationRepository.Edit(notification, Session);            
            
            var response = new ResponseDTO<UserManagementDto>(userMapped);
            return response;
        }

        public async Task<ResponseDTO<UserManagementDto>> CreateByAdmin(UserManagementDto dto)
        {
            var errorsResponse = await validateFields(dto);

            if (errorsResponse != null && !errorsResponse.Succeeded)
            {
                return errorsResponse;
            }

            var currentUserId = Session.UserId;
            var currentUser = await _userService.GetById(currentUserId);
            var newUserData = dto.MapTo<TUser>();

            newUserData.CompanyId = Convert.ToInt32(dto.CompanyId);

            var newUser = await _userManagementRepository.Edit(newUserData, Session);
            var userMapped = newUser.MapTo<UserManagementDto>();
            var response = new ResponseDTO<UserManagementDto>(userMapped);

            return response;
        }
        public async Task<ResponseDTO<UserManagementDto>> Edit(UserManagementDto dto)
        {
            var errorsResponse = await validateFields(dto, false);

            if (errorsResponse != null && !errorsResponse.Succeeded)
            {
                return errorsResponse;
            }            

            var storedCurrentUser = await _userManagementRepository.Get(dto.Id, Session, true);
            var storedCurrentUserMapped = storedCurrentUser.MapTo<UserManagementDto>();

            dto.CompanyId = storedCurrentUserMapped.CompanyId;

            var user = dto.MapTo<TUser>();
            
            user.Password = storedCurrentUser.Password;            
            user.RelationshipNames = new string[] { "Permissions" };
            var updatedUser = await _userManagementRepository.Edit(user, Session);
            var userMapped = updatedUser.MapTo<UserManagementDto>();
            var response = new ResponseDTO<UserManagementDto>(userMapped);

            return response;
        }

        public async Task<ResponseDTO<UserManagementDto>> ResetPassword(UserManagementDto dto)
        {
            var currentUserId = Session.UserId;
            var currentUser = await _userService.GetById(currentUserId);            
            var storedCurrentUser = await _userManagementRepository.Get(dto.Id, Session, true);            

            string Password = Strings.CreateRandomString(12);

            //Asign Password to User
            String PasswordHash = await _authenticationService.GetHashPassword(storedCurrentUser.Email, Password);
            storedCurrentUser.Password = PasswordHash;
            storedCurrentUser.HasResetPassword = true;
            var newUser1 = await _userManagementRepository.Edit(storedCurrentUser, Session);

            //Send Email to User Created
            _sendEmail.Send(new EmailMessageRequestDto()
            {
                To = new List<string>()
                {
                    newUser1.Email
                },
                Subject = "Reinicio de Contraseña",
                Body = _emailSettings.PlantillaPasswordReset.Replace("{newUser1.Name}", newUser1.Name).Replace("{newUser1.Email}", newUser1.Email).Replace("{Password}", Password).Replace("{url}", _emailSettings.Url).Replace("{Anio}", DateTime.Now.Year.ToString())
            });

            var userMapped = newUser1.MapTo<UserManagementDto>();

            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(userMapped);

            var notification = new Notification()
            {
                To = newUser1.Email,
                Subject = "Reinicio de Contraseña",
                Detail = $"Hola {newUser1.Name}, <br><br><br>Se ha reiniciado exitosamente su contraseña para el ingreso a nuestra aplicación Inspektor®. <br><br> A continuación encontrará la información:<br><br> • Email : <B>{newUser1.Email}</B><br> • Contraseña : <B>{Password}</B><br> • URL de acceso:  {_emailSettings.Url} </B><br><br> <B>Importante</B><br> Este correo ha sido enviado automáticamente, favor no responder a esta dirección de correo, ya que no se encuentra habilitada para recibir mensajes. Si requiere mayor información sobre el contenido de este mensaje, contacte a su Oficial de Cumplimiento.<br><br><br> Cordialmente, <br><br> <B>Admin User Inspektor®</B><br> Software para la administración y gestión de listas",
                CompanyId = newUser1.CompanyId,
                Status = true,
                UserId = currentUserId,
                NotificationTypeId = 4,
                CreatedAt = DateTime.Now,
                json = jsonString
            };
            
            var newNotification = await _notificationRepository.Edit(notification, Session);

            var response = new ResponseDTO<UserManagementDto>(userMapped);

            return response;
        }

        public async Task<bool> Delete(int id)
        {
            await _userManagementRepository.Delete(id, Session);
            return true;
        }

        private async Task<ResponseDTO<UserManagementDto>> validateFields(UserManagementDto dto,
            bool includeExitingDto = true)
        {
            var response = new ResponseDTO<UserManagementDto>(null);

            if (dto == null ||
                string.IsNullOrEmpty(dto.Identification) ||
                string.IsNullOrEmpty(dto.Name) ||
                string.IsNullOrEmpty(dto.LastName) ||
                string.IsNullOrEmpty(dto.Login) ||
                string.IsNullOrEmpty(dto.Email)
            ) return null;

            var existingUser = await _userManagementRepository.GetExistingUser(dto, Session, includeExitingDto);

            if (existingUser == null) return response;

            response = new ResponseDTO<UserManagementDto>(null);
            var errorsDictionary = new Dictionary<string, dynamic>();

            const string email = "Este correo electrónico ya ha sido registrado";
            const string identification = "Esta identification ya ha sido registrada";
            const string login = "Este nombre de usuario ya ha sido registrado";

            if (string.Equals(existingUser.Email, dto.Email, StringComparison.CurrentCultureIgnoreCase))
                errorsDictionary["email"] = email;

            if (string.Equals(existingUser.Identification, dto.Identification,
                StringComparison.CurrentCultureIgnoreCase))
                errorsDictionary["identification"] = identification;

            if (string.Equals(existingUser.Login, dto.Login, StringComparison.CurrentCultureIgnoreCase))
                errorsDictionary["login"] = login;

            response.Errors = errorsDictionary;
            response.Succeeded = false;

            return response;
        }
        public async Task<ResponseDTO<List<UserManagementDto>>> GetAllByCompanyId(int CompanyId, bool includeDeleted = false)
        {
            var Users = await _userManagementRepository.GetAllByCompanyId(CompanyId, Session, includeDeleted);

            if (Users == null)
            {
                return new ResponseDTO<List<UserManagementDto>>(null) { Succeeded = false };
            }

            var usersMapped = Users.MapTo<List<UserManagementDto>>();
            var response = new ResponseDTO<List<UserManagementDto>>(usersMapped);

            return response;
        }
    }
}