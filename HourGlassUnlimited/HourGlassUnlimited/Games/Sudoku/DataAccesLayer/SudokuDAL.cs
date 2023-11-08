using HourGlassUnlimited.Games.Sudoku.DataAccesLayer.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.Games.Sudoku.DataAccesLayer
{
    public class SudokuDAL
    {
        private SudokuFactory _sudokuFact = null;

        public SudokuFactory? SudokuFact
        {
            get
            {
                if (_sudokuFact == null)
                {
                    _sudokuFact = new SudokuFactory();
                }
                return _sudokuFact; 
            }
        }


    }
}
