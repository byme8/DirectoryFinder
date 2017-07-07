using System.Windows;
using DirectoryFinder.Views;
using DryIoc;
using MaterialDesignThemes.Wpf;

namespace DirectoryFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Root.Children.Add(new MainView());
        }
    }
}