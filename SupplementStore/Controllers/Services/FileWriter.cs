using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SupplementStore.Controllers.Services {

    public class FileWriter : IFileWriter {

        public async Task ProcessAsync(IFormFile formFile, params string[] pathPieces) {

            var allPathPieces = new string[pathPieces.Length + 2];
            allPathPieces[0] = Directory.GetCurrentDirectory();
            allPathPieces[1] = "wwwroot";
            Array.Copy(pathPieces, 0, allPathPieces, 2, pathPieces.Length);

            var path = Path.Combine(allPathPieces);

            Directory.CreateDirectory(path);

            path = Path.Combine(path, formFile.FileName);

            using (Stream stream = new FileStream(path, FileMode.Create)) {

                await formFile.CopyToAsync(stream);
            }
        }

        public void Delete(string fileName, params string[] pathPieces) {

            var allPathPieces = new string[pathPieces.Length + 3];
            allPathPieces[0] = Directory.GetCurrentDirectory();
            allPathPieces[1] = "wwwroot";
            Array.Copy(pathPieces, 0, allPathPieces, 2, pathPieces.Length);
            allPathPieces[allPathPieces.Length - 1] = fileName;

            var path = Path.Combine(allPathPieces);

            if (File.Exists(path)) {

                File.Delete(path);
            }
        }
    }
}
