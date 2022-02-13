using Microsoft.Extensions.DependencyInjection;
using SupplementStore.Application.Services;
using SupplementStore.Infrastructure;
using SupplementStore.Infrastructure.AppServices;

namespace SupplementStore.DependencyResolving {

    public static class DependencyResolver {

        public static void Install(IServiceCollection services) {

            services.AddTransient(typeof(IDocument<>), typeof(Document<>));
            services.AddTransient<IDocumentApprover, DocumentApprover>();

            services.AddTransient<IProductProvider, ProductProvider>();
            services.AddTransient<IProductsProvider, ProductsProvider>();
            services.AddTransient<IBasketProductProvider, BasketProductProvider>();
            services.AddTransient<IBasketProductsProvider, BasketProductsProvider>();
            services.AddTransient<IBasketProductCreator, BasketProductCreator>();
            services.AddTransient<IBasketProductUpdater, BasketProductUpdater>();
            services.AddTransient<IBasketProductRemover, BasketProductRemover>();
            services.AddTransient<IOrderCreator, OrderCreator>();
            services.AddTransient<IOrderProvider, OrderProvider>();
        }
    }
}
