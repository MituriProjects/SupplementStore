using System;

namespace SupplementStore.ViewModels.Opinion {

    public class OpinionCreateVM {

        public string PurchaseId { get; set; }

        public string ProductName { get; set; }

        public DateTime BuyingDate { get; set; }

        [Label]
        public string Text { get; set; }

        public int Stars { get; set; } = 5;
    }
}
