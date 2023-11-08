using HourGlassUnlimited.Games.Sudoku.ViewModels;
using System.Windows.Controls;

namespace HourGlassUnlimited.Games.Sudoku.Views.Partial
{
    /// <summary>
    /// Logique d'interaction pour PartialLoadSave.xaml
    /// </summary>
    public partial class PartialLoadSave : Page
    {
        public PartialLoadSave(GameMenuVM context)
        {
            InitializeComponent();
            this.DataContext = context;
        }
    }
}
