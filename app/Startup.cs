using app.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup( IConfiguration configuration ) => Configuration = configuration;
        public void ConfigureServices( IServiceCollection services )
        {
            //Добавляем appSettings.json
            Configuration.Bind( "Project", new Config() );

            //Добавляем поддержку контроллеров и представлений MVC
            services.AddControllersWithViews()
               //Выставляем совместимость с  asp.net core 3.0
               .SetCompatibilityVersion( CompatibilityVersion.Version_3_0 ).AddSessionStateTempDataProvider();
        }

       
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            if( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //поддержка статических файлов
            app.UseStaticFiles();

            //регистрация маршрутов
            app.UseEndpoints( endpoints =>
             {
                 endpoints.MapControllerRoute( "default", "{controller=Home}/{action=Index}/{id?}" );
             } );
        }
    }
}
