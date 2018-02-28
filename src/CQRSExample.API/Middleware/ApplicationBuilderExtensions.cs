using Microsoft.AspNetCore.Builder;

namespace CQRSExample.API.Middleware
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CQRSExample API V1"));
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
