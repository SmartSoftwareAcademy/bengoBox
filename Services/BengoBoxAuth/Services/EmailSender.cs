using MailKit.Net.Smtp;
using MimeKit;
using Nest;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BengoBoxAuth.Services;
//using System.Web.Http.Results;

namespace BengoBoxAuth.Services
{
    public class EmailSender : IMailSender
    {
        private readonly EmailConfiguration _configuration;

        public EmailSender(EmailConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }
        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage=new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(null,_configuration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject=message.Subject;
            //emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text=string.Format("<h4 style='color:black;'>{0}</h4>",message.Body)};

            var bodyBuilder = new BodyBuilder { HtmlBody = string.Format("<h2 style='color:red;'>{0}</h2>", message.Body) };
            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }
            emailMessage.Body = bodyBuilder.ToMessageBody();

            return emailMessage;
        }

        private string Send(MimeMessage mimeMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_configuration.EmailServer, _configuration.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_configuration.Username, _configuration.Password);
                    client.Send(mimeMessage);
                    Console.WriteLine("Email Sent!");
                    return "Email sent successfully!";
                }
                catch (Exception ex)
                {
                 Console.WriteLine(ex.Message.ToString());
                    return "Email send Failded!";
                }
                finally{
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}