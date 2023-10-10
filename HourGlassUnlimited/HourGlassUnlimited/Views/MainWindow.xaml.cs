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
using System.Windows.Shapes;
using HourGlassUnlimited.Tools;

namespace HourGlassUnlimited.Views
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Holder_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if ((Holder.Content as Page).Title.Contains("Sign"))
            {
                if (NavBar.Visibility == Visibility.Visible)
                    NavBar.Visibility = Visibility.Hidden;
            }
            else
            {
                if (NavBar.Visibility == Visibility.Hidden)
                    NavBar.Visibility = Visibility.Visible;
            }
        }

        private void GameList_Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Rankings_Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Account_Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void SignOff_Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
