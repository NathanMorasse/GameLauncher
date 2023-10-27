using HourGlassUnlimited.Games.Sudoku.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HourGlassUnlimited.Games.Sudoku.Views
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new SudokuWindowVM();
        }

        private void Holder_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if ((Holder.Content as Page).Title.Contains("Menu"))
            {
                if (Menu_Button.Visibility == Visibility.Visible)
                    Menu_Button.Visibility = Visibility.Hidden;
            }
            else
            {
                if (Menu_Button.Visibility == Visibility.Hidden)
                    Menu_Button.Visibility = Visibility.Visible;
            }
        }

        private void Window_Closing(object sender, EventArgs e)
        {
            SudokuWindowVM vm = (SudokuWindowVM)DataContext;
            vm.Save();
        }
    }
}
