﻿using Microsoft.AspNetCore.Builder;
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
using Microsoft.OpenApi.Models;
using ProdavnicaERP.Data;
using ProdavnicaERP.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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




            //konfiguracije za automaper - za svako mapiranje se definise profil u tom profilu iz tog objekta mi mapiraj taj objekat na takav nacin
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


            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("WEBSHOPApiSpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "WEBSHOP API",
                        Version = "1",
                        Description = "Pomoću ovog API-ja može da se vrši dodavanje, modifikacija i brisanje , kao i pregled svih kreiranih javnih nadmetanja.",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Dajana Jelic",
                            Email = "dajanajelic05@gmail.com",
                            Url = new Uri(Configuration["Uri:Ftn"])
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense
                        {
                            Name = "FTN licence",
                            Url = new Uri(Configuration["Uri:Ftn"])
                        },
                        TermsOfService = new Uri(Configuration["Uri:Ftn"])
                    });
                var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";//refleksija
                //omogucava da manipulisemo sa putanjom
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);
                //setupAction.IncludeXmlComments(xmlCommentsPath);
            });
            services.AddDbContextPool<WEBSHOPContext>(options => options.UseSqlServer(Configuration.GetConnectionString("WEBSHOP")));
        

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
            app.UseRouting();
            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {

                //Podesavamo endpoint gde Swagger UI moze da pronadje OpenAPI specifikaciju
                setupAction.SwaggerEndpoint("/swagger/WEBSHOPOpenApiSpecification/swagger.json", "WEBSHOP API");

                setupAction.RoutePrefix = "swagger"; //Dokumentacija ce sada biti dostupna na root-u (ne mora da se pise /swagger)
            });

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllers();
            });
        }
    }
}