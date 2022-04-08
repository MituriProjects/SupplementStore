namespace SupplementStore.Application.Results {

    public class ProductImageRemoverResult {

        public bool Success { get; set; }

        public static ProductImageRemoverResult Succeeded = new ProductImageRemoverResult { Success = true };

        public static ProductImageRemoverResult Failed = new ProductImageRemoverResult { Success = false };
    }
}
