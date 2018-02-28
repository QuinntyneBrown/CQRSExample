using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSExample.WebAPI.Middleware
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Platform API V1"));
            return app;
        }

        public static IApplicationBuilder UseSecurity(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseCors("CorsPolicy");
            return app;
        }
    }
}
