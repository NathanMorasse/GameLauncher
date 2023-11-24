using DatabaseManager.Tools;
using DatabaseManager.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DatabaseManager.ViewModels
{
    public class MainWindowVM : ViewModelTemplate
    {
        public ICommand DepartmentList {  get; set; }

        public MainWindowVM() 
        {
            this.DepartmentList = new CommandLink(DepartmentList_Execute, Dummy_CanExecute);
        }

        private void DepartmentList_Execute(object parameter)
        {
            Navigator.DepartmentList();
        }
    }
}
