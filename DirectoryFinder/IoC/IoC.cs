using DirectoryFinder.Services;
using DirectoryFinder.ViewModels;
using DirectoryFinder.Views.ProgresShower;
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
            Container.Register<ProgresShowerViewModel>(Reuse.Singleton);
            Container.Register<ProgresService>(Reuse.Singleton);
            Container.Register<MainViewModel>();
            Container.Register<ErrorsViewModel>();
        }
    }
}