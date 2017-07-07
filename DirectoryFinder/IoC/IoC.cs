using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Container.Register<TreeHandler>(Reuse.Singleton);
            Container.Register<ISnackbarMessageQueue, SnackbarMessageQueue>(Reuse.Singleton, Made.Of(() => new SnackbarMessageQueue()));
            Container.Register<TreeViewModel>(Reuse.Singleton);
            Container.Register<MainViewModel>();
        }
    }
}
