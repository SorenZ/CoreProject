using System.Collections.Generic;
using System.Threading.Tasks;
using Alamut.Data.Structure;

namespace Mobin.CoreProject.CrossCutting.Notification.Services
{
    public interface IEmailService
    {
        Task<ServiceResult> SendAsync(string title, string to, string body);

        Task<ServiceResult> SendAsync(string title, IEnumerable<string> to, IEnumerable<string> cc,
            string body,
            IEnumerable<string> attachmentFileAddress);
    }
}