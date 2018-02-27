using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace CQRSExample.Web.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            Configuration.Options.LoadConfigurationOptions(services, configuration);
            return services;
        }

        public static IServiceCollection AddCustomMediator(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
            services.AddScoped<IMediator, AppMediator>();
            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Info
                {
                    Title = "CQRS Example",
                    Version = "v1",
                    Description = "CQRS Example .NET Core REST API",
                });
                options.CustomSchemaIds(x => x.FullName);
            });

            return services;
        }
    }
}
