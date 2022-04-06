using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Products;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices {

    public class ProductImagesProvider : IProductImagesProvider {

        public IProductImageRepository ProductImageRepository { get; set; }

        public ProductImagesProvider(IProductImageRepository productImageRepository) {

            ProductImageRepository = productImageRepository;
        }

        public IEnumerable<ProductImageDetails> Load(string productId) {

            return ProductImageRepository
                .FindBy(new ProductImagesFilter(new ProductId(productId)))
                .Select(e => new ProductImageDetails {
                    Name = e.Name
                })
                .ToList();
        }
    }
}
