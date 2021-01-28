using System;
using System.Collections.Generic;
using System.Linq;
using clean.api.Configurations;
using clean.api.HealthChecks;
using clean.infra.crosscutting.commons.Extensions;
using clean.infra.crosscutting.IoC;
using clean.infra.data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace clean.api
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
            services.AddControllers();
                    //.AddNewtonsoft(op => op.SerializerSettings.ReferenceLoopHandling = Newtosoft.Json.ReferenceLoopHandling.Ignore);

            services.AddAutoMapperConfiguration();
            services.RegisterServices();
            services.AddDbContext<CleanContext>();

            services.AddHealthChecks()
                .AddCheck(nameof(DbHealthCheck), new DbHealthCheck("connectionString"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseHealthChecks("/health-check", GetHealthOptions());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private HealthCheckOptions GetHealthOptions()
        {
            return new HealthCheckOptions
            {
                AllowCachingResponses = false,
                ResponseWriter = async (c, r) =>
                {
                    c.Response.ContentType = "application/json";
                    var results = r.Entries.Select(p =>
                    {
                        return KeyValuePair.Create(p.Key, new
                        {
                            Status = p.Value.Status.ToString(),
                            Description = p.Value.Description,
                            Duration = p.Value.Duration,
                            Data = p.Value.Data,
                            Tags = p.Value.Tags.ToJson()
                        });
                    }).ToDictionary(a => a.Key, a => a.Value);

                    var result = new
                    {
                        Status = r.Status.ToString(),
                        TotalDuration = r.TotalDuration.TotalSeconds.ToString(),
                        Results = results
                    };

                    await c.Response.WriteAsync(result.ToJson());
                }
            };
        }
    }
}
