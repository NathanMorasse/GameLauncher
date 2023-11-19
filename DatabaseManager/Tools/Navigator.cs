using DatabaseManager.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.Tools
{
    public static class Navigator
    {
        public static MainWindow MainWindow { get; set; }

        public static void Start()
        {
            MainWindow = new MainWindow();
            MainWindow.Show();
            MainWindow.Display.NavigationService.Navigate(new DepartmentList());
        }
    }
}
