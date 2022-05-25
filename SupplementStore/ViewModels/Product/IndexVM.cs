using SupplementStore.Application.Models;
using SupplementStore.ViewModels.Shared;
using System.Collections.Generic;

namespace SupplementStore.ViewModels.Product {

    public class IndexVM {

        public PageVM Page { get; set; } = new PageVM();

        public IEnumerable<ProductDetails> Products { get; set; }

        public Dictionary<string, ProductRating> ProductRatings { get; set; } = new Dictionary<string, ProductRating>();
    }
}
