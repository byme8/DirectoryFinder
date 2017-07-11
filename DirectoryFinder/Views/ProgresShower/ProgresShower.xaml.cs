using System.Windows.Controls;
using DirectoryFinder.ViewModels;
using DryIoc;

namespace DirectoryFinder.Views.ProgresShower
{
    /// <summary>
    /// Interaction logic for ProgressShower.xaml
    /// </summary>
    public partial class ProgresShower : UserControl
    {
        public ProgresShower()
        {
            InitializeComponent();
            this.DataContext = IoC.Container.Resolve<ProgresNotifierViewModel>();
        }
    }
}