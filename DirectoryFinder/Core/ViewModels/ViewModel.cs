using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace DirectoryFinder.Core.ViewModels
{
    public class ViewModel : ReactiveObject
    {

    }

    public class ViewModel<TValue> : ViewModel
    {
        public TValue Value
        {
            get;
            set;
        }

        public ViewModel(TValue value)
        {
            this.Value = value;
        }
    }
}
