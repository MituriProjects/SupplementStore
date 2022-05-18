using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SupplementStore.ViewModels {

    public class LabelAttribute : DisplayNameAttribute {

        public LabelAttribute([CallerMemberName]string callerName = null) {

            DisplayNameValue = $"{callerName}Label";
        }
    }
}
