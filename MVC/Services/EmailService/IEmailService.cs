using MVC.Models;

namespace MVC.Services.EmailService
{
    public interface IEmailService
    {
        Task SendMailAsync(MailModel model);
    }
}