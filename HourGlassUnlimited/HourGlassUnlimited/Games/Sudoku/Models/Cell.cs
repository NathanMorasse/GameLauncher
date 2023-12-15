using HourGlassUnlimited.Games.Sudoku.Tools;
using HourGlassUnlimited.Games.Sudoku.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.Games.Sudoku.Models
{
    public class Cell: INotifyPropertyChanged
    {
        private Int64 _value;
        public Int64 Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged(nameof(Value));
                    if (SudokuNavigator.GamePage != null)
                    {
                        GamePageVM vm = (GamePageVM)SudokuNavigator.GamePage.DataContext;
                        vm.CanValidate = vm.IsBoardFilled();                        
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Cell() { }
    }
}
