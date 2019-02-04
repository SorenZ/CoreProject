using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Mobin.CoreProject.CrossCutting.Notification.Services;

namespace Mobin.CoreProject.CrossCutting.Security
{
    public class MessageSender : IEmailSender
    {
        private readonly IEmailService _emailService;

        public MessageSender(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            await _emailService.SendAsync(subject, email, message);
        }
    }
}