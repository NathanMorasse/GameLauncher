﻿using HourGlassUnlimited.Games.Sudoku.ViewModels;
using System.Windows.Controls;

namespace HourGlassUnlimited.Games.Sudoku.Views
{
    /// <summary>
    /// Logique d'interaction pour PartialDifficulty.xaml
    /// </summary>
    public partial class PartialDifficulty : Page
    {
        public PartialDifficulty(GameMenuVM context)
        {
            InitializeComponent();
            this.DataContext = context;
        }
    }
}
