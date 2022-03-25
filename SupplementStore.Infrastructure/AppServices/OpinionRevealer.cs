using SupplementStore.Application.Services;
using SupplementStore.Domain.Opinions;

namespace SupplementStore.Infrastructure.AppServices {

    public class OpinionRevealer : IOpinionRevealer {

        IOpinionRepository OpinionRepository { get; }

        IDomainApprover DomainApprover { get; }

        public OpinionRevealer(
            IOpinionRepository opinionRepository,
            IDomainApprover domainApprover) {

            OpinionRepository = opinionRepository;
            DomainApprover = domainApprover;
        }

        public void Reveal(string opinionId) {

            var opinion = OpinionRepository.FindBy(new OpinionId(opinionId));

            if (opinion == null)
                throw new MissingEntityException("There is no opinion entity with the provided id.");

            opinion.IsHidden = false;

            DomainApprover.SaveChanges();
        }
    }
}
