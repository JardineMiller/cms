using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cms.ApplicationLayer;
using cms.ApplicationLayer.Commands;
using cms.ApplicationLayer.Queries;
using cms.Config;
using cms.Data_Layer;
using cms.Data_Layer.Models;
using cms.Data_Layer.Seeding;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;

namespace cms
{
    public class Startup
    {
        public static IConfiguration Config { get; private set; }

        public Startup(IConfiguration config)
        {
            Config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Configs
            services.Configure<DatabaseConfig>(Config.GetSection("database"));

            // Database
            var serviceProvider = services.BuildServiceProvider();
            var connection = serviceProvider.GetService<IOptions<DatabaseConfig>>().Value.Connection;
            services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(connection));
            services.AddScoped<ISeeder, DatabaseSeeder>();

            // Services
            services.AddMvc();

            // Queries
            services.AddScoped<IQueryHandler<GetUsersQuery, IEnumerable<User>>, GetUsersQueryHandler>();

            // Commands
            services.AddScoped<ICommandHandler<DeleteUsersCommand>, DeleteUsersCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateUsersCommand>, UpdateUsersCommandHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ISeeder seeder)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            seeder.SeedAll();
            app.UseStatusCodePages();
            app.UseMvc();
        }
    }
}