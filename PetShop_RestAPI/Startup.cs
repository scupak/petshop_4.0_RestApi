using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Petshop.core.ApplicationServices;
using Petshop.core.DomainServices;
using Petshop.Core.Entity;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Petshop.core.ApplicationServices.impl;
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
            // Create a byte array with random values. This byte array is used
            // to generate a key for signing JWT tokens.
            Byte[] secretBytes = new byte[40];
            Random rand = new Random();
            rand.NextBytes(secretBytes);

            // Add JWT based authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    //ValidAudience = "TodoApiClient",
                    ValidateIssuer = false,
                    //ValidIssuer = "TodoApi",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
                    ValidateLifetime = true, //validate the expiration and not before values in the token
                    ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                };
            });

            //CORS configuration 
            services.AddCors(options => options.AddPolicy("AllowEverything", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

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
                opt =>
                {
                    opt.UseSqlite("Data Source=PetShop.db").EnableSensitiveDataLogging();
                },ServiceLifetime.Transient);

            // Register the AuthenticationHelper in the helpers folder for dependency
            // injection. It must be registered as a singleton service. The AuthenticationHelper
            // is instantiated with a parameter. The parameter is the previously created
            // "secretBytes" array, which is used to generate a key for signing JWT tokens,
            services.AddSingleton<IAuthenticationHelper>(new
                AuthenticationHelper(secretBytes));


            

            services.AddScoped<IColourRepository, ColourRepository>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<IPetTypeRepository, PetTypeRepository>();
            services.AddScoped<IPetTypeService, PetTypeService>();

            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IUserService, UserService>();
            


            services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                o.SerializerSettings.MaxDepth = 5;

            } );

            services.AddControllers();

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
          
            /*
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
            */
            if (env.IsDevelopment())
            {

                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var petRepo = scope.ServiceProvider.GetService<IPetRepository>();
                    var ownerRepo = scope.ServiceProvider.GetService<IOwnerRepository>();
                    var petTypeRepo = scope.ServiceProvider.GetService<IPetTypeRepository>();
                    var authenticationHelper = scope.ServiceProvider.GetService<IAuthenticationHelper>();

                    var context = scope.ServiceProvider.GetService<Context>();
                    

                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    var colour = new Colour{
                        Name = "blue"

                    };

                    var petType1 = context.PetTypes.Add(new PetType()
                    {
                        name = "cat"


                    }).Entity;

                    var pet1 =new Pet{


                        Name = "merry",
                        Birthdate = DateTime.Now.AddYears(-12),
                        Color = "Blue",
                        PetType = petType1,
                        Price = 50,
                        SoldDate = DateTime.Now.AddYears(-2),

                    };

                    pet1.ColourPets = new List<ColourPet>
                    {
                        new ColourPet
                        {
                            Pet = pet1,
                            Colour = colour
                        }
                    };

                    context.Pets.Add(pet1);

                    context.Pets.Add(new Pet()
                    {
                        Name = "jake",
                        PetType = petType1,
                        Birthdate = DateTime.Now.AddYears(-12),
                        Color = "Blue",
                        Price = 50,
                        SoldDate = DateTime.Now.AddYears(-2),
                    });


                    string password = "1234";
                    byte[] passwordHashJoe, passwordSaltJoe, passwordHashAnn, passwordSaltAnn;

                   
                    authenticationHelper.CreatePasswordHash(password, out passwordHashJoe, out passwordSaltJoe);
                    authenticationHelper.CreatePasswordHash(password, out passwordHashAnn, out passwordSaltAnn);

                    context.Users.Add(new User()
                    {
                        Username = "UserJoe",
                            PasswordHash = passwordHashJoe,
                            PasswordSalt = passwordSaltJoe,
                            IsAdmin = false
                      
                    });


                    context.Users.Add(new User()
                    {
                        
                            Username = "AdminAnn",
                            PasswordHash = passwordHashAnn,
                            PasswordSalt = passwordSaltAnn,
                            IsAdmin = true
                       
                    });

                    context.SaveChanges();

                }
           
                // new DataInitializer(petRepo, ownerRepo, petTypeRepo).InitData(); 
            }
            else
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var petRepo = scope.ServiceProvider.GetService<IPetRepository>();
                    var ownerRepo = scope.ServiceProvider.GetService<IOwnerRepository>();
                    var petTypeRepo = scope.ServiceProvider.GetService<IPetTypeRepository>();
                    var context = scope.ServiceProvider.GetService<Context>();


                    context.Database.EnsureCreated();
                }

            }
            //  }

            app.UseHttpsRedirection();

            app.UseRouting();

            //Enable CORS policy 
            app.UseCors("AllowEverything");

            // Use authentication
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
