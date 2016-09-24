using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MusicHistoryCMS.Models;
using MusicHistoryCMS.Services;
using Sakura.AspNet.Mvc.PagedList;
using Sakura.AspNet.Mvc;

namespace MusicHistoryCMS
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.UseBootstrapPagerGenerator(pagerOptions =>
            {
                pagerOptions.Items = new PagerItemOptions
                {
                    TextFormat = "{0}", // The format for the pager button text, here means the content is just the actual page number. This property is used with string.Format method.
                    LinkParameterName = "page", // This property measn the generated pager button url will append the "page={pageNumber}" to the current URL.
                };
                pagerOptions.ExpandPageLinksForCurrentPage = 2; // Will display more 2 pager buttons before and after current page.
                pagerOptions.PageLinksForEndings = 2; // Will display 2 pager buttons for first and last pages.
                pagerOptions.Layout = PagedListPagerLayouts.Default; // Layout controls which elements will be displayed in the pager. For more information, please read the documentation.

                // Configure for "go to next" button
                pagerOptions.NextButton = new SpecialPagerItemOptions
                {
                    Text = "Next",
                    InactiveBehavior = SpecialPagerItemInactiveBehavior.Disable, // When there is no next page, disable this button
                    LinkParameterName = "page"
                };

                pagerOptions.PreviousButton = new SpecialPagerItemOptions
                {
                    Text = "Previous",
                    InactiveBehavior = SpecialPagerItemInactiveBehavior.Disable, // When there is no next page, disable this button
                    LinkParameterName = "page"
                };

                // Configure for "go to first page" button
                pagerOptions.FirstButton = new FirstAndLastPagerItemOptions
                {
                    Text = "First",
                    ActiveMode = FirstAndLastPagerItemActiveMode.Always,
                    InactiveBehavior = SpecialPagerItemInactiveBehavior.Disable,
                    LinkParameterName = "page",
                };

                // Configure for "go to last page" button
                pagerOptions.LastButton = new FirstAndLastPagerItemOptions
                {
                    Text = "Last",
                    ActiveMode = FirstAndLastPagerItemActiveMode.Always,
                    InactiveBehavior = SpecialPagerItemInactiveBehavior.Disable,
                    LinkParameterName = "page",
                };

                // Configure for omitted buttons (placeholder button when there's too many pages)
                pagerOptions.Omitted = new PagerItemOptions
                {
                    Text = "...",
                    Link = string.Empty // disable link
                };
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(o => {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonLetterOrDigit = false; ;
                o.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            // Add framework services.
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
                try
                {
                    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
                    {
                        serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                             .Database.Migrate();
                    }
                }
                catch { }
            }

            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            app.UseStaticFiles();

            app.UseIdentity();

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
