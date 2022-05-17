using SupplementStore.Application.Models;
using System.Collections.Generic;

namespace SupplementStore.ViewModels.Opinion {

    public class IndexVM {

        public bool IsProductToOpineWaiting { get; set; }

        public IEnumerable<OpinionDetails> Opinions { get; set; }
    }
}
