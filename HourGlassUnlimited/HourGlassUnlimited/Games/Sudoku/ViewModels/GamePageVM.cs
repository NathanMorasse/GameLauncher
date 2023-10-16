using HourGlassUnlimited.ViewModels;
using HourGlassUnlimited.Games.Sudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace HourGlassUnlimited.Games.Sudoku.ViewModels
{
    public class GamePageVM : VM
    {
        private SudokuGame _currentGame;


        public SudokuGame CurrentGame
        {
            get { return _currentGame; }
            set 
            {
                _currentGame = value;
                ChangeValue("Current");
            }
        }

        public ObservableCollection<ObservableCollection<int>> Board
        {
            get { return _currentGame.GameBoard.Grid; }
            set
            {
                _currentGame.GameBoard.Grid = value;
                ChangeValue("Board");
            }
        }

        public GamePageVM()
        {
        }

    }
}
