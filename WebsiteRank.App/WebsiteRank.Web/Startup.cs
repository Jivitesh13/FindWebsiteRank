using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebsiteRank.Common.Options;
using WebsiteRank.Data;
using WebsiteRank.Data.DBContexts;
using WebsiteRank.Data.Repositories;
using WebsiteRank.SearchProvider.Implementation;
using WebsiteRank.SearchProvider.Interface;
using WebsiteRank.SearchService.Interface;
using WebSearchService = WebsiteRank.SearchService.Implementation.SearchService;

namespace WebsiteRank.Web
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
            
            services.Configure<GoogleOptions>(Configuration.GetSection($"{SearchProvidersSettingsOptions.SECTION_NAME}:{GoogleOptions.SECTION_NAME}"));
            services.Configure<BingOptions>(Configuration.GetSection($"{SearchProvidersSettingsOptions.SECTION_NAME}:{BingOptions.SECTION_NAME}"));

            // Add Db context with SQL server DB
            services.AddDbContext<WebsiteRankDBContext>(o => o.UseSqlServer(Configuration.GetConnectionString("WebsiteRankDBConnection")));
            
            services.AddAutoMapper(typeof(Startup));

            // Service registration
            services.AddScoped<IProviderService, GoogleService>();
            services.AddScoped<IProviderService, BingService>();
            services.AddScoped<ISearchService, WebSearchService>();
            services.AddScoped<ISearchHistoryRepository, SearchHistoryRepository>();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebsiteRankDBContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            db.Database.EnsureCreated();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
