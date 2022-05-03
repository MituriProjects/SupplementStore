using Microsoft.Extensions.DependencyInjection;
using SupplementStore.Application.Services;
using SupplementStore.Domain;
using SupplementStore.Domain.Addresses;
using SupplementStore.Domain.Baskets;
using SupplementStore.Domain.Messages;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using SupplementStore.Domain.Wishes;
using SupplementStore.Infrastructure;
using SupplementStore.Infrastructure.AppServices.Addresses;
using SupplementStore.Infrastructure.AppServices.BasketProduct;
using SupplementStore.Infrastructure.AppServices.Messages;
using SupplementStore.Infrastructure.AppServices.Opinions;
using SupplementStore.Infrastructure.AppServices.Order;
using SupplementStore.Infrastructure.AppServices.ProductImages;
using SupplementStore.Infrastructure.AppServices.Products;
using SupplementStore.Infrastructure.AppServices.Wishes;
using SupplementStore.Infrastructure.Documentation;
using SupplementStore.Infrastructure.Repositories;

namespace SupplementStore.DependencyResolving {

    public static class AppDependencyResolver {

        public static void Install(IServiceCollection services) {

            services.AddTransient(typeof(IDocument<>), typeof(Document<>));
            services.AddTransient<IDomainApprover, DocumentApprover>();

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IBasketProductRepository, BasketProductRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IPurchaseRepository, PurchaseRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductImageRepository, ProductImageRepository>();
            services.AddTransient<IWishRepository, WishRepository>();
            services.AddTransient<IOpinionRepository, OpinionRepository>();

            services.AddTransient<AddressFactory>();
            services.AddTransient<BasketProductManager>();
            services.AddTransient<MessageFactory>();
            services.AddTransient<OrderFactory>();

            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductImageService, ProductImageService>();
            services.AddTransient<IBasketProductService, BasketProductService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IWishService, WishService>();
            services.AddTransient<IOpinionService, OpinionService>();
        }
    }
}
