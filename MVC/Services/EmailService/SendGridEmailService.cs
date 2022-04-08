using MVC.Models;
using MVC.Services.EmailService;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MVC.Services.EmailService
{
    public class SendGridEmailService : IEmailService
    {
        private readonly ISendGridClient _sendGridClient;

        public SendGridEmailService(ISendGridClient sendGridClient)
        {
            _sendGridClient = sendGridClient;
        }

        public Task SendMailAsync(MailModel model)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("wissen.akademie@outlook.com", "Wissen Web Mobile 8"),
                Subject = model.Subject
            };
            msg.AddContent(MimeType.Html, model.Body);
            foreach (var emailModel in model.To)
            {
                msg.AddTo(new EmailAddress(emailModel.Adress, emailModel.Name));
            }
            foreach (var emailModel in model.Cc)
            {
                msg.AddCc(new EmailAddress(emailModel.Adress, emailModel.Name));
            }
            foreach (var emailModel in model.Bcc)
            {
                msg.AddBcc(new EmailAddress(emailModel.Adress, emailModel.Name));
            }
            return _sendGridClient.SendEmailAsync(msg);
        }
    }
}