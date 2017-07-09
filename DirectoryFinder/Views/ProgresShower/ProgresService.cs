using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryFinder.Views.ProgresShower
{
    public class ProgresService
    {
        private ProgresShowerViewModel viewModel;
        
        public ProgresService(ProgresShowerViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public void Start(string task)
        {
            App.Current.Dispatcher.Invoke(() => this.viewModel.Tasks.Add(task));
        }

        public void Stop(string task)
        {
            App.Current.Dispatcher.Invoke(() => this.viewModel.Tasks.Remove(task));
        }
    }
}
