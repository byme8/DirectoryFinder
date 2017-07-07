using System.Windows.Controls;
using DirectoryFinder.ViewModels;
using DryIoc;

namespace DirectoryFinder.Views
{
    /// <summary>
    /// Interaction logic for TreeView.xaml
    /// </summary>
    public partial class TreeView : UserControl
    {
        public TreeView()
        {
            InitializeComponent();
            this.DataContext = IoC.Container.Resolve<TreeViewModel>();
        }
    }
}