using SupplementStore.Application.Args;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;

namespace SupplementStore.Infrastructure.AppServices {

    public class OpinionCreator : IOpinionCreator {

        IOrderProductRepository OrderProductRepository { get; }

        IOpinionRepository OpinionRepository { get; }

        IDocumentApprover DocumentApprover { get; }

        public OpinionCreator(
            IOrderProductRepository orderProductRepository,
            IOpinionRepository opinionRepository,
            IDocumentApprover documentApprover) {

            OrderProductRepository = orderProductRepository;
            OpinionRepository = opinionRepository;
            DocumentApprover = documentApprover;
        }

        public void Create(OpinionCreatorArgs args) {

            var orderProduct = OrderProductRepository.FindBy(new OrderProductId(args.OrderProductId));

            if (orderProduct == null)
                return;

            if (orderProduct.OpinionId != null)
                return;

            var opinion = new Opinion {
                OrderProductId = new OrderProductId(args.OrderProductId),
                Text = args.Text,
                Grade = new Grade(args.Stars)
            };

            OpinionRepository.Add(opinion);

            orderProduct.OpinionId = opinion.OpinionId;

            DocumentApprover.SaveChanges();
        }
    }
}
