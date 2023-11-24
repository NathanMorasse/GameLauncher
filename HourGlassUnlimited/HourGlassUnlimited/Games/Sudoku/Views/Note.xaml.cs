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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HourGlassUnlimited.Games.Sudoku.Views
{
    /// <summary>
    /// Logique d'interaction pour Note.xaml
    /// </summary>
    public partial class Note : UserControl
    {
        public Note()
        {
            InitializeComponent();
        }

        private void Note_Checked(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(NoteContainer);
            NoteContainer.BorderBrush = Brushes.Blue;
            NoteContainer.BorderThickness = new Thickness(2);
        }

        private void Note_Unchecked(object sender, RoutedEventArgs e)
        {
            NoteContainer.BorderThickness = new Thickness(0);
        }

        private void NoteContainer_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.NumPad1:
                case Key.D1:
                    One.Visibility = ToggleVisibility(One.Visibility);
                    break;
                case Key.NumPad2:
                case Key.D2:
                    Two.Visibility = ToggleVisibility(Two.Visibility);
                    break;
                case Key.NumPad3:
                case Key.D3:
                    Three.Visibility = ToggleVisibility(Three.Visibility);
                    break;
                case Key.NumPad4:
                case Key.D4:
                    Four.Visibility = ToggleVisibility(Four.Visibility);
                    break;
                case Key.NumPad5:
                case Key.D5:
                    Five.Visibility = ToggleVisibility(Five.Visibility);
                    break;
                case Key.NumPad6:
                case Key.D6:
                    Six.Visibility = ToggleVisibility(Six.Visibility);
                    break;
                case Key.NumPad7:
                case Key.D7:
                    Seven.Visibility = ToggleVisibility(Seven.Visibility);
                    break;
                case Key.NumPad8:
                case Key.D8:
                    Eight.Visibility = ToggleVisibility(Eight.Visibility);
                    break;
                case Key.NumPad9:
                case Key.D9:
                    Nine.Visibility = ToggleVisibility(Nine.Visibility);
                    break;
                case Key.Back:
                    One.Visibility = ToggleVisibility(One.Visibility);
                    Two.Visibility = ToggleVisibility(Two.Visibility);
                    Three.Visibility = ToggleVisibility(Three.Visibility);
                    Four.Visibility = ToggleVisibility(Four.Visibility);
                    Five.Visibility = ToggleVisibility(Five.Visibility);
                    Six.Visibility = ToggleVisibility(Six.Visibility);
                    Seven.Visibility = ToggleVisibility(Seven.Visibility);
                    Eight.Visibility = ToggleVisibility(Eight.Visibility);
                    Nine.Visibility = ToggleVisibility(Nine.Visibility);
                    break;
                default:
                    break;
            }
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).Visibility = ToggleVisibility((sender as Button).Visibility);
        }

        private Visibility ToggleVisibility(Visibility visibility)
        {
            return visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }
    }
}