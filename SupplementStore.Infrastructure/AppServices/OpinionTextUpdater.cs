using SupplementStore.Application.Services;
using SupplementStore.Domain.Opinions;

namespace SupplementStore.Infrastructure.AppServices {

    public class OpinionTextUpdater : IOpinionTextUpdater {

        IOpinionRepository OpinionRepository { get; }

        IDocumentApprover DocumentApprover { get; }

        public OpinionTextUpdater(
            IOpinionRepository opinionRepository,
            IDocumentApprover documentApprover) {

            OpinionRepository = opinionRepository;
            DocumentApprover = documentApprover;
        }

        public void Update(string opinionId, string text) {

            var opinion = OpinionRepository.FindBy(new OpinionId(opinionId));

            if (opinion == null)
                throw new MissingEntityException("There is no opinion entity with the provided id.");

            opinion.Text = text;

            DocumentApprover.SaveChanges();
        }
    }
}
