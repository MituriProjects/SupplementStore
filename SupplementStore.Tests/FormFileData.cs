using System.Text;

namespace SupplementStore.Tests {

    public class FormFileData {

        public byte[] Bytes { get; }

        public string Name { get; }

        public string FileName { get; }

        public FormFileData(string fileContent, string name, string fileName) {

            Bytes = Encoding.UTF8.GetBytes(fileContent);
            Name = name;
            FileName = fileName;
        }
    }
}
