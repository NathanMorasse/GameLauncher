using DatabaseManager.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.ViewModels
{
    public class DepartmentRoomListVM : ViewModelTemplate
    {
        public static int Department_Id {  get; set; }
        public DepartmentVM Department { get; set; }

        public DepartmentRoomListVM()
        {

        }
    }
}
