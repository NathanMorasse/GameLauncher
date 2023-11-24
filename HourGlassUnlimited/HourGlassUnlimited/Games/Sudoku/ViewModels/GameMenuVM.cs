using HourGlassUnlimited.Games.Sudoku.Tools;
using HourGlassUnlimited.Tools;
using HourGlassUnlimited.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using HourGlassUnlimited.Games.Sudoku.Models;
using HourGlassUnlimited.Games.Sudoku.DataAccesLayer;
using HourGlassUnlimited.DataAccessLayer;
using HourGlassUnlimited.Models;

namespace HourGlassUnlimited.Games.Sudoku.ViewModels
{
    public class GameMenuVM : VM
    {
        private SudokuGame NewGame = null;
        public ICommand Select_Classic { get; set; }
        public ICommand Select_Daily { get; set; }
        public ICommand Launch_Classic { get; set; }
        public ICommand Launch_Daily { get; set; }

        public GameMenuVM()
        {
            Select_Classic = new CommandLink(Select_Classic_Execute, Select_Classic_CanExecute);
            Select_Daily = new CommandLink(Select_Daily_Execute, Select_Daily_CanExecute);
            Launch_Classic = new CommandLink(Launch_Classic_Execute, Launch_Classic_CanExecute);
        }
        private bool Select_Classic_CanExecute(object parameter) { return true; }
        private async void Select_Classic_Execute(object parameter)
        {
            SudokuDAL dal = new SudokuDAL();
            SudokuGame savedGame = dal.SudokuFact.LoadSave(false);
            if (savedGame == null)
            {
                SudokuNavigator.PartialDifficultyView();
            }
            else
            {
                NewGame = savedGame;
                SudokuNavigator.PartialLoadSaveView();
            }
        }

        private bool Select_Daily_CanExecute(object parameter) { return true; }
        private async void Select_Daily_Execute(object parameter)
        {
            DAL dal = new DAL();
            SudokuDAL sudokuDal = new SudokuDAL();
            SudokuGame savedGame = sudokuDal.SudokuFact.LoadSave(true);
            if (savedGame == null || savedGame.Date.Date != DateTime.Now.Date)
            {
                GameBase gameBase = dal.Games.GetByTitle("Sudoku");
                SudokuGame game = new SudokuGame { Id = gameBase.Id, Title = gameBase.Title, Date= DateTime.Now};
                game.IsDaily = true;
                game.GameBoard = await sudokuDal.SudokuFact.GenerateBoard("hard", true, string.Empty);
                SudokuNavigator.GamePage.SetGame(game);
                SudokuNavigator.GamePageView();
            }
            else
            {
                Board temp = savedGame.GameBoard;
                savedGame.GameBoard = await sudokuDal.SudokuFact.GenerateBoard("hard", savedGame.IsDaily, savedGame.GameBoard.Seed);
                SudokuNavigator.GamePage.SetGame(savedGame);
                SudokuNavigator.GamePageView();
                await Task.Delay(100);
                GamePageVM vm = (GamePageVM)SudokuNavigator.GamePage.DataContext;
                vm.LoadSavedCells(temp.Grid);
            }
        }

        private bool Launch_Classic_CanExecute(object parameter) { return true; }
        private async void Launch_Classic_Execute(object parameter)
        {
            DAL dal = new DAL();
            SudokuDAL sudokuDAL = new SudokuDAL();
            GameBase gameBase = dal.Games.GetByTitle("Sudoku");
            SudokuGame game = new SudokuGame { Id=gameBase.Id, Title=gameBase.Title, Date= DateTime.Now};
            if (parameter.ToString() == "continue")
            {
                if (NewGame != null)
                {
                    game = NewGame;
                    Board temp = NewGame.GameBoard;
                    game.GameBoard = await sudokuDAL.SudokuFact.GenerateBoard(game.GameBoard.Difficulty, game.IsDaily, game.GameBoard.Seed);
                    SudokuNavigator.GamePage.SetGame(game);
                    SudokuNavigator.GamePageView();
                    await Task.Delay(100);
                    GamePageVM vm = (GamePageVM)SudokuNavigator.GamePage.DataContext;
                    vm.LoadSavedCells(temp.Grid);
                }
            }
            else
            {
                if (parameter.ToString() == "restart")
                {
                    SudokuNavigator.PartialDifficultyView();
                }
                else
                {
                    game.GameBoard = await sudokuDAL.SudokuFact.GenerateBoard(parameter.ToString(), false, string.Empty);
                    SudokuNavigator.GamePage.SetGame(game);
                    SudokuNavigator.GamePageView();
                }
            }
        }
    }
}
