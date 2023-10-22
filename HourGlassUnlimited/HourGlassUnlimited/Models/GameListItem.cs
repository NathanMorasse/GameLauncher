using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace HourGlassUnlimited.Models
{
    public class GameListItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public BitmapImage Image { get; set; }

        public GameListItem(string name, string path)
        {
            Name = name;
            Path = path;

            Image = new BitmapImage();
            Image.BeginInit();
            Image.UriSource = new Uri(Path);
            Image.EndInit();
        }
    }
}
