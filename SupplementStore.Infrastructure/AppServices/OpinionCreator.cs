using SupplementStore.Application.Args;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;

namespace SupplementStore.Infrastructure.AppServices {

    public class OpinionCreator : IOpinionCreator {

        IPurchaseRepository PurchaseRepository { get; }

        IOpinionRepository OpinionRepository { get; }

        IDomainApprover DomainApprover { get; }

        public OpinionCreator(
            IPurchaseRepository purchaseRepository,
            IOpinionRepository opinionRepository,
            IDomainApprover domainApprover) {

            PurchaseRepository = purchaseRepository;
            OpinionRepository = opinionRepository;
            DomainApprover = domainApprover;
        }

        public void Create(OpinionCreatorArgs args) {

            var purchase = PurchaseRepository.FindBy(new PurchaseId(args.PurchaseId));

            if (purchase == null)
                return;

            if (purchase.OpinionId != null)
                return;

            var opinion = new Opinion {
                PurchaseId = new PurchaseId(args.PurchaseId),
                Text = args.Text,
                Rating = new Rating(args.Stars)
            };

            OpinionRepository.Add(opinion);

            purchase.OpinionId = opinion.OpinionId;

            DomainApprover.SaveChanges();
        }
    }
}
