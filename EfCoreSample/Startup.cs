using EfCoreSample.Persistance;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration.UserSecrets;
using EfCoreSample.Infrastructure;
using AutoMapper;
using Microsoft.OpenApi.Models;
using System.IO;
using EfCoreSample.Infrastructure.Abstraction;
using EfCoreSample.Infrastructure.Repository;
using EfCoreSample.Doman.Entities;
using EfCoreSample.Infrastructure.Services;
using EfCoreSample.Doman;
using EfCoreSample.Doman.DTO;
using EfCoreSample.Infrastructure.Extensions;
using System;
using System.Reflection;
using FluentValidation.AspNetCore;
using EfCoreSample.Doman.Validators;

namespace EfCoreSample
{
    public class Startup
    {
        public Startup()
        {
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true);
            
                builder.AddUserSecrets<EfCoreSampleDbContext>();
            Configuration = builder.Build();

        }

        public IConfiguration Configuration { get; set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => 
                    fv.RegisterValidatorsFromAssemblyContaining<ProjectValidatorPut>()
                    .RunDefaultMvcValidationAfterFluentValidationExecutes = false);


            services.AddDbContext<EfCoreSampleDbContext>((serviceprovider, builder) =>
                builder.UseMySql(Configuration["ConnectionStrings:LocalConnection"]));
            services.AddScoped<IRepository<Project, long>, ProjectRepository>();
            services.AddScoped<IService<Project, long>, ProjectService>();
           
            services.AddAutoMapper(typeof(Startup), typeof(Project), typeof(Employee), typeof(EmployeeProject));
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Project API", Version = "v1" });
                c.IncludeXmlComments(xmlPath);
            });
            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, EfCoreSampleDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
            app.EnsureContextMigrated<EfCoreSampleDbContext>();
            //TODO change this seed method, remove EfCoreSampleDbContext from configure method
            SeedDb.Initialize(context);//ContextSeed.SeedAsync(app).Wait();
        }
    }
}
