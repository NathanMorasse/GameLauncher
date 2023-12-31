﻿using System.Windows.Controls;
using HourGlassUnlimited.Games.Sudoku.Views;
using HourGlassUnlimited.Games.Sudoku.Views.Partial;
using HourGlassUnlimited.Games.Sudoku.ViewModels;


namespace HourGlassUnlimited.Games.Sudoku.Tools
{
    public static class SudokuNavigator
    {
        #region Attributes

        private static MainWindow _mainWindow;
        private static Frame _holder;

        private static GamePage _gamePage;
        private static GameMenu _gameMenu;
        private static PartialDifficulty _partialDifficulty;
        private static PartialLoadSave _partialLoadSave;
        private static PartialDescription _partialDescription;

        #endregion

        #region Properties

        public static MainWindow MainWindow { get { return _mainWindow; } set { _mainWindow = value; } }
        public static Frame Holder { get { return _holder; } set { _holder = value; } }

        public static GamePage GamePage { get { return _gamePage; } set { _gamePage = value; } }
        public static GameMenu GameMenu { get { return _gameMenu; } set { _gameMenu = value; } }
        public static PartialDifficulty PartialDifficulty { get { return _partialDifficulty; } set { _partialDifficulty = value; } }
        public static PartialLoadSave PartialLoadSave { get { return _partialLoadSave; } set { _partialLoadSave = value; } }
        public static PartialDescription PartialDescription { get { return _partialDescription; } set { _partialDescription = value; } }
        #endregion

        #region Initialization

        public static void Start()
        {
            MainWindow = new MainWindow();
            Holder = MainWindow.Holder;

            GamePage = new GamePage();
            GameMenu = new GameMenu();
            PartialDifficulty = new PartialDifficulty((GameMenuVM)GameMenu.DataContext);
            PartialLoadSave = new PartialLoadSave((GameMenuVM)GameMenu.DataContext);
            PartialDescription = new PartialDescription((GameMenuVM)GameMenu.DataContext);
            
            MainWindow.Show();
            GameMenuView();
        }

        #endregion

        #region Navigation

        public static void Close()
        {
            _mainWindow.Close();
        }

        public static void GamePageView()
        {
            Holder.NavigationService.Navigate(GamePage);
        }

        public static void GameMenuView()
        {
            Holder.NavigationService.Navigate(GameMenu);
            GameMenu.Partial_PopUp.NavigationService.Navigate(PartialDescription);
        }

        public static void PartialDifficultyView()
        {
            GameMenu.Partial_PopUp.NavigationService.Navigate(PartialDifficulty);
        }

        public static void PartialLoadSaveView()
        {
            GameMenu.Partial_PopUp.NavigationService.Navigate(PartialLoadSave);
        }

        public static void PartialDescriptionView()
        {
            GameMenu.Partial_PopUp.NavigationService.Navigate(PartialDescription);
        }
        #endregion
    }
}
