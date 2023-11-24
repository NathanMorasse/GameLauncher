using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.DataAccessLayer.Factories.Helpers
{
    public static class Commands
    {
        public static string GetAllDepartments
        {
            get
            {
                return "select * from `department`;";
            }
        }
        public static string CreateDepartment
        {
            get
            {
                return "insert into `department` (`Name`, `Building`, `Floor`) values (@Name, @Building, @Floor);";
            }
        }

        public static string UpdateDepartment
        {
            get
            {
                return "update `department` set `Name` = @Name, `Building` = @Building, `Floor` = @Floor where `Id` = @Id;";
            }
        }

        public static string DeleteDepartment
        {
            get
            {
                return "delete from `department` where `Id` = @Id;";
            }
        }

    }
}
