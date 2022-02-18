using SupplementStore.Domain.Orders;
using System.Linq;

namespace SupplementStore.Infrastructure.Repositories {

    public class OrderRepository : Repository<Order>, IOrderRepository {

        public OrderRepository(IDocument<Order> document) : base(document) {
        }

        public Order FindBy(OrderId orderId) {

            return Document.All
                .FirstOrDefault(e => e.OrderId == orderId);
        }
    }
}
