﻿using System.Collections.Generic;
using cms.ApplicationLayer;
using cms.ApplicationLayer.Commands;
using cms.ApplicationLayer.Commands.Handlers;
using cms.ApplicationLayer.Commands.Processor;
using cms.ApplicationLayer.Commands.Resolver;
using cms.ApplicationLayer.Queries;
using cms.ApplicationLayer.Queries.Handlers;
using cms.ApplicationLayer.Queries.Processor;
using cms.ApplicationLayer.Queries.Resolver;
using cms.Config;
using cms.Data_Layer.Contexts;
using cms.Data_Layer.Models;
using cms.Data_Layer.Seeding;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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
            services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(connection));
            services.AddScoped<ISeeder, DatabaseSeeder>();

            // Services
            services.AddMvc();

            // Queries
            services.AddScoped<IQueryProcessor, QueryProcessor>();
            services.AddScoped<IQueryResolver, QueryResolver>();
            services.AddScoped<IQueryHandler<GetUsersQuery, List<User>>, GetUsersQueryHandler>();
            services.AddScoped<IQueryHandler<GetUserPostsQuery, List<Post>>, GetUserPostsQueryHandler>();
            services.AddScoped<IQueryHandler<GetPostsQuery, List<Post>>, GetPostsQueryHandler>();

            // Commands
            services.AddScoped<ICommandProcessor, CommandProcessor>();
            services.AddScoped<ICommandResolver, CommandResolver>();
            services.AddScoped<ICommandHandler<DeleteUsersCommand, CommandResult<List<long>>>, DeleteUsersCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateUsersCommand, CommandResult<List<User>>>, UpdateUsersCommandHandler>();
            services.AddScoped<ICommandHandler<CreateUsersCommand, CommandResult<List<User>>>, CreateUserCommandHandler>();
            
            services.AddScoped<ICommandHandler<CreateUserPostCommand, CommandResult<Post>>, CreateUserPostCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateUserPostCommand, CommandResult<Post>>, UpdateUserPostCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteUserPostCommand, CommandResult<long?>>, DeleteUserPostCommandHandler>();
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
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}