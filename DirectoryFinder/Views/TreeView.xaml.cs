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
            this.DataContext = this.TreeViewModel = IoC.Container.Resolve<TreeViewModel>(); ;
        }

        public TreeViewModel TreeViewModel
        {
            get;
            private set;
        }
    }
}