using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services.SmsService;
using System.Diagnostics;
using MVC.Services.EmailService;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISmsService _smsService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IServiceProvider _serviceProvider;

        public HomeController(ISmsService smsService,
            IEmailService emailService,
            IWebHostEnvironment appEnvironment,
            IServiceProvider serviceProvider)
        {
            _smsService = smsService;
            _emailService = emailService;
            _appEnvironment = appEnvironment;
            _serviceProvider = serviceProvider;
        }

        public IActionResult Index(int id = 0)
        {
            var result = _smsService.Send(new SmsModel()
            {
                TelefonNo = "12345",
                Mesaj = "home/index çalıştı"
            });

            var wissenSms = (WissenSmsService)_smsService;
            Debug.WriteLine(wissenSms.EndPoint);

            #region Factory Design Pattern Uygulaması

            IEmailService emailService;
            if (id % 2 == 0)
            {
                emailService = _serviceProvider.GetService<SendGridEmailService>();

            }
            else
            {
                emailService = _serviceProvider.GetService<OutlookEmailService>();
            }

            #endregion


            //var fileStream = new FileStream(@$"{_appEnvironment.WebRootPath}\files\portre.jpeg", FileMode.Open);

            emailService.SendMailAsync(new MailModel()
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
                Body = "Bu emailin body kısmıdır",
                //Attachs = new List<Stream>()
                //{
                //    fileStream
                //}
            });
            //fileStream.Close();

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