namespace SupplementStore.Application.Results {

    public class ProductImageRemoveResult {

        public bool Success { get; set; }

        public static ProductImageRemoveResult Succeeded = new ProductImageRemoveResult { Success = true };

        public static ProductImageRemoveResult Failed = new ProductImageRemoveResult { Success = false };
    }
}
