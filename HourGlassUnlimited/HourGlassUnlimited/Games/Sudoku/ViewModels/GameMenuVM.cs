using HourGlassUnlimited.Games.Sudoku.Tools;
using HourGlassUnlimited.Tools;
using HourGlassUnlimited.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HourGlassUnlimited.Games.Sudoku.ViewModels
{
    public class GameMenuVM : VM
    {
        public ICommand Normal { get; set; }

        public GameMenuVM()
        {
            this.Normal = new CommandLink(Normal_Execute, Normal_CanExecute);
        }

        private bool Normal_CanExecute(object parameter) { return true; }
        private void Normal_Execute(object parameter) { SudokuNavigator.GamePageView(); }
    }
}
