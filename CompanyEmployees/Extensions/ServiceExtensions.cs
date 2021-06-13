using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEmployees.Extensions
{
    public static class ServiceExtensions
    {
        // Cross Origin Resource Sharing(CORS)
        // คือกลไกที่ทำให้เว็บเซิร์ฟเวอร์สามารถอนุญาต หรือไม่อนุญาต
        // การร้องขอทรัพยากรใดๆ ในหน้าเว็บ ที่ถูกเรียกมาจากโดเมนอื่น
        // ที่ไม่ใช่โดเมนที่หน้าเว็บนั้นอยู่
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });


        // IIS Configuration
        //ASP.NET Core applications are by default self-hosted, and if we want to host our
        //application on IIS, we need to configure an IIS integration which will eventually
        //help us with the deployment to IIS.To do that.
        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {
            });


        // Add for Logger
        public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddScoped<ILoggerManager, LoggerManager>();

    }
}
