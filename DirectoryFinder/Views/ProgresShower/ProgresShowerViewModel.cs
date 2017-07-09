using DirectoryFinder.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace DirectoryFinder.Views.ProgresShower
{
    public class ProgresShowerViewModel : ViewModel
    {
        public ProgresShowerViewModel()
        {
            this.Tasks = new ObservableCollection<string>();
            this.Tasks.CollectionChanged += Tasks_CollectionChanged;
        }

        void Tasks_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.RaisePropertyChanged("Visibility");
        }

        public ObservableCollection<string> Tasks
        {
            get;
            private set;
        }

        public bool Visibility
        {
            get
            {
                return this.Tasks.Any();
            }
        }
    }
}
