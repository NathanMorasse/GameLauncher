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
            SudokuNavigator.PartialDifficultyView();
        }

        private bool Select_Daily_CanExecute(object parameter) { return true; }
        private async void Select_Daily_Execute(object parameter)
        {
            SudokuNavigator.GamePageView();
        }

        private bool Launch_Classic_CanExecute(object parameter) { return true; }
        private async void Launch_Classic_Execute(object parameter) 
        {
            SudokuGame game = new SudokuGame();
            game.IsDaily = false;
            DAL dal = new DAL();
            game.GameBoard = await dal.SudokuFact.GenerateBoard(parameter.ToString());
            SudokuNavigator.GamePage.SetGame(game);
            SudokuNavigator.GamePageView();
        }

        private bool Launch_Daily_CanExecute(object parameter) { return true; }
        private async void Launch_Daily_Execute(object parameter)
        {
            SudokuNavigator.GamePageView();
        }
    }
}
