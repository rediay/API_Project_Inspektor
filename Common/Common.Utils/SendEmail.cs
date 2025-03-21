using Common.DTO;
using DocumentFormat.OpenXml.Vml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using System.Configuration;
using Common.Services.Infrastructure.Services;

namespace Common.Utils
{
    public sealed class EmailSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string MailAdress { get; set; }
        public string EnableSSL { get; set; } = "false";
        public string EmailName { get; set; }                
        public string Url { get; set; }
        public string PlantillaUserCreated { get; set; }
        public string PlantillaPasswordReset { get; set; }

    }


    public class SendEmail : ISendEmail
    {
        private readonly MailboxAddress _mailboxAddress;
        private readonly SmtpClient _client;                
        private readonly SecureSocketOptions _encryptionType;

        private readonly EmailSettings _emailSettings;

        public SendEmail(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
            _encryptionType = bool.Parse(_emailSettings.EnableSSL) ? SecureSocketOptions.StartTls : SecureSocketOptions.None;
            _mailboxAddress = new MailboxAddress("Risk Consulting", _emailSettings.MailAdress);
            _client = new SmtpClient();                          
        }

        public bool Send(EmailMessageRequestDto emailMessage)
        {            
            try
            {
                MimeMessage message = new MimeMessage();
                message.From.Add(_mailboxAddress);
                emailMessage.To.ForEach(y => message.To.Add(MailboxAddress.Parse(y)));
                message.Subject = emailMessage.Subject;

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = emailMessage.Body;
                message.Body = bodyBuilder.ToMessageBody();
                
                //connects to the gmail smtp server using port 465 with SSL enabled
                _client.Connect(_emailSettings.Host, _emailSettings.Port, _encryptionType);
                //needed if the SMTP server requires authentication, like gmail for example                
                _client.Authenticate(_emailSettings.UserName, _emailSettings.Password);
                _client.Send(message);

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                _client.Disconnect(true);
                _client.Dispose();
            }
        }

    }
}
