using SupplementStore.Domain;
using SupplementStore.Domain.Opinions;

namespace SupplementStore.Tests {

    static class RatingFactory {

        public static Rating Create(Entity entity) {

            return new Rating(RandomManager.Next(6));
        }
    }
}
