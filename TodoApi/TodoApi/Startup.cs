using System;
using System.Text.Json;

using Autofac;
using Autofac.Configuration;

using CSharpFunctionalExtensions;

using FluentValidation;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Todo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) =>
            services.AddControllers(options => options.Filters.Add(new ProducesAttribute("application/json")))
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var autofacModulesConfigurationBuilder = new ConfigurationBuilder();
            autofacModulesConfigurationBuilder.AddJsonFile("autofacModules.json");

            builder.RegisterModule(new ConfigurationModule(autofacModulesConfigurationBuilder.Build()));
        }

        public void ConfigureTestContainer(ContainerBuilder builder)
        {
            var autofacModulesConfigurationBuilder = new ConfigurationBuilder();
            autofacModulesConfigurationBuilder.AddJsonFile("testAutofacModules.json");

            builder.RegisterModule(new ConfigurationModule(autofacModulesConfigurationBuilder.Build()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _ = env ?? throw new ArgumentNullException(nameof(env));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var error = context.Features.Get<IExceptionHandlerPathFeature>().Error;

                if (error is ValidationException)
                {
                    var result = JsonSerializer.Serialize(Result.Failure(error.Message));
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(result);
                }
                else
                {
                    throw error;
                }
            }));

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
