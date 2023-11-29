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
using DatabaseManager.ViewModels;

namespace DatabaseManager.Views
{
    /// <summary>
    /// Logique d'interaction pour RoomDetail.xaml
    /// </summary>
    public partial class RoomDetail : Page
    {
        public RoomDetail()
        {
            InitializeComponent();
            this.DataContext = new RoomDetailVM();
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            Number_TextBlock.Visibility = Visibility.Hidden;
            Department_TextBlock.Visibility = Visibility.Hidden;
            AC_TextBlock.Visibility = Visibility.Hidden;
            Heater_TextBlock.Visibility = Visibility.Hidden;
            Phone_TextBlock.Visibility = Visibility.Hidden;
            Sensor_TextBlock.Visibility = Visibility.Hidden;

            Number_TextBox.Visibility = Visibility.Visible;
            Department_ComboBox.Visibility = Visibility.Visible;
            AC_CheckBox.Visibility = Visibility.Visible;
            Heater_CheckBox.Visibility = Visibility.Visible;
            Phone_CheckBox.Visibility = Visibility.Visible;
            Sensor_CheckBox.Visibility = Visibility.Visible;

            Edit_Button.Visibility = Visibility.Hidden;
            Save_Button.Visibility = Visibility.Visible;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            Number_TextBox.Visibility = Visibility.Hidden;
            Department_ComboBox.Visibility = Visibility.Hidden;
            AC_CheckBox.Visibility = Visibility.Hidden;
            Heater_CheckBox.Visibility = Visibility.Hidden;
            Phone_CheckBox.Visibility = Visibility.Hidden;
            Sensor_CheckBox.Visibility = Visibility.Hidden;

            Number_TextBlock.Visibility = Visibility.Visible;
            Department_TextBlock.Visibility = Visibility.Visible;
            AC_TextBlock.Visibility = Visibility.Visible;
            Heater_TextBlock.Visibility = Visibility.Visible;
            Phone_TextBlock.Visibility = Visibility.Visible;
            Sensor_TextBlock.Visibility = Visibility.Visible;

            Save_Button.Visibility = Visibility.Hidden;
            Edit_Button.Visibility = Visibility.Visible;
        }
    }
}
