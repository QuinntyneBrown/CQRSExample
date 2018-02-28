using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CQRSExample.Web.Services;
using CQRSExample.API.Services;
using CQRSExample.API.Middleware;
using CQRSExample.Infrastructure.Services;
using CQRSExample.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace CQRSExample.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSecurity(Configuration);
            services.AddCustomConfiguration(Configuration);
            services.AddHttpClient();
            services.AddDataStores(Configuration["Data:DefaultConnection:ConnectionString"]);
            services.AddCustomSwagger();
            services.AddCustomMediator();
            services.AddCustomCache();
            services.AddSignalR();
            services.AddCustomMvc();
        }

        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, IEncryptionService encryptionService, AppDataContext context, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseSecurity();
            app.UseMvc();
            app.UseSignalR(routes => routes.MapHub<AppHub>("/appHub"));
            app.UseCustomSwagger();
            
        }
    }
}
