using HourGlassUnlimited.Games.Sudoku.ViewModels;
using System.Windows.Controls;

namespace HourGlassUnlimited.Games.Sudoku.Views.Partial
{
    /// <summary>
    /// Logique d'interaction pour PartialDescription.xaml
    /// </summary>
    public partial class PartialDescription : Page
    {
        public PartialDescription(GameMenuVM context)
        {
            InitializeComponent();
            this.DataContext = context;
        }
    }
}
