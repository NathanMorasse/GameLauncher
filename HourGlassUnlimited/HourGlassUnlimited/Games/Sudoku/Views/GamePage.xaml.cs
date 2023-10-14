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
using HourGlassUnlimited.Games.Sudoku.ViewModels;

namespace HourGlassUnlimited.Games.Sudoku.Views
{
    /// <summary>
    /// Logique d'interaction pour GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        public GamePage()
        {
            InitializeComponent();
            this.DataContext = new GamePageVM();
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

        private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
