using System.Collections.Generic;

namespace SupplementStore.ViewModels.Role {

    public class UserRolesVM {

        public string UserId { get; set; }

        public string UserName { get; set; }

        public Dictionary<string, bool> UserRoles { get; set; }
    }
}
