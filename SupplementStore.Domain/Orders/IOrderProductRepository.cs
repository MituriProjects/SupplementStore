namespace SupplementStore.Domain.Orders {

    public interface IOrderProductRepository : IRepository<OrderProduct> {
        OrderProduct FindBy(OrderProductId orderProductId);
    }
}
