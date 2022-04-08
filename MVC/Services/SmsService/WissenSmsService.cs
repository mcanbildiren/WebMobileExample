using MVC.Models;
using System.Diagnostics;

namespace MVC.Services.SmsService
{
    public class WissenSmsService : ISmsService
    {
        public string EndPoint { get; set; } = "https://wissenakademie.com";
        public SmsStates Send(SmsModel model)
        {
            Debug.WriteLine($"Wissen: {model.TelefonNo} - {model.Mesaj}");
            return SmsStates.Sent;
        }
    }
}
