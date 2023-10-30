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
        public Board GameBoard { get; set; } = new Board();
        public bool IsDaily { get; set; }
        public string TimePassed { get; set; }
        public SudokuGame() : base() { }
    }
}
