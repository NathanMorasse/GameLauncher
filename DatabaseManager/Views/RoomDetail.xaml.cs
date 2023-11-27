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
    }
}
