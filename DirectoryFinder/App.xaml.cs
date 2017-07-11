using System.Threading;
using System.Windows;
using DirectoryFinder.Domain.Providers;
using DirectoryFinder.Domain.Services;
using DirectoryFinder.Providers;
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
            this.RegisterProviders();
            this.StartHandeling();
        }

        private void RegisterProviders()
        {
            IoC.Container.Register<IProgresNotifier, ProgresNotifier>(Reuse.Singleton);
            IoC.Container.Register<IFolderPathProvider, FolderPathProvider>(Reuse.Singleton);
            IoC.Container.Register<ISavePathProvider, SavePathProvider>(Reuse.Singleton);
        }

        private void StartHandeling()
        {
            this.applicationFinished = new CancellationTokenSource();
            IoC.Container.Resolve<IUIHandler>().StartHandeling(applicationFinished.Token);
            IoC.Container.Resolve<ISeralizationHandler>().StartHandeling(applicationFinished.Token);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            this.applicationFinished.Cancel();
        }
    }
}