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
using HourGlassUnlimited.Models;
using HourGlassUnlimited.DataAccessLayer;

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
            SudokuDAL dal = new SudokuDAL();
            string result = await dal.SudokuFact.ValidateBoard(CurrentGame.GameBoard);
            if (result == "valid")
            {
                GameResult = "Grille complétée correctement!";
                SudokuNavigator.GamePage.StopTimer();
                SaveScore();
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
                SudokuDAL sudokuDal = new SudokuDAL();
                DAL dal = new DAL();
                GameBase game = dal.Games.GetByTitle("Sudoku");
                SudokuGame newGame = new SudokuGame { Id=game.Id, Title=game.Title };
                Board newBoard = await sudokuDal.SudokuFact.GenerateBoard(CurrentGame.GameBoard.Difficulty, false, string.Empty);
                newGame.GameBoard = newBoard;
                SudokuNavigator.GamePage.SetGame(newGame);
                GameStatusVisibility = "Hidden";
                await Task.Delay(100);
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

        public void LoadSavedCells(ObservableCollection<ObservableCollection<Cell>> cells)
        {
            int rowIndex = 0;
            int cellIndex = 0;
            foreach (var row in CurrentBoard)
            {
                foreach (var cell in row)
                {
                    if (cell.Value == 0)
                    {
                        cell.Value = cells[rowIndex][cellIndex].Value;
                    }
                    cellIndex++;
                }
                cellIndex = 0;
                rowIndex++;
            }
        }

        public void SaveScore()
        {
            DAL dal = new DAL();
            GameBase game = dal.Games.GetByTitle("Sudoku");
            string category;
            if (CurrentGame.IsDaily)
            {
                category = "Daily";
            }
            else
            {
                category = "Normal";
            }
            Score newScore = new Score(ConnectionHelper.User.Id, game.Id, category, null, TimeSpan.Parse(TimePassed), 0, DateTime.Now);
            dal.Scores.SaveScore(newScore);

        }

        public GamePageVM()
        {
            Validate = new CommandLink(Validate_Execute, Validate_CanExecute);
            Reset = new CommandLink(Reset_Execute, Reset_CanExecute);
        }
    }
}
