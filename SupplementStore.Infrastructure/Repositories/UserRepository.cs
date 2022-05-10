using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace SupplementStore.Infrastructure.Repositories {

    public class UserRepository {

        ApplicationDbContext DbContext { get; }

        public UserRepository(ApplicationDbContext dbContext) {

            DbContext = dbContext;
        }

        public IdentityUser FindBy(string id) {

            return DbContext.Users
                .FirstOrDefault(e => e.Id == id);
        }
    }
}
