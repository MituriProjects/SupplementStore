using SupplementStore.Application.Services;
using SupplementStore.Domain.Opinions;

namespace SupplementStore.Infrastructure.AppServices {

    public class OpinionHider : IOpinionHider {

        IOpinionRepository OpinionRepository { get; }

        IDomainApprover DomainApprover { get; }

        public OpinionHider(
            IOpinionRepository opinionRepository,
            IDomainApprover domainApprover) {

            OpinionRepository = opinionRepository;
            DomainApprover = domainApprover;
        }

        public void Hide(string opinionId) {

            var opinion = OpinionRepository.FindBy(new OpinionId(opinionId));

            if (opinion == null)
                throw new MissingEntityException("There is no opinion entity with the provided id.");

            opinion.IsHidden = true;

            DomainApprover.SaveChanges();
        }
    }
}
