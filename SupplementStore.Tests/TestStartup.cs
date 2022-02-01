using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupplementStore.Infrastructure;

namespace SupplementStore.Tests {

    public class TestStartup : Startup {

        public TestStartup(IConfiguration configuration)
            : base(configuration) {
        }

        protected override void ConfigureDatabase(IServiceCollection services) {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("supplementstore_test_db"));
        }

        protected override void ReconfigureServices(IServiceCollection services) {

            services.AddTransient(typeof(IDocument<>), typeof(TestDocument<>));
            services.AddTransient(typeof(IDocumentApprover), typeof(TestDocumentApprover));
        }

        protected override void Reconfigure(IApplicationBuilder app, IHostingEnvironment env) {

            CreateUsers(
                app.ApplicationServices.GetService<UserManager<IdentityUser>>(),
                app.ApplicationServices.GetService<RoleManager<IdentityRole>>());
        }

        private void CreateUsers(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) {

            foreach (string role in TestData.Roles) {

                roleManager.CreateAsync(new IdentityRole(role));
            }

            foreach (IdentityUser user in TestData.AllUsers) {

                userManager.CreateAsync(user, TestData.Password);

                foreach (string role in TestData.UserRoles[user.Email]) {

                    userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
