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
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
//using TodoApi.Models;
using CarsAPI.Models;
using System.Reflection;
using System.IO;

namespace CarsAPI
{
    public class Startup
    { 
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           // services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));

            services.AddDbContext<CarsContext>(opt => opt.UseSqlite(Configuration.GetConnectionString("CarsDB")));
            services.AddControllers();
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();
            services.AddCors(options =>
            {
             options.AddPolicy(MyAllowSpecificOrigins,
             builder =>
                 {
            builder.WithOrigins("https://",
                                "https://brendansd3.github.io/")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
              });
             });

             services.AddSwaggerGen(c =>
            {
              c.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2",
        Title = "Cars API",
        Description = "An API for the DemoCar Store",
        Contact = new OpenApiContact
        {
            Name = "Brendan Wall",
            Email = string.Empty,
            Url = new Uri("https://github.com/BrendanSD3"),
        },
        License = new OpenApiLicense
        {
            Name = "Use under LICX",
            Url = new Uri("https://example.com/license"),
        }
    });
     // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CarsContext context)
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();

            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "Cars API v2");
                });
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseDefaultFiles();
            app.UseStaticFiles();


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
