using HourGlassUnlimited.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.ViewModels
{
    public class VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public virtual void ChangeValue(string property)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public Navigator Nav;

        protected VM()
        {
              Nav = Navigator.Instance;
        }
    }
}
