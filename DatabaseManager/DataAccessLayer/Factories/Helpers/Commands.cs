using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.DataAccessLayer.Factories.Helpers
{
    public static class Commands
    {
        public static string AllDepartments
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

        public static string AllRooms
        {
            get
            {
                return "select * from `room`;";
            }
        }

        public static string AllRoomsByDepartment
        {
            get
            {
                return "select * from `room` where `Department_Id` = @Department;";
            }
        }

        public static string CreateRoom
        {
            get
            {
                return "insert into `room` (`Department_Id`, `Number`, `HasAirConditioning`, `HasHeaters`, `HasPhone`, `HasMovementSensor`) " +
                    "values (@Department, @Number, @AC, @Heaters, @Phone, @Sensor);";
            }
        }

        public static string UpdateRoom
        {
            get
            {
                return "update `room` " +
                    "set `Department_Id` = @Department, `Number` = @Number, `HasAirConditioning` = @AC, `HasHeaters` = @Heaters, `HasPhone` = @Phone, `HasMovementSensor` = @Sensor " +
                    "where `Id` = @Id;";
            }
        }

        public static string DeleteRoom
        {
            get
            {
                return "delete from `room` where `Id` = @Id;";
            }
        }

    }
}
