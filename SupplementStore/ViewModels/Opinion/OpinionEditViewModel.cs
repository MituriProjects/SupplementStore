using System.ComponentModel.DataAnnotations;

namespace SupplementStore.ViewModels.Opinion {

    public class OpinionEditViewModel {

        public string Id { get; set; }

        [Display(Name = "Treść opinii:")]
        public string Text { get; set; }
    }
}
