using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using HourGlassUnlimited.DataAccessLayer;
using HourGlassUnlimited.Games.Sudoku.DataAccesLayer;
using HourGlassUnlimited.Games.Sudoku.Models;
using HourGlassUnlimited.Games.Sudoku.Tools;
using HourGlassUnlimited.Games.Sudoku.ViewModels;
using HourGlassUnlimited.Models;

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
        bool isDailyCompletedChecked = false;
        List<Note> notesObjects = new List<Note>();
        int notesCursor = 0;

        public GamePage()
        {
            InitializeComponent();
            this.DataContext = new GamePageVM();
            vm = (GamePageVM)this.DataContext;
            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);
            this.Loaded += GamePage_Loaded;
            NavigationCommands.BrowseBack.InputGestures.Clear();
            NavigationCommands.BrowseForward.InputGestures.Clear();
        }

        private async void GamePage_Loaded(object sender, RoutedEventArgs e)
        {
            if (vm.CurrentGame.IsDaily)
            {
                ResetButton.Visibility = Visibility.Collapsed;
                DAL dal = new DAL();
                GameBase game = dal.Games.GetByTitle("Sudoku");
                if (dal.Scores.UserCompletedDaily(game.Id))
                {
                    ValidateButton.IsEnabled = false;
                    vm.GameResult = "Grille complétée correctement!";
                    vm.GameStatusVisibility = "Visible";
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
                if (!isDailyCompletedChecked && vm.CurrentGame.IsDaily)
                {
                    DAL dal = new DAL();
                    GameBase game = dal.Games.GetByTitle("Sudoku");
                    if (dal.Scores.UserCompletedDaily(game.Id))
                    {
                        StopTimer();
                        isDailyCompletedChecked = true;
                    }
                }
            }
        }

        public void SetGame(SudokuGame game)
        {
            var vm = (GamePageVM)this.DataContext;
            vm.CurrentGame = game;
            vm.CurrentBoard = game.GameBoard.Grid;
            if (game.TimePassed != null)
            {
                if (game.IsDaily)
                {
                    startTime = DateTime.Now - game.Date;
                }
                else
                {
                    startTime = TimeSpan.Parse(game.TimePassed);
                }
            }
            sw.Start();
            dt.Start();
        }

        public void LoadNotes(string notes)
        {
            if (notes != null && notes != String.Empty)
            {
                notesObjects = new List<Note>();
                GetAllNotes(NotesGrid);
                string[] notesBoard = notes.Split('\u003B');
                for (int i = 0; i < notesBoard.Count(); i++)
                {
                    if (notesBoard[i].Contains("1"))
                    {
                        notesObjects[i].One.Visibility = Visibility.Visible;
                    }
                    if (notesBoard[i].Contains("2"))
                    {
                        notesObjects[i].Two.Visibility = Visibility.Visible;
                    }
                    if (notesBoard[i].Contains("3"))
                    {
                        notesObjects[i].Three.Visibility = Visibility.Visible;
                    }
                    if (notesBoard[i].Contains("4"))
                    {
                        notesObjects[i].Four.Visibility = Visibility.Visible;
                    }
                    if (notesBoard[i].Contains("5"))
                    {
                        notesObjects[i].Five.Visibility = Visibility.Visible;
                    }
                    if (notesBoard[i].Contains("6"))
                    {
                        notesObjects[i].Six.Visibility = Visibility.Visible;
                    }
                    if (notesBoard[i].Contains("7"))
                    {
                        notesObjects[i].Seven.Visibility = Visibility.Visible;
                    }
                    if (notesBoard[i].Contains("8"))
                    {
                        notesObjects[i].Eight.Visibility = Visibility.Visible;
                    }
                    if (notesBoard[i].Contains("9"))
                    {
                        notesObjects[i].Nine.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        public string GetNotesString()
        {
            string notesString = "";
            notesObjects = new List<Note>();
            GetAllNotes(NotesGrid);
            for (int i = 0; i < notesObjects.Count(); i++)
            {
                if (notesObjects[i].One.Visibility == Visibility.Visible)
                {
                    notesString = notesString + "1";
                }
                if (notesObjects[i].Two.Visibility == Visibility.Visible)
                {
                    notesString = notesString + "2";
                }
                if (notesObjects[i].Three.Visibility == Visibility.Visible)
                {
                    notesString = notesString + "3";
                }
                if (notesObjects[i].Four.Visibility == Visibility.Visible)
                {
                    notesString = notesString + "4";
                }
                if (notesObjects[i].Five.Visibility == Visibility.Visible)
                {
                    notesString = notesString + "5";
                }
                if (notesObjects[i].Six.Visibility == Visibility.Visible)
                {
                    notesString = notesString + "6";
                }
                if (notesObjects[i].Seven.Visibility == Visibility.Visible)
                {
                    notesString = notesString + "7";
                }
                if (notesObjects[i].Eight.Visibility == Visibility.Visible)
                {
                    notesString = notesString + "8";
                }
                if (notesObjects[i].Nine.Visibility == Visibility.Visible)
                {
                    notesString = notesString + "9";
                }

                notesString = notesString + ";";
            }
            return notesString;
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
            startTime = TimeSpan.Zero;
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

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Êtes-vous sur de vouloir éffacer tous les chiffres et les notes que vous avez ajouter sur la grille?",
                    "Éffacer la grille",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ClearBoard(BoardGrid);
                ClearNotes(NotesGrid);
            }
        }

        private void ClearBoard(DependencyObject parent)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is TextBox textBox && textBox.IsEnabled)
                {
                    textBox.Text = string.Empty;
                }
                else
                {
                    ClearBoard(child);
                }
            }
        }

        private void ClearNotes(DependencyObject parent)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is Note note)
                {
                    note.One.Visibility = Visibility.Hidden;
                    note.Two.Visibility = Visibility.Hidden;
                    note.Three.Visibility = Visibility.Hidden;
                    note.Four.Visibility = Visibility.Hidden;
                    note.Five.Visibility = Visibility.Hidden;
                    note.Six.Visibility = Visibility.Hidden;
                    note.Seven.Visibility = Visibility.Hidden;
                    note.Eight.Visibility = Visibility.Hidden;
                    note.Nine.Visibility = Visibility.Hidden;
                }
                else
                {
                    ClearNotes(child);
                }
            }
        }

        private void GetAllNotes(DependencyObject parent)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is Note note)
                {
                    notesObjects.Add(note);
                }
                else
                {
                    GetAllNotes(child);
                }
            }
        }

        public void DisableNote(int position)
        {
            notesCursor = 0;
            FindNoteToDisable(NotesGrid, position);
        }

        public void FindNoteToDisable(DependencyObject parent, int positionToDisable)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is Note note )
                {
                    if (notesCursor == positionToDisable)
                    {
                        note.Visibility = Visibility.Hidden;
                    }
                    notesCursor++;
                }
                else
                {
                    FindNoteToDisable(child, positionToDisable);
                }
            }
        }

        private void NotesButton_Checked(object sender, RoutedEventArgs e)
        {
            NotesViewBox.Visibility = NotesViewBox.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
            NotesButton.Background = new SolidColorBrush(Color.FromRgb(0,45,179));
            NotesButton.Foreground = Brushes.Black;
        }

        private void NotesButton_Unchecked(object sender, RoutedEventArgs e)
        {
            NotesViewBox.Visibility = NotesViewBox.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
            NotesButton.Background = new SolidColorBrush(Color.FromRgb(0, 57, 230));
            NotesButton.Foreground = Brushes.LightGray;
        }
    }
}
