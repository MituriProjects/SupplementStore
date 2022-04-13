using System.ComponentModel.DataAnnotations;

namespace SupplementStore.ViewModels.Opinion {

    public class OpinionEditVM {

        public string Id { get; set; }

        [Display(Name = "Treść opinii:")]
        public string Text { get; set; }
    }
}
