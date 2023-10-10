using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using HourGlassUnlimited.Games.Sudoku.Views;

namespace HourGlassUnlimited.Games.Sudoku.Tools
{
    public static class SudokuNavigator
    {
        #region Attributes

        private static MainWindow _mainWindow;
        private static Frame _holder;

        private static GamePage _gamePage;
        private static GameMenu _gameMenu;

        #endregion

        #region Properties

        public static MainWindow MainWindow { get { return _mainWindow; } set { _mainWindow = value; } }
        public static Frame Holder { get { return _holder; } set { _holder = value; } }

        public static GamePage GamePage { get { return _gamePage; } set { _gamePage = value; } }
        public static GameMenu GameMenu { get { return _gameMenu; } set { _gameMenu = value; } }

        #endregion

        #region Initialization

        public static void Start()
        {
            MainWindow = new MainWindow();
            Holder = MainWindow.Holder;

            GamePage = new GamePage();
            GameMenu = new GameMenu();

            MainWindow.Show();
            Holder.NavigationService.Navigate(GameMenu);
        }

        #endregion

        #region Navigation

        public static void GamePageView()
        {
            Holder.NavigationService.Navigate(GamePage);
        }

        public static void GameMenuView()
        {
            Holder.NavigationService.Navigate(GameMenu);
        }

        #endregion
    }
}
