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
            EnumVisual(BoardGrid);
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
        private void LockClues()
        {
            //foreach (TextBox tb in FindVisualChildren<TextBox>(Board))
            //{
            //    if (tb.Text == string.Empty || tb.Text == "" || tb.Text == "0")
            //    {
            //        tb.IsEnabled = true;
            //    }
            //    else
            //    {
            //        tb.IsEnabled = false;
            //    }
            //}
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


        static public void EnumVisual(Visual myVisual)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(myVisual); i++)
            {
                // Retrieve child visual at specified index value.
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(myVisual, i);

                if (childVisual is TextBox)
                {
                    TextBox textBox = (TextBox)childVisual;

                    // Do processing of the child visual object.
                    if (textBox.Text == string.Empty || textBox.Text == "" || textBox.Text == "0")
                    {
                        textBox.IsEnabled = true;
                    }
                    else
                    {
                        textBox.IsEnabled = false;
                    }
                }

                // Enumerate children of the child visual object.
                EnumVisual(childVisual);
            }
        }
    }
}
