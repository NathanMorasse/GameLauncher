using HourGlassUnlimited.Games.Sudoku.Tools;
using HourGlassUnlimited.Tools;
using HourGlassUnlimited.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HourGlassUnlimited.Games.Sudoku.ViewModels;
using HourGlassUnlimited.Games.Sudoku.Models;
using HourGlassUnlimited.Games.Sudoku.DataAccesLayer;

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
        private async void Normal_Execute(object parameter) 
        {
            SudokuGame game = new SudokuGame();
            game.IsDaily = true;
            DAL dal = new DAL();
            game.GameBoard = await dal.SudokuFact.GenerateBoard("easy");
            SudokuNavigator.GamePage.SetGame(game);
            SudokuNavigator.GamePageView(); 
        }
    }
}
