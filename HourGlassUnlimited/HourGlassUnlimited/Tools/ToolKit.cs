using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.Tools
{
    public static class ToolKit
    {
        public static string GetDefaultPath()
        {
            DirectoryInfo info;
            string path = null;
            string name = null;

            info = Directory.GetParent(Directory.GetCurrentDirectory());

            path = info.ToString();

            name = info.Name;

            while (name != "HourGlassUnlimited")
            {
                info = Directory.GetParent(path);

                path = info.ToString();

                name = info.Name;
            }

            return path;
        }
    }
}
