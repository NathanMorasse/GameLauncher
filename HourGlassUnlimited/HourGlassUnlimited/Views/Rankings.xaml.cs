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
using HourGlassUnlimited.ViewModels;

namespace HourGlassUnlimited.Views
{
    /// <summary>
    /// Logique d'interaction pour Rankings.xaml
    /// </summary>
    public partial class Rankings : Page
    {
        public Rankings()
        {
            InitializeComponent();
            this.DataContext = new RankingsVM();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EnableCategories(Categories);
            Button button = (Button)sender;
            button.IsEnabled = false;
        }

        private void EnableCategories(DependencyObject parent)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is Button button)
                {
                    button.IsEnabled = true;
                }
                else
                {
                    EnableCategories(child);
                }
            }
        }
    }
}
