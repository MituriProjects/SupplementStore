using System;
using System.Collections.Generic;

namespace SupplementStore.Tests {

    static class RandomManager {

        static Random Random { get; } = new Random();

        static Queue<int> PreviouslyGiven { get; } = new Queue<int>();

        public static int Next(int max) {

            while (max <= PreviouslyGiven.Count) {

                PreviouslyGiven.Dequeue();
            }

            int next = 0;
            do {

                next = Random.Next(max);

            } while (PreviouslyGiven.Contains(next));

            PreviouslyGiven.Enqueue(next);

            return next;
        }
    }
}
