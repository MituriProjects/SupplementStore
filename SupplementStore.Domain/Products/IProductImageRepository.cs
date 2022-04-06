namespace SupplementStore.Domain.Products {

    public interface IProductImageRepository : IRepository<ProductImage> {
        ProductImage FindBy(ProductImageId productImageId);
    }
}
