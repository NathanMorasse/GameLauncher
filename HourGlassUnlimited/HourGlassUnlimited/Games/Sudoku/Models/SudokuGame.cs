using HourGlassUnlimited.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.Games.Sudoku.Models
{
    public class SudokuGame : GameBase
    {
        public Board GameBoard { get; set; }
        public bool IsDaily { get; set; }
    }
}
