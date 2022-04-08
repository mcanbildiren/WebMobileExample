using MVC.Models;
using MVC.Services.EmailService;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MVC.Services.EmailService
{
    public class OutlookEmailService : IEmailService
    {
        public string SenderMail => "wissen.akademie@outlook.com";
        public string Password => "1-9x2+abc";
        public string Smtp => "smtp-mail.outlook.com";
        public int SmtpPort => 587;

        public Task SendMailAsync(MailModel model)
        {
            var mail = new MailMessage { From = new MailAddress(this.SenderMail) };

            foreach (var c in model.To)
            {
                mail.To.Add(new MailAddress(c.Adress, c.Name));
            }

            foreach (var cc in model.Cc)
            {
                mail.CC.Add(new MailAddress(cc.Adress, cc.Name));
            }
            foreach (var cc in model.Bcc)
            {
                mail.Bcc.Add(new MailAddress(cc.Adress, cc.Name));
            }

            if (model.Attachs is { Count: > 0 })
            {
                foreach (var attach in model.Attachs)
                {
                    var fileStream = attach as FileStream;
                    var info = new FileInfo(fileStream.Name);

                    mail.Attachments.Add(new Attachment(attach, info.Name));
                }
            }

            mail.IsBodyHtml = true;
            mail.BodyEncoding = Encoding.UTF8;
            mail.SubjectEncoding = Encoding.UTF8;
            mail.HeadersEncoding = Encoding.UTF8;

            mail.Subject = model.Subject;
            mail.Body = model.Body;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var smtpClient = new SmtpClient(this.Smtp, this.SmtpPort)
            {
                Credentials = new NetworkCredential(this.SenderMail, this.Password),
                EnableSsl = true
            };
            return smtpClient.SendMailAsync(mail);
        }
    }
}