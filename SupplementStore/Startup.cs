using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupplementStore.Application.Services;
using SupplementStore.Infrastructure;
using SupplementStore.Infrastructure.AppServices;

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

            services.AddTransient(typeof(IDocument<>), typeof(Document<>));
            services.AddTransient<IDocumentApprover, DocumentApprover>();

            services.AddTransient<IProductProvider, ProductProvider>();
            services.AddTransient<IProductsProvider, ProductsProvider>();
            services.AddTransient<IBasketProductProvider, BasketProductProvider>();
            services.AddTransient<IBasketProductsProvider, BasketProductsProvider>();
            services.AddTransient<IBasketProductCreator, BasketProductCreator>();
            services.AddTransient<IBasketProductUpdater, BasketProductUpdater>();
            services.AddTransient<IBasketProductRemover, BasketProductRemover>();
            services.AddTransient<IOrderCreator, OrderCreator>();

            services.AddMvc(options => {

                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            ReconfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {

            if (env.IsDevelopment()) {

                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes => {

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
