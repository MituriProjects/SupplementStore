using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Tests {

    static class TestData {

        public static string Password = "UserSecret!123";

        public static string[] Roles = new string[] {
            "Owner",
            "Admin"
        };

        public static IdentityUser[] Owners = new IdentityUser[] {
            new IdentityUser { Email = "owner0@test.pl", UserName = "owner0@test.pl" }
        };

        public static IdentityUser[] Admins = new IdentityUser[] {
            new IdentityUser { Email = "admin0@test.pl", UserName = "admin0@test.pl" }
        };

        public static IdentityUser[] Users = new IdentityUser[] {
            new IdentityUser { Email = "user0@test.pl", UserName = "user0@test.pl" },
            new IdentityUser { Email = "user1@test.pl", UserName = "user1@test.pl" }
        };

        public static IdentityUser[] AllUsers = Owners.Concat(Admins).Concat(Users).ToArray();

        public static IdentityUser Owner => Owners[0];

        public static IdentityUser Admin => Admins[0];

        public static IdentityUser User => Users[0];

        public static IDictionary<string, IEnumerable<string>> UserRoles = Owners
            .ToDictionary(g => g.Email, g => new string[] { Roles[0], Roles[1] }.AsEnumerable())
            .Concat(Admins
            .ToDictionary(g => g.Email, g => new string[] { Roles[1] }.AsEnumerable()))
            .Concat(Users
            .ToDictionary(g => g.Email, g => new string[] { }.AsEnumerable()))
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }
}
