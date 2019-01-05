using System.Collections.Generic;
using System.Threading.Tasks;
using Alamut.Data.Structure;
using Mobin.Components.Notifier.Client;

namespace Mobin.CoreProject.CrossCutting.Notification.Services
{
    public class SmsServiceByComponent : ISmsService
    {
        private readonly SmsProxy _sms;

        public SmsServiceByComponent(SmsProxy sms)
        {
            _sms = sms;
        }

        public async Task<ServiceResult> SendAsync(string to, string body, string consumer) =>
            await _sms.Send(to, body, consumer);

        public async Task<ServiceResult> SendAsync(string to, string body, string sarshomare, string consumer) =>
            await _sms.Send(to, body, sarshomare, consumer);

        public async Task<ServiceResult> SendAsync(IEnumerable<string> to, IEnumerable<string> body, string sarshomare,
            string consumer) =>
            await _sms.Send(to, body, sarshomare, consumer);

    }
}
