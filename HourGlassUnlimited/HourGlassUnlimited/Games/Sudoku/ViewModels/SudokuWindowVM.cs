using HourGlassUnlimited.Games.Sudoku.DataAccesLayer;
using HourGlassUnlimited.Games.Sudoku.Tools;
using HourGlassUnlimited.Games.Sudoku.Views;
using HourGlassUnlimited.Tools;
using HourGlassUnlimited.ViewModels;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HourGlassUnlimited.Games.Sudoku.ViewModels
{
    public class SudokuWindowVM : VM
    {
        public ICommand Exit_To_Launcher { get; set; }
        public ICommand Back_To_Menu { get; set; }

        public SudokuWindowVM() : base()
        {
            this.Exit_To_Launcher = new CommandLink(Exit_To_Launcher_Execute, Exit_To_Launcher_CanExecute);
            this.Back_To_Menu = new CommandLink(Back_To_Menu_Execute, Back_To_Menu_CanExecute);
        }

        private bool Exit_To_Launcher_CanExecute(object parameter) { return true; }
        private async void Exit_To_Launcher_Execute(object parameter)
        {
            if (Navigator.MainWindow.IsVisible)
            {
                Navigator.MainWindow.WindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                Navigator.Start();
                Navigator.SignInView();
                Navigator.MainWindow.Show();
            }

            SudokuNavigator.Close();
        }

        private bool Back_To_Menu_CanExecute(object parameter) { return true; }
        private async void Back_To_Menu_Execute(object parameter)
        {
            Save();
            SudokuNavigator.GameMenuView();
            SudokuNavigator.GamePage = new Views.GamePage();
        }

        public void Save()
        {
            SudokuDAL dal = new SudokuDAL();
            GamePageVM vm = (GamePageVM)SudokuNavigator.GamePage.DataContext;
            vm.CurrentGame.GameBoard.Notes = SudokuNavigator.GamePage.GetNotesString();
            if (!vm.CurrentGame.IsDaily)
            {
                if (MessageBox.Show("Voulez-vous sauvegarder la partie en cours?",
                    "Sauvegarde",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        dal.SudokuFact.SaveGame(vm.CurrentGame, vm.TimePassed);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Une erreur est survenue lors de la sauvegarde de votre partie. \nErreur interne: " + e.Message + "\nErreur interne: " + e.InnerException.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            else
            {
                dal.SudokuFact.SaveGame(vm.CurrentGame, vm.TimePassed);
            }
        }
    }
}
