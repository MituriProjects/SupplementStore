using Moq;
using SupplementStore.Controllers.Services;

namespace SupplementStore.Tests {

    static class Mocks {

        public static Mock<IFileWriter> FileWriterMock { get; private set; }

        public static void Reset() {

            FileWriterMock = new Mock<IFileWriter>();
        }
    }
}
