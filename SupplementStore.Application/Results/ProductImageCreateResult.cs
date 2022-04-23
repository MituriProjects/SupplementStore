namespace SupplementStore.Application.Results {

    public class ProductImageCreateResult {

        public bool Success { get; set; }

        public static ProductImageCreateResult Succeeded = new ProductImageCreateResult { Success = true };

        public static ProductImageCreateResult Failed = new ProductImageCreateResult { Success = false };
    }
}
