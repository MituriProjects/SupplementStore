using System;
using System.Collections.Generic;

namespace SupplementStore.Tests {

    static class RandomManager {

        static Random Random { get; } = new Random();

        static Queue<int> PreviouslyGiven { get; } = new Queue<int>();

        public static int Next(int max) {

            if (max <= 0)
                throw new ArgumentException("The RandomManager's Next method expects a max argument to be above zero.");

            while (max <= PreviouslyGiven.Count) {

                PreviouslyGiven.Dequeue();
            }

            int next = 0;
            do {

                next = Random.Next(1, max);

            } while (PreviouslyGiven.Contains(next));

            PreviouslyGiven.Enqueue(next);

            return next;
        }
    }
}
