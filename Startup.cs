using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentsAPI.Middlewares;
using StudentsAPI.Services.v1;

namespace StudentsAPI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IStudentsService, StudentsService>();
            services.AddSingleton<Services.v2.IStudentsService, Services.v2.StudentsService>();
            services.AddSingleton<Services.v2.IEventsService, Services.v2.EventsService>();
            services.AddControllers().AddXmlSerializerFormatters();

            services.AddApiVersioning(options =>
           {
               options.ReportApiVersions = true;
               options.AssumeDefaultVersionWhenUnspecified = true;
               options.DefaultApiVersion = new ApiVersion(1, 0);
               options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(a => a.Run(async context =>
                {
                    await context.Response.WriteAsync("Unexpected server error. Please contact admin@localhost.com.");
                }));
            }

            app.UseMiddleware<ApiKeyValidationMiddleware>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
