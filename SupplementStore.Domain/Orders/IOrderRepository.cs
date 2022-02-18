namespace SupplementStore.Domain.Orders {

    public interface IOrderRepository : IRepository<Order> {
        Order FindBy(OrderId orderId);
    }
}
