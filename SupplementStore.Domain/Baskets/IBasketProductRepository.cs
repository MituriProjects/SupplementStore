namespace SupplementStore.Domain.Baskets {

    public interface IBasketProductRepository : IRepository<BasketProduct> {
        BasketProduct FindBy(BasketProductId basketProductId);
    }
}
