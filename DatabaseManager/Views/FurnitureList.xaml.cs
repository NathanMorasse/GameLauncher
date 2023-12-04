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
    /// Logique d'interaction pour FurnitureList.xaml
    /// </summary>
    public partial class FurnitureList : Page
    {
        public FurnitureList()
        {
            InitializeComponent();
            this.DataContext = new FurnitureVM();
        }

        private void Furniture_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((this.DataContext as FurnitureVM).Selected == null)
            {
                See_Button.IsEnabled = false;
                Delete_Button.IsEnabled = false;
            }
            else
            {
                See_Button.IsEnabled = true;
                Delete_Button.IsEnabled = true;
            }
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
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
    }
}
