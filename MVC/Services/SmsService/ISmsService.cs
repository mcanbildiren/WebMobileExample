using MVC.Models;

namespace MVC.Services.SmsService
{
    public interface ISmsService
    {
        SmsStates Send(SmsModel model);
    }
}
