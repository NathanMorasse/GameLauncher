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
using HourGlassUnlimited.Games.Sudoku.Tools;
using System.IO;
using System.Reflection;

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

            GameAccess.LoadGames();

            //string[] str;

            //str = GameAccess.ListGames();

            //GameAccess.LaunchGame(str[0]);
        }

        private void TestFunc()
        {

        }
    }
}
