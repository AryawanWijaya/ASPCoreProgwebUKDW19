using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPCoreGroupB.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ASPCoreGroupB
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //menambahkan mvc
            services.AddMvc();
            services.AddScoped<IMahasiswa,MahasiswaDAL>();//sebenarnya yang dipanggil adalah MahasiswaDAL di controller
            // cmn krn di daftarkan pake AddScoped maka bisa memakai IMahasiswanya yang dipakai di controller
            // akur kerja: IMahasiswa di implement dg MahasiswaDAL(inheritance)
            //kemudian didaftarkan seperti gambar diatas
            //dan yang dipanggil di controller adalah IMahasiswanya
            services.AddScoped<IDosen,DosenDAL>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //menambahkan file static di wwwwroot
            app.UseStaticFiles();
            //untuk menambahkan mvc pattern
            app.UseMvcWithDefaultRoute();

            /* app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World !");
            });*/
        }
    }
}
