using SupplementStore.Application.Models;
using SupplementStore.Domain.Wishes;
using SupplementStore.Infrastructure.AppModels;
using System.Collections.Generic;

namespace SupplementStore.Infrastructure.AppServices.Wishes {

    public partial class WishService {

        public IEnumerable<ProductDetails> LoadMany(string userId) {

            var wishes = WishRepository.FindBy(new UserWishesFilter(userId));

            foreach (var wish in wishes) {

                var product = ProductRepository.FindBy(wish.ProductId);

                yield return ProductDetailsFactory.Create(product);
            }
        }
    }
}
