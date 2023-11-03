using HourGlassUnlimited.Games.Sudoku.Tools;
using HourGlassUnlimited.Tools;
using HourGlassUnlimited.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HourGlassUnlimited.Games.Sudoku.ViewModels;
using HourGlassUnlimited.Games.Sudoku.Models;
using HourGlassUnlimited.Games.Sudoku.DataAccesLayer;

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
            Launch_Daily = new CommandLink(Launch_Daily_Execute, Launch_Daily_CanExecute);
        }
        private bool Select_Classic_CanExecute(object parameter) { return true; }
        private async void Select_Classic_Execute(object parameter)
        {
            DAL dal = new DAL();
            SudokuGame savedGame = dal.SudokuFact.LoadSave(false);
            if (savedGame == null)
            {
                SudokuNavigator.PartialDifficultyView();
            }
            else
            {
                savedGame.IsDaily = false;
                NewGame = savedGame;
                SudokuNavigator.PartialLoadSaveView();
            }
        }

        private bool Select_Daily_CanExecute(object parameter) { return true; }
        private async void Select_Daily_Execute(object parameter)
        {
            SudokuNavigator.GamePageView();
        }

        private bool Launch_Classic_CanExecute(object parameter) { return true; }
        private async void Launch_Classic_Execute(object parameter)
        {
            DAL dal = new DAL();
            SudokuGame game = dal.SudokuFact.GetByTitle("Sudoku");
            if (parameter.ToString() == "continue")
            {
                if (NewGame != null)
                {
                    game = NewGame;
                    game.GameBoard = await dal.SudokuFact.GenerateBoard(game.GameBoard.Difficulty, game.IsDaily, game.GameBoard.Seed);
                    SudokuNavigator.GamePage.SetGame(game);
                    SudokuNavigator.GamePageView();
                    SudokuNavigator.GamePage.LoadSavedCells(SudokuNavigator.GamePage.BoardGrid, NewGame.GameBoard.Grid);
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
                    game.GameBoard = await dal.SudokuFact.GenerateBoard(parameter.ToString(), false, string.Empty);
                    SudokuNavigator.GamePage.SetGame(game);
                    SudokuNavigator.GamePageView();
                }
            }
        }

        private bool Launch_Daily_CanExecute(object parameter) { return true; }
        private async void Launch_Daily_Execute(object parameter)
        {
            DAL dal = new DAL();
            SudokuGame game = dal.SudokuFact.GetByTitle("Sudoku");
            game.IsDaily = true;
            game.GameBoard = await dal.SudokuFact.GenerateBoard(parameter.ToString(), true, string.Empty);
            SudokuNavigator.GamePage.SetGame(game);
            SudokuNavigator.GamePageView();
        }
    }
}
