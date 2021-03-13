using System.Text;
using API_Rest_GraphQl.Models.AppSettings;
using API_Rest_GraphQl.Models.Context;
using API_Rest_GraphQl.Repositorios;
using API_Rest_GraphQl.Repositorios.Interfaces;
using API_Rest_GraphQl.Services;
using API_Rest_GraphQl.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace API_Rest_GraphQl
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddCors();
            services.AddControllers();

            var settings = Configuration.GetSection("AppSettings");
            var key = Encoding.ASCII.GetBytes(settings.GetSection("Configuration").GetSection("Secret").Value);
            
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.IncludeErrorDetails = true;
                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            IdentityModelEventSource.ShowPII = true;

            //CONTEXT
            services.AddDbContext<BibliotecaContext>(opt => opt.UseInMemoryDatabase("Biblioteca"));

            //SERVICES
            services.AddScoped<ISeedingService, SeedingService>();
            services.AddScoped<ILivroService, LivroService>();
            services.AddScoped<ITokenService, TokenService>();

            //REPOSITORIOS
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
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

            app.UseCors(config => config
                .AllowAnyOrigin()
                .AllowAnyOrigin()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
