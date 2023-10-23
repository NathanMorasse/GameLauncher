using HourGlassUnlimited.ViewModels;
using HourGlassUnlimited.Games.Sudoku.Models;
using HourGlassUnlimited.Games.Sudoku.DataAccesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using HourGlassUnlimited.Games.Sudoku.Tools;
using System.Windows.Input;
using HourGlassUnlimited.Tools;
using System.Diagnostics;
using System.Windows.Threading;

namespace HourGlassUnlimited.Games.Sudoku.ViewModels
{
    public class GamePageVM : VM
    {
        private string _timePassed;
        private string _gameEnded = "Hidden";
        private string _gameResult;

        private SudokuGame _currentGame;

        public SudokuGame CurrentGame
        {
            get 
            {
                if (_currentGame == null)
                {
                    _currentGame = new SudokuGame();
                }
                return _currentGame; 
            }
            set 
            {
                _currentGame = value;
                ChangeValue("Current");
            }
        }

        public ObservableCollection<ObservableCollection<Cell>> CurrentBoard
        {
            get
            {
                return CurrentGame.GameBoard.Grid;
            }
            set
            {
                CurrentGame.GameBoard.Grid = value;
                ChangeValue("CurrentBoard");
            }
        }

        public string TimePassed
        {
            get
            {
                return _timePassed;
            }
            set
            {
                if (_timePassed == value)
                    return;
                _timePassed = value;
                ChangeValue("TimePassed");
            }
        }

        public string GameEnded
        {
            get
            {
                return _gameEnded;
            }
            set
            {
                _gameEnded = value;
                ChangeValue("GameEnded");
            }
        }

        public string GameResult
        {
            get
            {
                return _gameResult;
            }
            set
            {
                _gameResult = value;
                ChangeValue("GameResult");
            }
        }

        public ICommand Validate { get; set; }
        private bool Validate_CanExecute(object parameter) { return true; }
        private async void Validate_Execute(object parameter)
        {
            GameEnded = "Visible";
            DAL dal = new DAL();
            GameResult = await dal.SudokuFact.ValidateBoard(CurrentGame.GameBoard);
        }

        public ICommand Reset { get; set; }
        private bool Reset_CanExecute(object parameter) { return true; }
        private async void Reset_Execute(object parameter)
        {
            DAL dal = new DAL();
            Board newBoard = await dal.SudokuFact.GenerateBoard("random");
            CurrentBoard = newBoard.Grid;
            GameEnded = "Hidden";
            await Task.Delay(100); // solution temporaire
            await SudokuNavigator.GamePage.ResetGrid();
        }

        public GamePageVM()
        {
            Validate = new CommandLink(Validate_Execute, Validate_CanExecute);
            Reset = new CommandLink(Reset_Execute, Reset_CanExecute);
        }
    }
}
