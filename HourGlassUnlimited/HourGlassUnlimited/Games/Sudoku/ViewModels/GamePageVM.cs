using HourGlassUnlimited.ViewModels;
using HourGlassUnlimited.Games.Sudoku.Models;
using HourGlassUnlimited.Games.Sudoku.DataAccesLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using HourGlassUnlimited.Games.Sudoku.Tools;
using System.Windows.Input;
using HourGlassUnlimited.Tools;
using System.Windows;
using HourGlassUnlimited.Models;
using HourGlassUnlimited.DataAccessLayer;
using HourGlassUnlimited.Exceptions;

namespace HourGlassUnlimited.Games.Sudoku.ViewModels
{
    public class GamePageVM : VM
    {
        private string _timePassed;
        private string _gameStatusVisibility = "Collapsed";
        private string _gameResult;
        private bool _canValidate = false;
        private SudokuGame _currentGame;

        public string Global { get; set; }
        public string Personnal { get; set; }
        public string Daily { get; set; }

        public GamePageVM()
        {
            GetRanking();

            Validate = new CommandLink(Validate_Execute, Validate_CanExecute);
            Reset = new CommandLink(Reset_Execute, Reset_CanExecute);
        }

        private void GetRanking()
        {
            SudokuDAL dal = new SudokuDAL();
            List<string> temp = dal.SudokuFact.GetBestTimes();

            Global = temp[0];
            Personnal = temp[1];
            Daily = temp[2];
        }

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
            string result;
            try
            {
                result = await dal.SudokuFact.ValidateBoard(CurrentGame.GameBoard);
            }
            catch (BoardValidationException e)
            {
                MessageBox.Show("Une erreur est survenu lors de la validation de votre grille. \nErreur interne: " + e.Message + "\nErreur interne: " + e.InnerException.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning); MessageBox.Show("Échec de création d'une partie quotidienne. \nErreur interne: " + e.Message + "\nErreur interne: " + e.InnerException.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                result = "invalide";
            }

            if (result == "valid")
            {
                GameResult = "Grille complétée correctement!";
                SudokuNavigator.GamePage.StopTimer();
                if (CurrentGame.IsDaily)
                {
                    SaveScore();

                }
                SudokuNavigator.GamePage.ValidateButton.IsEnabled = false;
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
                try
                {
                    SudokuDAL sudokuDal = new SudokuDAL();
                    DAL dal = new DAL();
                    GameBase game = dal.Games.GetByTitle("Sudoku");
                    SudokuGame newGame = new SudokuGame { Id=game.Id, Title=game.Title };
                    Board newBoard = await sudokuDal.SudokuFact.GenerateBoard(CurrentGame.GameBoard.Difficulty, false, string.Empty, "");
                    newGame.GameBoard = newBoard;
                    SudokuNavigator.GamePage.SetGame(newGame);
                    GameStatusVisibility = "Collapsed";
                    await Task.Delay(100);
                    await SudokuNavigator.GamePage.ResetGrid();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Échec création d'une nouvelle partie. \nErreur interne: " + e.Message + "\nErreur interne: " + e.InnerException.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
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
            Score newScore = new Score(0, ConnectionHelper.User.Username, game.Id, category, null, TimeSpan.Parse(TimePassed), 0, DateTime.Now);
            dal.Scores.SaveScore(newScore);
            dal.Scores.UpdatePoints(game.Id);

        }

        public bool CompletedDaily()
        {
            return false;
        }
    }
}
