using System;

namespace SupplementStore.Application.Results {

    public class ProductToOpineResult {

        public bool IsEmpty { get; set; }

        public string PurchaseId { get; set; }

        public string ProductName { get; set; }

        public DateTime BuyingDate { get; set; }

        public static ProductToOpineResult Empty => new ProductToOpineResult { IsEmpty = true };
    }
}
