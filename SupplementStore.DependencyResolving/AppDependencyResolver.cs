using Microsoft.Extensions.DependencyInjection;
using SupplementStore.Application.Services;
using SupplementStore.Domain;
using SupplementStore.Domain.Baskets;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using SupplementStore.Domain.Wishes;
using SupplementStore.Infrastructure;
using SupplementStore.Infrastructure.AppServices;
using SupplementStore.Infrastructure.Repositories;

namespace SupplementStore.DependencyResolving {

    public static class AppDependencyResolver {

        public static void Install(IServiceCollection services) {

            services.AddTransient(typeof(IDocument<>), typeof(Document<>));
            services.AddTransient<IDomainApprover, DocumentApprover>();

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IBasketProductRepository, BasketProductRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IPurchaseRepository, PurchaseRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductImageRepository, ProductImageRepository>();
            services.AddTransient<IWishRepository, WishRepository>();
            services.AddTransient<IOpinionRepository, OpinionRepository>();

            services.AddTransient<IProductProvider, ProductProvider>();
            services.AddTransient<IProductsProvider, ProductsProvider>();
            services.AddTransient<IProductCreator, ProductCreator>();
            services.AddTransient<IProductUpdater, ProductUpdater>();
            services.AddTransient<IBasketProductProvider, BasketProductProvider>();
            services.AddTransient<IBasketProductsProvider, BasketProductsProvider>();
            services.AddTransient<IBasketProductCreator, BasketProductCreator>();
            services.AddTransient<IBasketProductUpdater, BasketProductUpdater>();
            services.AddTransient<IBasketProductRemover, BasketProductRemover>();
            services.AddTransient<IOrderCreator, OrderCreator>();
            services.AddTransient<IOrderProvider, OrderProvider>();
            services.AddTransient<IOrdersProvider, OrdersProvider>();
            services.AddTransient<IWishProvider, WishProvider>();
            services.AddTransient<IWishesProvider, WishesProvider>();
            services.AddTransient<IWishCreator, WishCreator>();
            services.AddTransient<IWishRemover, WishRemover>();
            services.AddTransient<IProductToOpineProvider, ProductToOpineProvider>();
            services.AddTransient<IOpinionProductProvider, OpinionProductProvider>();
            services.AddTransient<IProductOpinionsProvider, ProductOpinionsProvider>();
            services.AddTransient<IOpinionProvider, OpinionProvider>();
            services.AddTransient<IUserOpinionsProvider, UserOpinionsProvider>();
            services.AddTransient<IHiddenOpinionsProvider, HiddenOpinionsProvider>();
            services.AddTransient<IOpinionCreator, OpinionCreator>();
            services.AddTransient<IOpinionTextUpdater, OpinionTextUpdater>();
            services.AddTransient<IOpinionHider, OpinionHider>();
            services.AddTransient<IOpinionRevealer, OpinionRevealer>();
        }
    }
}
