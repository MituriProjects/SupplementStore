using System;

namespace SupplementStore.Tests.Integration {

    public interface IResponseDetails {
        void Examine(ContentScheme contentScheme);
        void Examine(string exactContent);
        void ExamineRedirect(string uri);
        void ExamineAuthRedirect(string returnUri);
        void ExamineAccessDeniedRedirect(string returnUri);
        void ExamineExceptionThrown<TException>()
            where TException : Exception;
        void ExamineNoExceptionThrown();
    }
}
