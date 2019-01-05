using System.Collections.Generic;
using System.Threading.Tasks;
using Alamut.Data.Structure;
using Mobin.Components.Notifier.Client;

namespace Mobin.CoreProject.CrossCutting.Notification.Services
{
    public class EmailServiceByComponent : IEmailService
    {
        private readonly EmailProxy _emailProxy;

        public EmailServiceByComponent(EmailProxy emailProxy)
        {
            _emailProxy = emailProxy;
        }

        public Task<ServiceResult> SendAsync(string title, string to, string body) =>
            _emailProxy.Send(title, to, body);



        public Task<ServiceResult> SendAsync(string title, IEnumerable<string> to, IEnumerable<string> cc,
            string body, IEnumerable<string> attachmentFileAddress) =>
            _emailProxy.Send(title, to, cc, body, attachmentFileAddress);
    }
}