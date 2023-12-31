﻿using HourGlassUnlimited.Models.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.Games.Sudoku.Models
{
    public class Board: ModelBase
    {
        public string Seed { get; set; }
        public string Difficulty  { get; set; }
        public string Notes { get; set; }
        public ObservableCollection<ObservableCollection<Cell>> Grid { get; set; } = new ObservableCollection<ObservableCollection<Cell>>();
        public Board() { }
    }
}
