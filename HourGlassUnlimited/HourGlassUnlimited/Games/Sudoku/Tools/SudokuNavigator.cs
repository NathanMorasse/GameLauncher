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

        public static MainWindow MainWindowView { get { return _mainWindow; } set { _mainWindow = value; } }
        public static Frame Holder { get { return _holder; } set { _holder = value; } }

        public static GamePage GamePageView { get { return _gamePage; } set { _gamePage = value; } }
        public static GameMenu GameMenuView { get { return _gameMenu; } set { _gameMenu = value; } }

        #endregion

        #region Initialization

        public static void Start()
        {

        }

        #endregion

        #region Navigation



        #endregion
    }
}
