using SupplementStore.Application.Args;
using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Entities.Baskets;
using SupplementStore.Domain.Entities.Orders;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices {

    public class OrderCreator : IOrderCreator {

        IDocument<Order> OrderDocument { get; }

        IDocument<BasketProduct> BasketProductDocument { get; }

        IDocument<OrderProduct> OrderProductDocument { get; }

        IDocumentApprover DocumentApprover { get; }

        public OrderCreator(
            IDocument<Order> orderDocument,
            IDocument<BasketProduct> basketProductDocument,
            IDocument<OrderProduct> orderProductDocument,
            IDocumentApprover documentApprover) {

            OrderDocument = orderDocument;
            BasketProductDocument = basketProductDocument;
            OrderProductDocument = orderProductDocument;
            DocumentApprover = documentApprover;
        }

        public OrderDetails Create(OrderCreatorArgs args) {

            var order = new Order {
                UserId = args.UserId,
                Address = args.Address,
                PostalCode = args.PostalCode,
                City = args.City
            };

            OrderDocument.Add(order);

            foreach (var basketProduct in BasketProductDocument.All.Where(e => e.UserId == args.UserId).ToList()) {

                var orderProduct = new OrderProduct {
                    OrderId = order.Id,
                    ProductId = basketProduct.ProductId,
                    Quantity = basketProduct.Quantity
                };

                OrderProductDocument.Add(orderProduct);

                BasketProductDocument.Delete(basketProduct.Id);
            }

            if (order.GetBrokenRules().Count() == 0) {

                DocumentApprover.SaveChanges();

                return new OrderDetails {
                    Id = order.Id.ToString()
                };
            }

            return null;
        }
    }
}
