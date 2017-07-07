using System.Windows.Controls;
using DirectoryFinder.ViewModels;
using DryIoc;

namespace DirectoryFinder.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            this.DataContext = IoC.Container.Resolve<MainViewModel>();
        }
    }
}