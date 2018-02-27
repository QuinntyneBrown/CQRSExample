using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQRSExample.Web.Configuration
{
    internal static class Options
    {
        internal static void LoadConfigurationOptions(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthenticationSettings>(configuration.GetSection("Authentication"));
        }
    }
}
