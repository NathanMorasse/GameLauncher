using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using HourGlassUnlimited.Games.Sudoku.Models;
using HourGlassUnlimited.Games.Sudoku.ViewModels;

namespace HourGlassUnlimited.Games.Sudoku.Views
{
    /// <summary>
    /// Logique d'interaction pour GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        string currentTime = string.Empty;
        TimeSpan startTime = TimeSpan.Zero;
        GamePageVM vm;

        public GamePage()
        {
            InitializeComponent();
            this.DataContext = new GamePageVM();
            vm = (GamePageVM)this.DataContext;
            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);
            this.Loaded += GamePage_Loaded;
        }

        private void GamePage_Loaded(object sender, RoutedEventArgs e)
        {
            if (vm.CurrentGame.IsDaily)
            {
                ResetButton.Visibility = Visibility.Hidden;
                if (ButtonGrid.ColumnDefinitions.Count() == 2)
                {
                    ButtonGrid.ColumnDefinitions.RemoveAt(1);
                }
            }
            LockInitialValues(BoardGrid);
        }

        void dt_Tick(object sender, EventArgs e)
        {
            if (sw.IsRunning)
            {
                TimeSpan ts = sw.Elapsed + startTime;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Hours, ts.Minutes, ts.Seconds );
                clock_text.Text = currentTime;
            }
        }

        public void SetGame(SudokuGame game)
        {
            var vm = (GamePageVM)this.DataContext;
            vm.CurrentGame = game;
            vm.CurrentBoard = game.GameBoard.Grid;
            if (game.TimePassed != null)
            {
                startTime = TimeSpan.Parse(game.TimePassed);
            }
            sw.Start();
            dt.Start();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsDigit(e.Text))
            {
                e.Handled = true;
            }
        }

        private bool IsDigit(string text)
        {
            bool valid = true;
            int input;
            valid = int.TryParse(text, out input);
            if (input <1)
            {
                valid = false;
            }
            return valid;
        }

        private void LockInitialValues(DependencyObject parent)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is TextBox textBox && !string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.IsEnabled = false;
                }
                else
                {
                    LockInitialValues(child);
                }
            }
        }

        public async Task StopTimer()
        {
            sw.Stop();
            dt.Stop();
        }

        public async Task ResetGrid()
        {
            EnableAllTextBoxes(BoardGrid);
            LockInitialValues(BoardGrid);
            sw.Restart();
            dt.Start();
        }

        private void EnableAllTextBoxes(DependencyObject parent)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is TextBox textBox)
                {
                    textBox.IsEnabled = true;
                }
                else
                {
                    EnableAllTextBoxes(child);
                }
            }
        }

    }
}
