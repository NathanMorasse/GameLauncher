using System;
using System.Collections.Generic;
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

        public GamePage()
        {
            InitializeComponent();
            this.DataContext = new GamePageVM();
            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }

        void dt_Tick(object sender, EventArgs e)
        {
            if (sw.IsRunning)
            {
                TimeSpan ts = sw.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                clock_text.Text = currentTime;
            }
        }

        public void SetGame(SudokuGame game)
        {
            var vm = (GamePageVM)this.DataContext;
            vm.CurrentGame = game;
            vm.CurrentBoard = game.GameBoard.Grid;
            sw.Start();
            dt.Start();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Allow only digits (0-9) and prevent other characters
            if (!IsDigit(e.Text))
            {
                e.Handled = true;
            }
        }

        private bool IsDigit(string text)
        {
            return int.TryParse(text, out _);
        }

        private void Validate_Click(object sender, RoutedEventArgs e)
        {
            sw.Stop();
            dt.Stop();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            sw.Restart();
            dt.Start();
        }
    }
}
