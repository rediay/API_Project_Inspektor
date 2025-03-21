using Common.DTO;
using Common.Services.Infrastructure.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
namespace Common.Services.Mail
{
    public class EMailService : IEMailService
    {
        private readonly MailboxAddress mailboxAddress;
        private readonly SmtpClient client;
        private readonly string host, userName, password, mailAddress, emailName;
        private readonly int port;
        private readonly SecureSocketOptions encryptionType;

        public EMailService(IConfiguration configuration)
        {

            var EmailConfig = configuration.GetSection("EmailConfig");
            host = EmailConfig["Host"].ToString();
            port = Convert.ToInt32(EmailConfig["Port"]);
            userName = EmailConfig["Username"].ToString();
            password = EmailConfig["Password"].ToString();
            mailAddress = EmailConfig["MailAdress"].ToString();
            encryptionType = bool.Parse(EmailConfig["EnableSSL"]) ? SecureSocketOptions.StartTls : SecureSocketOptions.None;
            emailName = EmailConfig["EmailName"].ToString();
            mailboxAddress = new MailboxAddress(emailName, mailAddress);
            client = new SmtpClient();
        }

        public async Task<string> SendMail(EmailMessageRequestDto emailMessage)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(mailboxAddress);

            switch (emailMessage.EmailType)
            {
                case 1: // Usuario Creado
                    message.Subject = "Usuario Creado";
                    emailMessage.Body = "El usuario ha sido creado con éxito.";
                    break;
                case 2: // Usuario Creado
                    message.Subject = "Usuario Editado";
                    emailMessage.Body = "El usuario ha sido creado con éxito.";
                    break;
                default:
                    break;
            }

            emailMessage.To.ForEach(y => message.To.Add(MailboxAddress.Parse(y)));
            emailMessage.ToCC.ForEach(y => message.Cc.Add(MailboxAddress.Parse(y)));

            var body = new TextPart("plain")
            {
                Text = emailMessage.Body
            };

            var multipart = new Multipart("mixed");
            multipart.Add(body);

            var attachments = new List<MimePart>();

            if (emailMessage.AttachmentsRoutes.Count > 0)
            {
                emailMessage.AttachmentsRoutes.ForEach(x => attachments.Add(new MimePart("text", "txt")
                {
                    Content = new MimeContent(File.OpenRead(x)),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = Path.GetFileName(x)
                }));
                attachments.ForEach(x => multipart.Add(x));
            }

            message.Body = multipart;
            try
            {
                //connects to the gmail smtp server using port 465 with SSL enabled
                client.Connect(host, port, encryptionType);
                //needed if the SMTP server requires authentication, like gmail for example                

                client.Authenticate(userName, password);

                return client.Send(message);

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
