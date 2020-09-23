using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Petshop.core.ApplicationServices;
using Petshop.core.DomainServices;
using Petshop.Core.Entity;
using System;
using System.Reflection;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Petshop.Infrastructure.Db.Data;
using Petshop.Infrastructure.Db.Data.Repositories;

namespace PetShop_RestAPI
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v4",
                    Title = "Petshop API",
                    Description = "A simple example ASP.NET Core Web API",
                   // TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "scupak",
                        Email = string.Empty,
                        //Url = new Uri("https://twitter.com/spboyer"),
                    },
                    
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });


            services.AddDbContext<Context>(
                opt => opt.UseSqlite("Data Source=PetShop.db"));

            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<IPetTypeRepository, PetTypeRepository>();
            services.AddScoped<IPetTypeService, PetTypeService>();

            services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                o.SerializerSettings.MaxDepth = 5;

            } );
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;

            });
          

            if (env.IsDevelopment())
            { 
                app.UseDeveloperExceptionPage();

                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<Context>();

                    var petType1 = context.PetTypes.Add(new PetType()
                    {
                        name = "cat"


                    }).Entity;

                    var pet1 = context.Pets.Add(new Pet()
                    {


                        Name = "Jerry",
                        Birthdate = DateTime.Now.AddYears(-12),
                        Color = "Blue",
                        PetType = petType1,
                        Price = 50,
                        SoldDate = DateTime.Now.AddYears(-2),

                    }).Entity;

                    context.Pets.Add(new Pet()
                    {
                        Name = "jake",
                        Birthdate = DateTime.Now.AddYears(-12),
                        Color = "Blue",
                        Price = 50,
                        SoldDate = DateTime.Now.AddYears(-2),
                    });

                    context.SaveChanges();
                }


            }

            if (env.IsDevelopment())
            {

                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var petRepo = scope.ServiceProvider.GetService<IPetRepository>();
                    var ownerRepo = scope.ServiceProvider.GetService<IOwnerRepository>();
                    var petTypeRepo = scope.ServiceProvider.GetService<IPetTypeRepository>();
                    var context = scope.ServiceProvider.GetService<Context>();

                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    var petType1 = context.PetTypes.Add(new PetType()
                    {
                        name = "cat"


                    }).Entity;

                    var pet1 = context.Pets.Add(new Pet()
                    {


                        Name = "Jerry",
                        Birthdate = DateTime.Now.AddYears(-12),
                        Color = "Blue",
                        PetType = petType1,
                        Price = 50,
                        SoldDate = DateTime.Now.AddYears(-2),

                    }).Entity;

                    context.Pets.Add(new Pet()
                    {
                        Name = "jake",
                        PetType = petType1,
                        Birthdate = DateTime.Now.AddYears(-12),
                        Color = "Blue",
                        Price = 50,
                        SoldDate = DateTime.Now.AddYears(-2),
                    });

                    context.SaveChanges();

                }

                // new DataInitializer(petRepo, ownerRepo, petTypeRepo).InitData(); 
            }
            //  }

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
