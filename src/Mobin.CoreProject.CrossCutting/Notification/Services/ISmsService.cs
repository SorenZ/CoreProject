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
}
