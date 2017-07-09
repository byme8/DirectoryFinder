using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DryIoc;

namespace DirectoryFinder.Views.ProgresShower
{
    /// <summary>
    /// Interaction logic for ProgressShower.xaml
    /// </summary>
    public partial class ProgresShower : UserControl
    {
        public ProgresShower()
        {
            InitializeComponent();
            this.DataContext = IoC.Container.Resolve<ProgresShowerViewModel>();
        }
    }
}
