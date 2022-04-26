using SupplementStore.Application.Args;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;

namespace SupplementStore.Infrastructure.AppServices.Opinions {

    public partial class OpinionService {

        public void Create(OpinionCreateArgs args) {

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
