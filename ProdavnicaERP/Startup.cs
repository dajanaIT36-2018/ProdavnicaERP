using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using ProdavnicaERP.Data;
using ProdavnicaERP.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProdavnicaERP
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
            //Enable CORS
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            //JSON Serializer
            //services.AddControllersWithViews().AddNewtonsoftJson(options =>
            //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
            //    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
            //    = new DefaultContractResolver());

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddControllers(setup =>
                setup.ReturnHttpNotAcceptable = true
            ).AddXmlDataContractSerializerFormatters()

            .ConfigureApiBehaviorOptions(setupAction => //Deo koji se odnosi na podržavanje Problem Details for HTTP APIs
             {
                 setupAction.InvalidModelStateResponseFactory = context =>
                 {
                     //Kreiramo problem details objekat
                     ProblemDetailsFactory problemDetailsFactory = context.HttpContext.RequestServices
                         .GetRequiredService<ProblemDetailsFactory>();

                     //Prosleđujemo trenutni kontekst i ModelState, ovo prevodi validacione greške iz ModelState-a u RFC format
                     ValidationProblemDetails problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                         context.HttpContext,
                         context.ModelState);

                     //Ubacujemo dodatne podatke
                     problemDetails.Detail = "Pogledajte polje errors za detalje.";
                     problemDetails.Instance = context.HttpContext.Request.Path;

                     //po defaultu se sve vraća kao status 400 BadRequest, to je ok kada nisu u pitanju validacione greške,
                     //ako jesu hoćemo da koristimo status 422 UnprocessibleEntity
                     //tražimo info koji status kod da koristimo
                     var actionExecutiongContext = context as ActionExecutingContext;

                     //proveravamo da li postoji neka greška u ModelState-u, a takođe proveravamo da li su svi prosleđeni parametri dobro parsirani
                     //ako je sve ok parsirano ali postoje greške u validaciji hoćemo da vratimo status 422
                     if ((context.ModelState.ErrorCount > 0) &&
                         (actionExecutiongContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count))
                     {
                         problemDetails.Type = "https://google.com"; //inače treba da stoji link ka stranici sa detaljima greške
                         problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                         problemDetails.Title = "Došlo je do greške prilikom validacije.";

                         //sve vraćamo kao UnprocessibleEntity objekat
                         return new UnprocessableEntityObjectResult(problemDetails)
                         {
                             ContentTypes = { "application/problem+json" }
                         };
                     };

                     //ukoliko postoji nešto što nije moglo da se parsira hoćemo da vraćamo status 400 kao i do sada
                     problemDetails.Status = StatusCodes.Status400BadRequest;
                     problemDetails.Title = "Došlo je do greške prilikom parsiranja poslatog sadržaja.";
                     return new BadRequestObjectResult(problemDetails)
                     {
                         ContentTypes = { "application/problem+json" }
                     };
                 };
             });
            services.AddRazorPages();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IKorisnikRepository, KorisnikRepository >();
            services.AddScoped<IKorpaRepository, KorpaRepository>();
            services.AddScoped< IPorudzbinaRepository, PorudzbinaRepository> ();
            services.AddScoped<IProizvodjacRepository, ProizvodjacRepository>();
            services.AddScoped<IProizvodRepository, ProizvodRepository>();
            services.AddScoped<IStatusPorudzbineRepository, StatusPorudzbineRepository>();
            services.AddScoped<IStavkaKorpeRepository, StavkaKorpeRepository>();
            services.AddScoped<IStavkaPorudzbineRepository, StavkaPorudzbineRepository>();
            services.AddScoped<ITipKorisnikaRepository, TipKorisnikaRepository>();
            services.AddScoped<ITipProizvodaRepository, TipProizvodaRepository>();
            services.AddScoped<IVrstaProizvodaRepository, VrstaProizvodaRepository>();


            services.AddDbContextPool<WEBSHOPContext>(options => options.UseSqlServer(Configuration.GetConnectionString("WEBSHOP")));

        }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Enable CORS
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Došlo je do neočekivane greške. Molimo pokušajte kasnije.");
                    });
                });
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
