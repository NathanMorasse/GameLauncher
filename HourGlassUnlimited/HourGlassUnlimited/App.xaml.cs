using HourGlassUnlimited.Tools;
using HourGlassUnlimited.Views;
using HourGlassUnlimited.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HourGlassUnlimited
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Navigator.Start();

            Navigator.MainWindow.Show();

            Navigator.SignInView();
        }
    }
}
