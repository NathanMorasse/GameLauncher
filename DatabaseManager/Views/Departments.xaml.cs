using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour Departments.xaml
    /// </summary>
    public partial class Departments : Page
    {
        public Departments()
        {
            InitializeComponent();

            // Initialize items (you can load them from a database or other source)
            //var items = new ObservableCollection<ItemViewModel>
            //{
            //    new ItemViewModel { Title = "Item 1", Description = "Description 1" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    new ItemViewModel { Title = "Item 2", Description = "Description 2" },
            //    // Add more items as needed
            //};

            //// Add items to the ListBox
            //itemListBox.ItemsSource = items;
        }

        public class ItemViewModel
        {
            //public string Title { get; set; }
            //public string Description { get; set; }
            //// Add other properties as needed

            //public ItemViewModel() { }

            //public ItemViewModel(string title, string description)
            //{
            //    Title = title;
            //    Description = description;
            //}
        }
    }
}
