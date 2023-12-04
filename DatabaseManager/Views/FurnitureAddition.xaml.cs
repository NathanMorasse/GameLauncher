using DatabaseManager.ViewModels;
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

namespace DatabaseManager.Views
{
    /// <summary>
    /// Logique d'interaction pour FurnitureAddition.xaml
    /// </summary>
    public partial class FurnitureAddition : Page
    {
        public FurnitureAddition()
        {
            InitializeComponent();
            this.DataContext = new FurnitureAdditionVM();
        }

        public void ShowError(string message)
        {
            ErrorPopUpText.Text = message;

            if (ErrorPopUp.Visibility != Visibility.Visible)
            {
                ErrorPopUp.Visibility = Visibility.Visible;
            }
        }

        private void Remove_Error(object sender, RoutedEventArgs e)
        {
            if (ErrorPopUp.Visibility != Visibility.Collapsed)
            {
                ErrorPopUp.Visibility = Visibility.Collapsed;
            }
        }

        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[0-9.\\-]+"))
            {
                e.Handled = true;
            }
        }
    }
}
