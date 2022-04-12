using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Tests {

    class TestDocumentApprover : IDomainApprover {

        static IEnumerable<object> OnSaveChangesValues { get; set; }

        public void SaveChanges() {

            OnSaveChangesValues = TestDocumentSet.GetValues();
        }

        public static void ExamineSaveChanges() {

            Assert.IsNotNull(OnSaveChangesValues, "No call to the IDomainApprover's SaveChanges method was detected.");

            var onExaminationValues = TestDocumentSet.GetValues();

            Assert.IsTrue(Enumerable.SequenceEqual(OnSaveChangesValues, onExaminationValues),
                "TestDocumentApprover has detected changes made after the call to the IDomainApprover's SaveChanges method.");
        }

        public static void ExamineNoChangesSaved() {

            Assert.IsNull(OnSaveChangesValues, "The call to the IDomainApprover's SaveChanges method was detected.");
        }

        public static void ClearDocuments() {

            TestDocumentSet.ClearDocuments();

            OnSaveChangesValues = null;
        }
    }
}
