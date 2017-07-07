using System.Threading;
using System.Windows;
using DirectoryFinder.Services;
using DryIoc;

namespace DirectoryFinder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IoC.Container.Resolve<TreeHandler>().StartHandeling(CancellationToken.None);
        }
    }
}