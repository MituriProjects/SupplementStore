using SupplementStore.Application.Models;
using System.Collections.Generic;

namespace SupplementStore.ViewModels.Product {

    public class IndexVM {

        public int Skip { get; set; }

        public int Take { get; set; } = 10;

        public int AllProductsCount { get; set; }

        public IEnumerable<ProductDetails> Products { get; set; }

        public Dictionary<string, ProductRating> ProductRatings { get; set; } = new Dictionary<string, ProductRating>();
    }
}
