namespace SupplementStore.Infrastructure {

    public class DocumentApprover : IDocumentApprover {

        ApplicationDbContext DbContext { get; }

        public DocumentApprover(ApplicationDbContext dbContext) {

            DbContext = dbContext;
        }

        public void SaveChanges() {

            DbContext.SaveChanges();
        }
    }
}
