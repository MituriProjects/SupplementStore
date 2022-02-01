using System;

namespace SupplementStore.Tests {

    static class RandomManager {

        static Random Random { get; } = new Random();

        public static int Next(int max) {

            return Random.Next(max);
        }
    }
}
