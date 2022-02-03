using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SupplementStore.Tests {

    static class TestData {

        public static string Password = "UserSecret!123";

        public static string[] Roles = new string[] {
            "Owner",
            "Admin"
        };

        public static IdentityUser[] AllUsers = new IdentityUser[] {
            new IdentityUser { Email = "user0@test.pl", UserName = "user0@test.pl" },
            new IdentityUser { Email = "user1@test.pl", UserName = "user1@test.pl" },
            new IdentityUser { Email = "user2@test.pl", UserName = "user2@test.pl" }
        };

        public static IDictionary<string, IEnumerable<string>> UserRoles = new Dictionary<string, IEnumerable<string>> {
            { Owner.Email, new string[] { Roles[0] } },
            { Admin.Email, new string[] { Roles[1] } },
            { User.Email, new string[] { } }
        };

        public static IdentityUser Owner => AllUsers[0];

        public static IdentityUser Admin => AllUsers[1];

        public static IdentityUser User => AllUsers[2];
    }
}
