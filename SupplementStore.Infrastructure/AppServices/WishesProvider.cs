using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Products;
using SupplementStore.Domain.Wishes;
using System.Collections.Generic;

namespace SupplementStore.Infrastructure.AppServices {

    public class WishesProvider : IWishesProvider {

        IProductRepository ProductRepository { get; }

        IWishRepository WishRepository { get; }

        public WishesProvider(
            IProductRepository productRepository,
            IWishRepository wishRepository) {

            ProductRepository = productRepository;
            WishRepository = wishRepository;
        }

        public IEnumerable<ProductDetails> Load(string userId) {

            var wishes = WishRepository.FindBy(new UserWishesFilter(userId));

            foreach (var wish in wishes) {

                var product = ProductRepository.FindBy(wish.ProductId);

                yield return new ProductDetails {
                    Id = product.ProductId.ToString(),
                    Name = product.Name,
                    Price = product.Price
                };
            }
        }
    }
}
