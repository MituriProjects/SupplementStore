namespace SupplementStore.Domain.Products {

    public interface IProductImageRepository : IRepository<ProductImage> {
        void Delete(ProductImageId productImageId);
        ProductImage FindBy(ProductImageId productImageId);
    }
}
