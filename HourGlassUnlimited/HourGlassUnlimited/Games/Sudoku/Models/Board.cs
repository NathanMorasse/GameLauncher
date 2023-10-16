using HourGlassUnlimited.Models.Base;
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
        public ObservableCollection<ObservableCollection<int>> Grid { get; set; } = new ObservableCollection<ObservableCollection<int>>();

    }
}
