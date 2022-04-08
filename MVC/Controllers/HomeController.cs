using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services.EmailService;
using MVC.Services.SmsService;
using System.Diagnostics;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISmsService _smsService;
        private readonly IEmailService _emailService;

        public HomeController(ISmsService smsService, IEmailService emailService)
        {
            _smsService = smsService;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            var result = _smsService.Send(new SmsModel()
            {
                TelefonNo = "12345",
                Mesaj = "home/index çalıştı"
            });

            var wissenSms = (WissenSmsService)_smsService;
            Debug.WriteLine(wissenSms.EndPoint);

            _emailService.SendMailAsync(new MailModel()
            {
                To = new List<EmailModel>()
                {
                    new EmailModel()
                    {
                        Name = "Wissen",
                        Adress = "site@wissenakademie.com"
                    }
                },
                Subject = "Index Açıldı",
                Body = "Bu emailin body kısmıdır"
            });

            return View();
        }

        public IActionResult Privacy()
        {
            var result = _smsService.Send(new SmsModel()
            {
                TelefonNo = "12345",
                Mesaj = "home/index çalıştı"
            });

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}