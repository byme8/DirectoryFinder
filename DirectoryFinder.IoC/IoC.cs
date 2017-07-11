using DirectoryFinder.Domain.Services;
using DirectoryFinder.Services;
using DirectoryFinder.ViewModels;
using DryIoc;

namespace DirectoryFinder
{
    public static class IoC
    {
        public static readonly Container Container;

        static IoC()
        {
            Container = new Container();
            Container.Register<IDirectorySearchHandler, DirectorySearchHandler>(Reuse.Singleton);
            Container.Register<IUIHandler, UIHandler>(Reuse.Singleton);
            Container.Register<ISeralizationHandler, SeralizationHandler>(Reuse.Singleton);
            Container.Register<TreeViewModel>(Reuse.Singleton);
            Container.Register<ProgresNotifierViewModel>(Reuse.Singleton);
            Container.Register<MainViewModel>();
            Container.Register<ErrorsViewModel>();
        }
    }
}