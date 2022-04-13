namespace SupplementStore.Infrastructure.Documentation {

    public class DocumentApprover : IDomainApprover {

        ApplicationDbContext DbContext { get; }

        public DocumentApprover(ApplicationDbContext dbContext) {

            DbContext = dbContext;
        }

        public void SaveChanges() {

            DbContext.SaveChanges();
        }
    }
}
