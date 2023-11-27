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
    /// Logique d'interaction pour RoomList.xaml
    /// </summary>
    public partial class RoomList : Page
    {
        public RoomList()
        {
            InitializeComponent();
            this.DataContext = new RoomVM();
        }

        private void Room_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Edit_PopUp.Visibility != Visibility.Collapsed)
            {
                Edit_PopUp.Visibility = Visibility.Collapsed;
            }

            if (Confirm_Delete.Visibility != Visibility.Collapsed)
            {
                Confirm_Delete.Visibility = Visibility.Collapsed;
            }

            See_Button.IsEnabled = true;
            Edit_Button.IsEnabled = true;
            Delete_Button.IsEnabled = true;
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Confirm_Delete.Visibility != Visibility.Collapsed)
            {
                Confirm_Delete.Visibility = Visibility.Collapsed;
            }

            if (Edit_PopUp.Visibility != Visibility.Visible)
            {
                Edit_PopUp.Visibility = Visibility.Visible;
            }
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Edit_PopUp.Visibility != Visibility.Collapsed)
            {
                Edit_PopUp.Visibility = Visibility.Collapsed;
            }

            if (Confirm_Delete.Visibility != Visibility.Visible)
            {
                Confirm_Delete.Visibility = Visibility.Visible;
            }
        }

        private void Confirm_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Confirm_Delete.Visibility != Visibility.Collapsed)
            {
                Confirm_Delete.Visibility = Visibility.Collapsed;
            }
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Edit_PopUp.Visibility != Visibility.Collapsed)
            {
                Edit_PopUp.Visibility = Visibility.Collapsed;
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
