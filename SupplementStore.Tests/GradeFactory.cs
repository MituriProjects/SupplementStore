using SupplementStore.Domain;
using SupplementStore.Domain.Opinions;

namespace SupplementStore.Tests {

    static class GradeFactory {

        public static Grade Create(Entity entity) {

            return new Grade(RandomManager.Next(6));
        }
    }
}
