using CompanyEmployees.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEmployees
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            // Add Path of Nlog
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),"/nlog.config"));
            //---------------->
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add
            // use with Extension > ServiceExtension.cs
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureLoggerService();
            //----------------------------------------->
            // This method registers only the controllers in IServiceCollection
            // and not Views or Pages  because they are not required in the Web API project
            // which we are building. In the Configure method
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Add
            else
            {
                app.UseHsts();  //will add middleware for using HSTS, which adds theStrict - Transport - Security header.
            }
            //----->

            app.UseHttpsRedirection();

            // Add
            // enables using static files for the request. If we don’t set a path to the static files directory, it will use a wwwroot folder in our project by default.
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            // forward proxy headers to the current request. This will help us during application deployment.
                        app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            // add routing and authorization features to our application, respectively
            app.UseRouting();
            app.UseAuthorization();
            //------------------------------>

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
