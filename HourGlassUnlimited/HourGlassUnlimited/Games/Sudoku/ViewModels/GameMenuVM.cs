using HourGlassUnlimited.Games.Sudoku.Tools;
using HourGlassUnlimited.Tools;
using HourGlassUnlimited.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using HourGlassUnlimited.Games.Sudoku.Models;
using HourGlassUnlimited.Games.Sudoku.DataAccesLayer;
using HourGlassUnlimited.DataAccessLayer;
using HourGlassUnlimited.Models;
using Microsoft.Win32;
using System.Windows;

namespace HourGlassUnlimited.Games.Sudoku.ViewModels
{
    public class GameMenuVM : VM
    {
        private SudokuGame NewGame = null;
        public ICommand Select_Classic { get; set; }
        public ICommand Select_Daily { get; set; }
        public ICommand Launch_Classic { get; set; }
        public ICommand Launch_Daily { get; set; }

        public GameMenuVM()
        {
            Select_Classic = new CommandLink(Select_Classic_Execute, Select_Classic_CanExecute);
            Select_Daily = new CommandLink(Select_Daily_Execute, Select_Daily_CanExecute);
            Launch_Classic = new CommandLink(Launch_Classic_Execute, Launch_Classic_CanExecute);
        }
        private bool Select_Classic_CanExecute(object parameter) { return true; }
        private async void Select_Classic_Execute(object parameter)
        {
            SudokuDAL dal = new SudokuDAL();
            SudokuGame savedGame;
            try
            {
                savedGame = dal.SudokuFact.LoadSave(false);
            }
            catch (Exception e)
            {
                savedGame=null;
                MessageBox.Show("La sauvegarde de votre dernière partie n'a pas chargé correctement vous pouvez commencer une nouvelle partie ou contacter un administrateur. \nErreur interne: "+e.InnerException.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            if (savedGame == null)
            {
                SudokuNavigator.PartialDifficultyView();
            }
            else
            {
                NewGame = savedGame;
                SudokuNavigator.PartialLoadSaveView();
            }
        }

        private bool Select_Daily_CanExecute(object parameter) { return true; }
        private async void Select_Daily_Execute(object parameter)
        {
            DAL dal = new DAL();
            SudokuDAL sudokuDal = new SudokuDAL();
            SudokuGame savedGame;
            try
            {
                savedGame = sudokuDal.SudokuFact.LoadSave(true);
            }
            catch (Exception e)
            {
                savedGame = null;
                MessageBox.Show("La sauvegarde de votre dernière partie n'a pas chargé correctement. Vous pouvez commencer une nouvelle partie ou contacter un administrateur. \nErreur interne: " + e.InnerException.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            if (savedGame == null || savedGame.Date.Date != DateTime.Now.Date)
            {
                try
                {
                    GameBase gameBase = dal.Games.GetByTitle("Sudoku");
                    SudokuGame game = new SudokuGame { Id = gameBase.Id, Title = gameBase.Title, Date= DateTime.Now};
                    game.IsDaily = true;
                    game.GameBoard = await sudokuDal.SudokuFact.GenerateBoard("hard", true, string.Empty, string.Empty);
                    SudokuNavigator.GamePage.SetGame(game);
                    SudokuNavigator.GamePageView();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Échec de création d'une nouvelle partie quotidienne. \nErreur interne: "+e.Message+"\nErreur interne: "+e.InnerException.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                try
                {
                    Board temp = savedGame.GameBoard;
                    savedGame.GameBoard = await sudokuDal.SudokuFact.GenerateBoard("hard", savedGame.IsDaily, savedGame.GameBoard.Seed, savedGame.GameBoard.Notes);
                    SudokuNavigator.GamePage.SetGame(savedGame);
                    SudokuNavigator.GamePageView();
                    await Task.Delay(100);
                    GamePageVM vm = (GamePageVM)SudokuNavigator.GamePage.DataContext;
                    vm.LoadSavedCells(temp.Grid);
                    SudokuNavigator.GamePage.LoadNotes(savedGame.GameBoard.Notes);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Échec de création d'une partie quotidienne. \nErreur interne: " + e.Message + "\nErreur interne: " + e.InnerException.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private bool Launch_Classic_CanExecute(object parameter) { return true; }
        private async void Launch_Classic_Execute(object parameter)
        {
            DAL dal = new DAL();
            SudokuDAL sudokuDAL = new SudokuDAL();
            GameBase gameBase = dal.Games.GetByTitle("Sudoku");
            SudokuGame game = new SudokuGame { Id=gameBase.Id, Title=gameBase.Title, Date= DateTime.Now};
            if (parameter.ToString() == "continue")
            {
                if (NewGame != null)
                {
                    try
                    {
                        game = NewGame;
                        Board temp = NewGame.GameBoard;
                        game.GameBoard = await sudokuDAL.SudokuFact.GenerateBoard(game.GameBoard.Difficulty, game.IsDaily, game.GameBoard.Seed, game.GameBoard.Notes);
                        SudokuNavigator.GamePage.SetGame(game);
                        SudokuNavigator.GamePageView();
                        await Task.Delay(100);
                        GamePageVM vm = (GamePageVM)SudokuNavigator.GamePage.DataContext;
                        vm.LoadSavedCells(temp.Grid);
                        SudokuNavigator.GamePage.LoadNotes(game.GameBoard.Notes);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Échec de chargement de partie. \nErreur interne: " + e.Message + "\nErreur interne: " + e.InnerException.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            else
            {
                if (parameter.ToString() == "restart")
                {
                    SudokuNavigator.PartialDifficultyView();
                }
                else
                {
                    try
                    {
                        game.GameBoard = await sudokuDAL.SudokuFact.GenerateBoard(parameter.ToString(), false, string.Empty, string.Empty);
                        SudokuNavigator.GamePage.SetGame(game);
                        SudokuNavigator.GamePageView();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Échec de création d'une nouvelle partie. \nErreur interne: " + e.Message + "\nErreur interne: " + e.InnerException.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }
    }
}
