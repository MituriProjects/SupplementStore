using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SupplementStore.Controllers.Services {

    public interface IFileManager {
        Task SaveAsync(IFormFile formFile, params string[] pathPieces);
        void Delete(string fileName, params string[] pathPieces);
    }
}
