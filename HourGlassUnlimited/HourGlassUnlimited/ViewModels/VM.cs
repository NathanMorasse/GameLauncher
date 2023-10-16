using HourGlassUnlimited.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using System.Reflection.Metadata;

namespace HourGlassUnlimited.ViewModels
{
    public abstract class VM : INotifyPropertyChanged, ISupportParameter
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public object? Parameter { get; set; }
        public virtual void ChangeValue(string property)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public VM() { }
    }
}
