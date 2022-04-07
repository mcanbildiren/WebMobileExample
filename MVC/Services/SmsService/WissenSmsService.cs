using MVC.Models;
using System.Diagnostics;

namespace MVC.Services.SmsService
{
    public class WissenSmsService : ISmsService
    {
        public SmsStates Send(SmsModel model)
        {
            Debug.Write($"Wissen: {model.TelefonNo} - {model.Mesaj}");
            return SmsStates.Sent;
        }
    }
}
