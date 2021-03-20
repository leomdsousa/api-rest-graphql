using System;
using System.IO;
using System.Reflection;
using System.Text;
using API_Rest_GraphQl.Models.AppSettings;
using API_Rest_GraphQl.Models.Context;
using API_Rest_GraphQl.Repositorios;
using API_Rest_GraphQl.Repositorios.Interfaces;
using API_Rest_GraphQl.Services;
using API_Rest_GraphQl.Services.Interfaces;
using API_Rest_GraphQl.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace API_Rest_GraphQl
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Esse método é chamado pelo runtime. Use-o para add serviços ao container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddCors();
            services.AddControllers()
                    .AddNewtonsoftJson();

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

            services.AddSwaggerGen(config => 
            {
                config.SwaggerDoc("v1", new OpenApiInfo() { Title = "Doc v1", Version = "v1" } );

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                config.IncludeXmlComments(xmlPath, true);
                config.CustomSchemaIds(x => x.FullName);
            });

            services.AddScoped<Mapper>();

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

        // Esse método é chamado pelo runtime. Use-o para configurar o pipeline da requisição HTTP.
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

            app.UseSwagger();

            app.UseSwaggerUI(config => 
            {
                config.RoutePrefix = "swagger";
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Doc v1");

            });
        }
    }
}
