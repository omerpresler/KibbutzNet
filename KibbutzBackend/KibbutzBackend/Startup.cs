using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;

namespace KibbutzBackend;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.Extensions;

public class Startup
{
    public void Configure(IApplicationBuilder app)
    {
    
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        });
      
    }
}