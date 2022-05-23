using SupplementStore.Application.Models;
using System.Collections.Generic;

namespace SupplementStore.Application.Results {

    public class HiddenOpinionListResult {

        public int AllHiddenOpinionsCount { get; set; }

        public IEnumerable<HiddenOpinionDetails> HiddenOpinions { get; set; }
    }
}
