using System;

namespace SupplementStore.Tests.Integration {

    class NullResponseDetails : IResponseDetails {

        public void Examine(ContentScheme contentScheme) {

            Throw();
        }

        public void Examine(string exactContent) {

            Throw();
        }

        public void ExamineAccessDeniedRedirect(string returnUri) {

            Throw();
        }

        public void ExamineAuthRedirect(string returnUri) {

            Throw();
        }

        public void ExamineExceptionThrown<TException>() where TException : Exception {

            Throw();
        }

        public void ExamineNoExceptionThrown() {

            Throw();
        }

        public void ExamineRedirect(string uri) {

            Throw();
        }

        void Throw() {

            throw new InvalidOperationException("No response details was detected.");
        }
    }
}
