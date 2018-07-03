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
using System.Net;
using System.IO;
using System.Xml;
using Microsoft.Practices.Prism.Mvvm;
using converter.ViewModel;


namespace converter.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
                this.DataContext = mainWindowViewModel;
            }
            catch (ConstructorAbortedException)
            {
                System.Environment.Exit(0);
            }
        }
    }
}
