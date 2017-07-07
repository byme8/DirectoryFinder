using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Container.Register<Services.DirectoryService>(Reuse.Singleton);
            Container.Register<MainViewModel>();
            Container.Register<TreeViewModel>();
        }
    }
}
