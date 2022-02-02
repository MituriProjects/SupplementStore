using SupplementStore.Application.Models;
using System.Collections.Generic;

namespace SupplementStore.Application.Results {

    public class ProductProviderResult {

        public int AllProductsCount { get; set; }

        public IEnumerable<ProductDetails> Products { get; set; }
    }
}