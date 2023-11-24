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
        public static DepartmentList DepartmentListView { get; set; }
        public static RoomList RoomListView { get; set; }

        public static void Start()
        {
            MainWindow = new MainWindow();
            MainWindow.Show();
            DepartmentList();
        }

        public static void DepartmentList()
        {
            DepartmentListView = new DepartmentList();
            MainWindow.Display.NavigationService.Navigate(DepartmentListView);
        }

        public static void RoomList()
        {
            RoomListView = new RoomList();
            MainWindow.Display.NavigationService.Navigate(RoomListView);
        }
    }
}
