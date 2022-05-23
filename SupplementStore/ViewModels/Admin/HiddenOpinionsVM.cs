using SupplementStore.Application.Models;
using SupplementStore.ViewModels.Shared;
using System.Collections.Generic;

namespace SupplementStore.ViewModels.Admin {

    public class HiddenOpinionsVM {

        public PageVM Page { get; set; } = new PageVM();

        public IEnumerable<HiddenOpinionDetails> HiddenOpinions { get; set; }
    }
}
