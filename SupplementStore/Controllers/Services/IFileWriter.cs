using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SupplementStore.Controllers.Services {

    public interface IFileWriter {
        Task ProcessAsync(IFormFile formFile, params string[] pathPieces);
        void Delete(string fileName, params string[] pathPieces);
    }
}
