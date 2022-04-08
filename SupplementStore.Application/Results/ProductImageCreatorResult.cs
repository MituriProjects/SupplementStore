namespace SupplementStore.Application.Results {

    public class ProductImageCreatorResult {

        public bool Success { get; set; }

        public static ProductImageCreatorResult Succeeded = new ProductImageCreatorResult { Success = true };

        public static ProductImageCreatorResult Failed = new ProductImageCreatorResult { Success = false };
    }
}
