﻿using System;
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
using DirectoryFinder.ViewModels;
using DirectoryFinder.Views;
using DryIoc;
using MaterialDesignThemes.Wpf;

namespace DirectoryFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Root.Children.Add(new MainView());
            this.Root.Children.Add(new Snackbar { MessageQueue = IoC.Container.Resolve<ISnackbarMessageQueue>() as SnackbarMessageQueue });
        }
    }
}
