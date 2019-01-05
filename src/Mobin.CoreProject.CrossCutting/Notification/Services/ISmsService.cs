using System.Collections.Generic;
using System.Threading.Tasks;
using Alamut.Data.Structure;

namespace Mobin.CoreProject.CrossCutting.Notification.Services
{
    public interface ISmsService
    {
        Task<ServiceResult> SendAsync(string to, string body, string consumer);

        Task<ServiceResult> SendAsync(string to, string body, string sarshomare, string consumer);

        Task<ServiceResult> SendAsync(IEnumerable<string> to, IEnumerable<string> body, string sarshomare, string consumer);
    }

    public interface IEmailService
    {
        Task<ServiceResult> SendAsync(string title, string to, string body);

        Task<ServiceResult> SendAsync(string title, IEnumerable<string> to, IEnumerable<string> cc,
            string body,
            IEnumerable<string> attachmentFileAddress);
    }
}
