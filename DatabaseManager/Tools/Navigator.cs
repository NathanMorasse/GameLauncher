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
        public static DepartmentRoomList DepartmentRoomListView { get; set; }
        public static RoomDetail RoomDetailView { get; set; }
        public static FurnitureList FurnitureListView { get; set; }
        public static FurnitureDetail FurnitureDetailView { get; set; }
        public static FurnitureAddition FurnitureAdditionView { get; set; }
        public static RoomFurnitureList RoomFurnitureListView { get; set; }

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

        public static void DepartmentRoomList()
        {
            DepartmentRoomListView = new DepartmentRoomList();
            MainWindow.Display.NavigationService.Navigate(DepartmentRoomListView);
        }

        public static void RoomDetail()
        {
            RoomDetailView = new RoomDetail();
            MainWindow.Display.NavigationService.Navigate(RoomDetailView);
        }

        public static void FurnitureList()
        {
            FurnitureListView = new FurnitureList();
            MainWindow.Display.NavigationService.Navigate(FurnitureListView);
        }

        public static void FurnitureDetail()
        {
            FurnitureDetailView = new FurnitureDetail();
            MainWindow.Display.NavigationService.Navigate(FurnitureDetailView);
        }

        public static void FurnitureAddition()
        {
            FurnitureAdditionView = new FurnitureAddition();
            MainWindow.Display.NavigationService.Navigate(FurnitureAdditionView);
        }

        public static void RoomFurnitureList()
        {
            RoomFurnitureListView = new RoomFurnitureList();
            MainWindow.Display.NavigationService.Navigate(RoomFurnitureListView);
        }
    }
}
