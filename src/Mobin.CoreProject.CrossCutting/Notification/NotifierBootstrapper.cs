using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mobin.Components.Notifier.Client;
using Mobin.CoreProject.CrossCutting.Notification.Services;

namespace Mobin.CoreProject.CrossCutting.Notification
{
    public static class NotifierBootstrapper
    {
        public static void RegisterNotifier(this IServiceCollection services, IConfiguration configuration)
        {
            var config = new NotifierConfig();

            configuration.Bind(nameof(NotifierConfig), config);
            services.AddSingleton(config);

            services.AddSingleton<SmsProxy>();
            services.AddSingleton<ISmsService, SmsServiceByComponent>();

            services.AddSingleton<EmailProxy>();
            services.AddSingleton<IEmailService, EmailServiceByComponent>();
        }
    }
}
