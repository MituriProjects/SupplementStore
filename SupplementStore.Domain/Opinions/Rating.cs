using System.Collections.Generic;

namespace SupplementStore.Domain.Opinions {

    public class Rating : ValueObject<Rating> {

        public int Stars { get; private set; }

        private Rating() {
        }

        public Rating(int stars) {

            Stars = stars;

            Validate();
        }

        protected override IEnumerable<object> GetValues() {

            yield return Stars;
        }

        private void Validate() {

            if (Stars < 1)
                throw new InvalidStateException("A Rating's Stars count must not be lesser than 1.");
            if (Stars > 5)
                throw new InvalidStateException("A Rating's Stars count must not be greater than 5.");
        }
    }
}
