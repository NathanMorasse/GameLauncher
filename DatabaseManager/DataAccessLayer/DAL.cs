using DatabaseManager.DataAccessLayer.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.DataAccessLayer
{
    public static class DAL
    {
        public static readonly string ConnectionString = "Server=sql.decinfo-cchic.ca;Port=33306;Database=a23_e80_demandeclient_2133752;Uid=dev-2133752;Pwd=Mateo235";


        private static DepartmentFactory? _DepartmentFactory;


        public static DepartmentFactory? Departments
        {
            get
            {
                if (_DepartmentFactory == null)
                {
                    _DepartmentFactory = new DepartmentFactory();
                }

                return _DepartmentFactory;
            }
        }
    }
}
