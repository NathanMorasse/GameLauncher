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
using HourGlassUnlimited.ViewModels;

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
            DataContext = new MainWindowVM();
        }

        private void Holder_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if ((Holder.Content as Page).Title.Contains("Sign"))
            {
                if (Header.Visibility == Visibility.Visible)
                    Header.Visibility = Visibility.Hidden;
            }
            else
            {
                if (Header.Visibility == Visibility.Hidden)
                    Header.Visibility = Visibility.Visible;

                CurrentNav();
            }
        }

        private void CurrentNav()
        {
            string current = (Holder.Content as Page).Title.ToString();

            switch (current)
            {
                case "GameList":
                    {
                        GameList_Text.TextDecorations = TextDecorations.Underline;
                        GameList_Text.Foreground = Brushes.White;
                        Rankings_Text.TextDecorations = null;
                        Rankings_Text.Foreground = Brushes.LightGray;
                        Account_Text.TextDecorations = null;
                        Account_Text.Foreground = Brushes.LightGray;
                        break;
                    }
                case "Rankings":
                    {
                        GameList_Text.TextDecorations = null;
                        GameList_Text.Foreground = Brushes.LightGray;
                        Rankings_Text.TextDecorations = TextDecorations.Underline;
                        Rankings_Text.Foreground = Brushes.White;
                        Account_Text.TextDecorations = null;
                        Account_Text.Foreground = Brushes.LightGray;
                        break;
                    }
                case "EditAccount":
                    {
                        GameList_Text.TextDecorations = null;
                        GameList_Text.Foreground = Brushes.LightGray;
                        Rankings_Text.TextDecorations = null;
                        Rankings_Text.Foreground = Brushes.LightGray;
                        Account_Text.TextDecorations = TextDecorations.Underline;
                        Account_Text.Foreground = Brushes.White;
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
