namespace SupplementStore.Domain.Baskets {

    public interface IBasketProductRepository : IRepository<BasketProduct> {
        void Delete(BasketProductId basketProductId);
        BasketProduct FindBy(BasketProductId basketProductId);
    }
}
