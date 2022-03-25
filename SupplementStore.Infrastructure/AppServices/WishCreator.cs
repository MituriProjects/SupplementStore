﻿using SupplementStore.Application.Services;
using SupplementStore.Domain.Products;
using SupplementStore.Domain.Wishes;

namespace SupplementStore.Infrastructure.AppServices {

    public class WishCreator : IWishCreator {

        IProductRepository ProductRepository { get; }

        IWishRepository WishRepository { get; }

        IDomainApprover DomainApprover { get; }

        public WishCreator(
            IProductRepository productRepository,
            IWishRepository wishRepository,
            IDomainApprover domainApprover) {

            ProductRepository = productRepository;
            WishRepository = wishRepository;
            DomainApprover = domainApprover;
        }

        public void Create(string userId, string productId) {

            var product = ProductRepository.FindBy(new ProductId(productId));

            if (product == null)
                return;

            var wish = WishRepository.FindBy(new UserWishFilter(userId, new ProductId(productId)));

            if (wish != null)
                return;

            WishRepository.Add(new Wish {
                UserId = userId,
                ProductId = new ProductId(productId)
            });

            DomainApprover.SaveChanges();
        }
    }
}
