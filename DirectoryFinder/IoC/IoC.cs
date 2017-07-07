using DirectoryFinder.Services;
using DirectoryFinder.ViewModels;
using DryIoc;
using MaterialDesignThemes.Wpf;

namespace DirectoryFinder
{
    public static class IoC
    {
        public static readonly Container Container;

        static IoC()
        {
            Container = new Container();
            Container.Register<DirectorySearchHandler>(Reuse.Singleton);
            Container.Register<UIHandler>(Reuse.Singleton);
            Container.Register<SeralizationHandler>(Reuse.Singleton);
            Container.Register<TreeViewModel>(Reuse.Singleton);
            Container.Register<MainViewModel>();
            Container.Register<ErrorsViewModel>();
        }
    }
}