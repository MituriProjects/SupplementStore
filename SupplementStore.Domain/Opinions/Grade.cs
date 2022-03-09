using System.Collections.Generic;

namespace SupplementStore.Domain.Opinions {

    public class Grade : ValueObject<Grade> {

        public int Stars { get; private set; }

        private Grade() {
        }

        public Grade(int stars) {

            Stars = stars;

            Validate();
        }

        protected override IEnumerable<object> GetValues() {

            yield return Stars;
        }

        private void Validate() {


        }
    }
}
