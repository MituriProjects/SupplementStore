using SupplementStore.Application.Models;
using System.Collections.Generic;

namespace SupplementStore.ViewModels.Product {

    public class ProductDetailsVM {

        public ProductDetails Product { get; set; }

        public IEnumerable<ProductOpinionDetails> Opinions { get; set; }

        public IEnumerable<string> Images { get; set; }
    }
}
