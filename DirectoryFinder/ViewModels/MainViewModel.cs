using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DirectoryFinder.Core.ViewModels;
using DirectoryFinder.Services;
using ReactiveUI;

namespace DirectoryFinder.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            // TODO: Replace by IoC container.
            this.DirectoryFinder = new Services.DirectoryFinder();
            this.DirectorySelector = new DirectorySelector();
        }

        public Services.DirectoryFinder DirectoryFinder
        {
            get;
        }

        public DirectorySelector DirectorySelector
        {
            get;
        }
    }
}
