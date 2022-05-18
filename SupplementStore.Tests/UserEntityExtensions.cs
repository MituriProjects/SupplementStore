using Microsoft.AspNetCore.Identity;
using SupplementStore.Domain;

namespace SupplementStore.Tests {

    static class UserEntityExtensions {

        public static TUserEntity WithUserId<TUserEntity>(this TUserEntity userEntity, IdentityUser user)
            where TUserEntity : UserEntity {

            userEntity.UserId = user.Id;

            return userEntity;
        }
    }
}
