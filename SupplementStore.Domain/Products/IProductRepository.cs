namespace SupplementStore.Domain.Products {

    public interface IProductRepository : IRepository<Product> {
        Product FindBy(ProductId productId);
    }
}
