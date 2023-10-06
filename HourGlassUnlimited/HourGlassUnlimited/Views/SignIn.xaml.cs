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
using HourGlassUnlimited.Tools;

namespace HourGlassUnlimited.Views
{
    /// <summary>
    /// Logique d'interaction pour SignIn.xaml
    /// </summary>
    public partial class SignIn : Page
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void SignIn_Button_Click(object sender, RoutedEventArgs e)
        {
            Navigator.GameList();
        }

        private void SignUp_Button_Click(object sender, RoutedEventArgs e)
        {
            Navigator.SignUp();
        }
    }
}
