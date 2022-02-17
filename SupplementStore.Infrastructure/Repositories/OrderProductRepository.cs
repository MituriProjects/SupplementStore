using SupplementStore.Domain.Orders;
using System.Linq;

namespace SupplementStore.Infrastructure.Repositories {

    public class OrderProductRepository : Repository<OrderProduct>, IOrderProductRepository {

        public OrderProductRepository(IDocument<OrderProduct> document) : base(document) {
        }

        public OrderProduct FindBy(OrderProductId orderProductId) {

            return Document.All
                .FirstOrDefault(e => e.OrderProductId == orderProductId);
        }
    }
}
