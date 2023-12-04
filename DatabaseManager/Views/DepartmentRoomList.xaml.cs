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
    /// Logique d'interaction pour DepartmentRoomList.xaml
    /// </summary>
    public partial class DepartmentRoomList : Page
    {
        public DepartmentRoomList()
        {
            InitializeComponent();
            this.DataContext = new DepartmentRoomListVM();
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

            if ((this.DataContext as DepartmentRoomListVM).Selected == null)
            {
                See_Button.IsEnabled = false;
                Edit_Button.IsEnabled = false;
                Delete_Button.IsEnabled = false;
            }
            else
            {
                See_Button.IsEnabled = true;
                Edit_Button.IsEnabled = true;
                Delete_Button.IsEnabled = true;
            }
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
            else
            {
                Edit_PopUp.Visibility = Visibility.Collapsed;
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

        public void ShowError(string message)
        {
            if (Confirm_Delete.Visibility == Visibility.Visible)
            {
                Confirm_Delete.Visibility = Visibility.Collapsed;
            }

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
    }
}
