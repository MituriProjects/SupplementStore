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
using SupplementStore.Infrastructure.AppServices.BasketProduct;
using SupplementStore.Infrastructure.AppServices.Opinions;
using SupplementStore.Infrastructure.AppServices.Order;
using SupplementStore.Infrastructure.AppServices.ProductImages;
using SupplementStore.Infrastructure.AppServices.Products;
using SupplementStore.Infrastructure.AppServices.Wishes;
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

            services.AddTransient<BasketProductManager>();
            services.AddTransient<OrderFactory>();

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductImageService, ProductImageService>();
            services.AddTransient<IBasketProductService, BasketProductService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IWishService, WishService>();
            services.AddTransient<IOpinionService, OpinionService>();
            services.AddTransient<IHiddenOpinionsProvider, HiddenOpinionsProvider>();
            services.AddTransient<IOpinionCreator, OpinionCreator>();
            services.AddTransient<IOpinionTextUpdater, OpinionTextUpdater>();
            services.AddTransient<IOpinionHider, OpinionHider>();
            services.AddTransient<IOpinionRevealer, OpinionRevealer>();
        }
    }
}
