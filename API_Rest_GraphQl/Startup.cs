using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Rest_GraphQl.Models.AppSettings;
using API_Rest_GraphQl.Models.Context;
using API_Rest_GraphQl.Repositorios;
using API_Rest_GraphQl.Repositorios.Interfaces;
using API_Rest_GraphQl.Services;
using API_Rest_GraphQl.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API_Rest_GraphQl
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddControllers();

            services.AddAntiforgery();

            //CONTEXT
            services.AddDbContext<BibliotecaContext>(opt => opt.UseInMemoryDatabase("Biblioteca"));

            //SERVICES
            services.AddScoped<ISeedingService, SeedingService>();
            services.AddScoped<ILivroService, LivroService>();

            //REPOSITORIOS
            services.AddScoped<ILivroRepository, LivroRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedingService seedingService)
        {
            seedingService.ObterLivros();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
