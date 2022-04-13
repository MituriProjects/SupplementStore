using SupplementStore.Domain.Opinions;

namespace SupplementStore.Infrastructure.AppServices.Opinions {

    public partial class OpinionService {

        public void UpdateText(string opinionId, string text) {

            var opinion = OpinionRepository.FindBy(new OpinionId(opinionId));

            if (opinion == null)
                throw new MissingEntityException("There is no opinion entity with the provided id.");

            opinion.Text = text;

            DomainApprover.SaveChanges();
        }
    }
}
