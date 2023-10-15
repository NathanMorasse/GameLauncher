using HourGlassUnlimited.ViewModels;
using HourGlassUnlimited.Games.Sudoku.Models;
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
        private SudokuGame sudokuGame;

        private ObservableCollection<ObservableCollection<int>> _board;

        public ObservableCollection<ObservableCollection<int>> Board
        {
            get { return _board; }
            set
            {
                _board = value;
                ChangeValue("Board");
            }
        }

        public GamePageVM(SudokuGame game)
        {
            Board = game.GameBoard.Grid;
        }

    }
}
