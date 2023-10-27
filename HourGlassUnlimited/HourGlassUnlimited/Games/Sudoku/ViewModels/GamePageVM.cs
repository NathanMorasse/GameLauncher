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
using System.Windows;
using System.Windows.Controls;

namespace HourGlassUnlimited.Games.Sudoku.ViewModels
{
    public class GamePageVM : VM
    {
        private string _timePassed;
        private string _gameStatusVisibility = "Hidden";
        private string _gameResult;
        private bool _canValidate = false;
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
                CanValidate = IsBoardFilled();
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

        public string GameStatusVisibility
        {
            get
            {
                return _gameStatusVisibility;
            }
            set
            {
                _gameStatusVisibility = value;
                ChangeValue("GameStatusVisibility");
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

        public bool CanValidate
        {
            get
            {
                return _canValidate;
            }
            set
            {
                _canValidate = value;
                ChangeValue("CanValidate");
            }
        }

        public ICommand Validate { get; set; }
        private bool Validate_CanExecute(object parameter) { return true; }
        private async void Validate_Execute(object parameter)
        {
            DAL dal = new DAL();
            string result = await dal.SudokuFact.ValidateBoard(CurrentGame.GameBoard);
            if (result == "valid")
            {
                GameResult = "Grille complétée correctement!";
                SudokuNavigator.GamePage.StopTimer();
            }
            else
            {
                if (IsBoardFilled())
                {
                    GameResult = "Grille invalide :(";
                }
                else
                {
                    GameResult = "Grille Incomplete";
                }
            }
            GameStatusVisibility = "Visible";
        }

        public ICommand Reset { get; set; }
        private bool Reset_CanExecute(object parameter) { return true; }
        private async void Reset_Execute(object parameter)
        {
            if (MessageBox.Show("Êtes-vous sur de vouloir recommencer avec une nouvelle grille?",
                    "Nouvelle partie",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DAL dal = new DAL();
                Board newBoard = await dal.SudokuFact.GenerateBoard("medium", false);
                CurrentBoard = newBoard.Grid;
                GameStatusVisibility = "Hidden";
                await Task.Delay(10);
                await SudokuNavigator.GamePage.ResetGrid();
            }
        }

        public bool IsBoardFilled()
        {
            if (CurrentBoard.Count != 9)
            {
                return false; 
            }

            foreach (var row in CurrentBoard)
            {
                if (row.Count != 9)
                {
                    return false; 
                }

                foreach (var cell in row)
                {
                    if (cell.Value < 1 || cell.Value > 9)
                    {
                        return false; 
                    }
                }
            }

            return true;
        }

        public GamePageVM()
        {
            Validate = new CommandLink(Validate_Execute, Validate_CanExecute);
            Reset = new CommandLink(Reset_Execute, Reset_CanExecute);
        }
    }
}
