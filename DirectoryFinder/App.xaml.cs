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
        private CancellationTokenSource applicationFinished;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.applicationFinished = new CancellationTokenSource();
            IoC.Container.Resolve<UIHandler>().StartHandeling(applicationFinished.Token);
            IoC.Container.Resolve<SeralizationHandler>().StartHandeling(applicationFinished.Token);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            this.applicationFinished.Cancel();
        }
    }
}