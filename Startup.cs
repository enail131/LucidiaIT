﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LucidiaIT.Data;
using LucidiaIT.Models;
using LucidiaIT.Services;
using LucidiaIT.Interfaces;
using LucidiaIT.Models.EmployeeModels;
using LucidiaIT.Models.PartnerModels;

namespace LucidiaIT
{
    public class Startup
    {
        public IConfiguration _config { get; }

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection")));

            services.AddDbContext<EmployeeContext>(options =>
                options.UseSqlServer(_config.GetConnectionString("EmployeeContext")));

            services.AddDbContext<PartnerContext>(options =>
                options.UseSqlServer(_config.GetConnectionString("PartnerContext")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IStorageService, StorageService>();
            services.AddTransient<IMessageBuilder, MessageBuilder>();
            services.AddTransient<ISendGridBuilder, SendGridBuilder>();

            services.AddScoped<IDataService<Employee>, EmployeeService>();
            services.AddScoped<IDataService<Partner>, PartnerService>();

            services.AddSingleton(_config);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
