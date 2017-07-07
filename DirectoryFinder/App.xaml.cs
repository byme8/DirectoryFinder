using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using DirectoryFinder.Services;
using DryIoc;
using MaterialDesignThemes.Wpf;

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
