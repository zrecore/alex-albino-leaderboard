using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Microsoft.EntityFrameworkCore;
using LeaderboardAPI.Models;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;

using Microsoft.OData;


namespace LeaderboardAPI
{
    public class Startup
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            using (var client = new LeaderboardAPIContext())
            {
                client.Database.EnsureCreated();
                client.Database.Migrate();
            }

            this.hostingEnvironment = env;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            // Entity Framework 7.x
            services.AddEntityFrameworkSqlite()
                    .AddDbContext<LeaderboardAPIContext>();

            // Add CORS (Cross Origin Requests) headers
            // See https://manuel-rauber.com/2016/03/29/node-js-asp-net-core-1-0-a-usage-comparison-part-4-cross-origin-resource-sharing/
            // NOTE: For production, we must limit who can actually call these APIs!
            Console.WriteLine("Startup.cs::ConfigureServices() ...Adding CORS AllowFromAll policy.");
            services.AddCors(options => 
            {
                options.AddPolicy("AllowFromAll",
                builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowCredentials());
            });

            // OData
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors("AllowFromAll");
            // app.UseOData("odata");
            app.UseMvc();
            if (env.IsDevelopment())
            {
                // Ensure default data is available during development.
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    Console.WriteLine("Startup.cs::Configure() ...Seeding DB.");
                    serviceScope.ServiceProvider.GetService<LeaderboardAPIContext>().EnsureSeedData();
                }
            }
        }
    }
}
