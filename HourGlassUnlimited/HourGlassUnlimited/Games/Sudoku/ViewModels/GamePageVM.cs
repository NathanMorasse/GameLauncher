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

namespace HourGlassUnlimited.Games.Sudoku.ViewModels
{
    public class GamePageVM : VM
    {
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
        public ICommand Validate { get; set; }
        private bool Validate_CanExecute(object parameter) { return true; }
        private async void Validate_Execute(object parameter)
        {
            DAL dal = new DAL();
            string valid = await dal.SudokuFact.ValidateBoard(CurrentGame.GameBoard);
        }

        public GamePageVM()
        {
            Validate = new CommandLink(Validate_Execute, Validate_CanExecute);
        }
    }
}
