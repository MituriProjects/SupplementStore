using System;
using System.ComponentModel.DataAnnotations;

namespace SupplementStore.ViewModels.Opinion {

    public class OpinionCreateViewModel {

        public string PurchaseId { get; set; }

        public string ProductName { get; set; }

        public DateTime BuyingDate { get; set; }

        [Display(Name = "Twoja opinia:")]
        public string Text { get; set; }

        public int Stars { get; set; } = 5;
    }
}
