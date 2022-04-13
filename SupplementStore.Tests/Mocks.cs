using Moq;
using SupplementStore.Controllers.Services;

namespace SupplementStore.Tests {

    static class Mocks {

        public static Mock<IFileManager> FileManagerMock { get; private set; }

        public static void Reset() {

            FileManagerMock = new Mock<IFileManager>();
        }
    }
}
