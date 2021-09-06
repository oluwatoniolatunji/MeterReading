using EnergyMeterReading.Api.Middlewares;
using EnergyMeterReading.DataAccess;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace EnergyMeterReading.Api
{
    public class Startup
    {
        private readonly string AppOrigin = "AppOrigin";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDatabaseServices(services);

            ConfigureDefaultServices(services);
        }

        public virtual void ConfigureDatabaseServices(IServiceCollection services)
        {
            services.AddDbContext<MeterReadingDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("MeterReadingDbConnection"),
                   x => x.MigrationsAssembly("EnergyMeterReading.DataAccess"))
            );
        }

        public void ConfigureDefaultServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: AppOrigin, builder =>
                {
                    builder.WithOrigins(Configuration["Cors:AllowedOrigin"]);
                });
            });

            services.AddRepositoryDependencies();

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(GlobalExceptionFilter));
            }).AddNewtonsoftJson();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                    .AddFluentValidation();

            services.Configure((Action<ApiBehaviorOptions>)(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    return context.GetModelStateError();
                };
            }));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Meter Reading APIs",
                    Version = "v1",
                    Description = "APIs for managing meter readings."
                });
            });
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./v1/swagger.json", "Meter Reading APIs");
            });

            app.UseCors(AppOrigin);

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
