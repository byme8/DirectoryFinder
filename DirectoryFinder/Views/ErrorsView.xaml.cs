using System.Windows.Controls;
using DirectoryFinder.ViewModels;
using DryIoc;

namespace DirectoryFinder.Views
{
    /// <summary>
    /// Interaction logic for ErrorsView.xaml
    /// </summary>
    public partial class ErrorsView : UserControl
    {
        public ErrorsView()
        {
            InitializeComponent();
            this.DataContext = IoC.Container.Resolve<ErrorsViewModel>();
        }
    }
}