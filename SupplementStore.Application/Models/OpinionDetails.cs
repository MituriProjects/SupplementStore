using System;

namespace SupplementStore.Application.Models {

    public class OpinionDetails {

        public string Id { get; set; }

        public string ProductName { get; set; }

        public DateTime BuyingDate { get; set; }

        public int Stars { get; set; }

        public string Text { get; set; }
    }
}
