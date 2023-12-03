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
    /// Logique d'interaction pour FurnitureDetail.xaml
    /// </summary>
    public partial class FurnitureDetail : Page
    {
        public FurnitureDetail()
        {
            InitializeComponent();
            this.DataContext = new FurnitureDetailVM();
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            Brand_TextBlock.Visibility = Visibility.Hidden;
            Type_TextBlock.Visibility = Visibility.Hidden;
            Description_TextBlock.Visibility = Visibility.Hidden;
            Length_TextBlock.Visibility = Visibility.Hidden;
            Height_TextBlock.Visibility = Visibility.Hidden;
            Width_TextBlock.Visibility = Visibility.Hidden;
            Room_TextBlock.Visibility = Visibility.Hidden;

            Brand_TextBox.Visibility = Visibility.Visible;
            Type_TextBox.Visibility= Visibility.Visible;
            Description_TextBox.Visibility= Visibility.Visible;
            Length_TextBox.Visibility= Visibility.Visible;
            Height_TextBox.Visibility= Visibility.Visible;
            Width_TextBox.Visibility= Visibility.Visible;
            Room_ComboBox.Visibility= Visibility.Visible;

            Edit_Button.Visibility= Visibility.Hidden;
            Edit_Stuff.Visibility = Visibility.Visible;
        }

        private void Edit_Stuff_Button_Click(object sender, RoutedEventArgs e)
        {
            Brand_TextBox.Visibility = Visibility.Hidden;
            Type_TextBox.Visibility = Visibility.Hidden;
            Description_TextBox.Visibility = Visibility.Hidden;
            Length_TextBox.Visibility = Visibility.Hidden;
            Height_TextBox.Visibility = Visibility.Hidden;
            Width_TextBox.Visibility = Visibility.Hidden;
            Room_ComboBox.Visibility = Visibility.Hidden;

            Brand_TextBlock.Visibility = Visibility.Visible;
            Type_TextBlock.Visibility = Visibility.Visible;
            Description_TextBlock.Visibility = Visibility.Visible;
            Length_TextBlock.Visibility = Visibility.Visible;
            Height_TextBlock.Visibility = Visibility.Visible;
            Width_TextBlock.Visibility = Visibility.Visible;
            Room_TextBlock.Visibility = Visibility.Visible;

            Edit_Stuff.Visibility = Visibility.Hidden;
            Edit_Button.Visibility = Visibility.Visible;
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Confirm_Delete.Visibility != Visibility.Visible)
            {
                Confirm_Delete.Visibility = Visibility.Visible;
            }
            else
            {
                Confirm_Delete.Visibility = Visibility.Collapsed;
            }
        }
    }
}
