using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupplementStore.Controllers.Services;
using SupplementStore.DependencyResolving;
using SupplementStore.Infrastructure;
using System;

namespace SupplementStore {

    public class Startup {

        IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) {

            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {

            ConfigureDatabase(services);

            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            AppDependencyResolver.Install(services);

            services.AddTransient(typeof(Lazy<>), typeof(LazyWrapper<>));

            services.AddTransient<RoleDirector>();
            services.AddTransient<OwnerManager>();
            services.AddTransient<AdminManager>();
            services.AddTransient<IFileManager, FileManager>();

            services.AddSingleton<Translator>();
            services.AddSingleton(typeof(Translator<>));

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc(options => {

                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

                options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((s1, s2) => $"Wartość '{s1}' jest nieprawidłowa dla pola '{s2}'");

            }).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization();

            ReconfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {

            var supportedCultures = new[] { "pl", "en" };
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            if (env.IsDevelopment()) {

                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes => {

                routes.MapRoute(
                    name: "createOwner",
                    template: "CreateOwner/{email}",
                    defaults: new { controller = "Owner", action = "Assign" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });
            });

            Reconfigure(app, env);
        }

        protected virtual void ConfigureDatabase(IServiceCollection services) {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
        }

        protected virtual void ReconfigureServices(IServiceCollection services) {
        }

        protected virtual void Reconfigure(IApplicationBuilder app, IHostingEnvironment env) {
        }
    }
}
