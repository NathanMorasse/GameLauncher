using HourGlassUnlimited.Games.Sudoku.DataAccesLayer.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.Games.Sudoku.DataAccesLayer
{
    public class DAL
    {
        private SudokuFactory _sudokuFact = null;
        private ScoreFactory _scoreFact = null;

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

        public ScoreFactory? ScoreFact
        {
            get
            {
                if (_scoreFact == null)
                {
                    _scoreFact = new ScoreFactory();
                }
                return _scoreFact;
            }
        }
    }
}
