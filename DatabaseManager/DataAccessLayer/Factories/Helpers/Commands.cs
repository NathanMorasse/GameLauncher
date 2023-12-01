using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.DataAccessLayer.Factories.Helpers
{
    public static class Commands
    {
        //Requêtes pour les départements

        public static string AllDepartments
        {
            get
            {
                return "select * from `department`;";
            }
        }

        public static string DepartmentByName
        {
            get
            {
                return "select * from `department` where `Name` = @Name;";
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
                return "delete from `furniture` where `Room_Id` in (select `Id` from `room` where `Department_Id` = @Id); " +
                    "delete from `room` where `Department_Id` = @Id; " +
                    "delete from `department` where `Id` = @Id;";
            }
        }

        //Requêtes pour les locaux

        public static string AllRooms
        {
            get
            {
                return "select `room`.`Id`, `department`.`Name` as `Department_Id`, concat(`department`.`Building`,\"-\",`room`.`Number`) as `Number`, `room`.`HasAirConditioning`, `room`.`HasHeaters`, `room`.`HasPhone`, `room`.`HasMovementSensor` " +
                    "from `room` " +
                    "join `department` " +
                    "on `room`.`Department_Id` = `department`.`Id`;";
            }
        }

        public static string RoomById
        {
            get
            {
                return "select `room`.`Id`, `department`.`Name` as `Department_Id`, concat(`department`.`Building`,\"-\",`room`.`Number`) as `Number`, `room`.`HasAirConditioning`, `room`.`HasHeaters`, `room`.`HasPhone`, `room`.`HasMovementSensor` " +
                    "from `room` " +
                    "join `department` " +
                    "on `room`.`Department_Id` = `department`.`Id` " +
                    "where `room`.`Id` = @Id;";
            }
        }

        public static string AllRoomsByDepartment
        {
            get
            {
                return "select `room`.`Id`, `department`.`Name` as `Department_Id`, concat(`department`.`Building`,\"-\",`room`.`Number`) as `Number`, `room`.`HasAirConditioning`, `room`.`HasHeaters`, `room`.`HasPhone`, `room`.`HasMovementSensor` " +
                    "from `room` " +
                    "join `department` " +
                    "on `room`.`Department_Id` = `department`.`Id` " +
                    "where `Department_Id` = @Department;";
            }
        }

        public static string CreateRoom
        {
            get
            {
                return "insert into `room` (`Department_Id`, `Number`, `HasAirConditioning`, `HasHeaters`, `HasPhone`, `HasMovementSensor`) " +
                    "values ((select `Id` from `department` where `Name` = @Department), @Number, @AC, @Heaters, @Phone, @Sensor);";
            }
        }

        public static string UpdateRoom
        {
            get
            {
                return "update `room` " +
                    "set `Department_Id` = (select `Id` from `department` where `Name` = @Department), `Number` = @Number, `HasAirConditioning` = @AC, `HasHeaters` = @Heaters, `HasPhone` = @Phone, `HasMovementSensor` = @Sensor " +
                    "where `Id` = @Id;";
            }
        }

        public static string DeleteRoom
        {
            get
            {
                return "delete from `furniture` where `Room_Id` = @Id; " +
                    "delete from `room` where `Id` = @Id; ";
            }
        }

        //Requêtes pour le mobilier

        public static string AllFurnitures
        {
            get
            {
                return "select `Id`, `Room_Id`, `Brand`, `Type`, `Description`, `Length`, `Height`, `Width`, " +
                    "( select concat((select `Building` from `department` where `Id` = `Department_Id`),\"-\",`Number`)  from `room`  where `Id` = `Room_Id` ) as `Number` " +
                    "from `furniture`;";
            }
        }

        public static string AllFurnituresByRoom
        {
            get
            {
                return "select `Id`, `Room_Id`, `Brand`, `Type`, `Description`, `Length`, `Height`, `Width`, " +
                    "(select concat((select `Building` from `department` where `Id` = `Department_Id`),\"-\",`Number`)  from `room`  where `Id` = `Room_Id` ) as `Number` " +
                    "from `furniture` " +
                    "where `Room_Id` = @Room;";
            }
        }

        public static string FurnitureById
        {
            get
            {
                return "select `Id`, `Room_Id`, `Brand`, `Type`, `Description`, `Length`, `Height`, `Width`, " +
                    "( select concat((select `Building` from `department` where `Id` = `Department_Id`),\"-\",`Number`)  from `room`  where `Id` = `Room_Id` ) as `Number` " +
                    "from `furniture` " +
                    "where `Id` = @Id;";
            }
        }

        public static string UpdateFurniture
        {
            get
            {
                return "update `furniture` " +
                    "set `Room_Id` =  @Room, `Brand` =  @Brand, `Type` =  @Type, `Description` =  @Description, `Length` =  @Length, `Height` =  @Height, `Width` =  @Width " +
                    "where `Id` = 3;";
            }
        }

        public static string CreateFurniture
        {
            get
            {
                return "insert into `furniture` (`Room_Id`, `Brand`, `Type`, `Description`, `Length`, `Height`, `Width`) " +
                    "values (@Room, @Brand, @Type, @Description, @Length, @Height, @Width);";
            }
        }

        public static string DeleteFurniture
        {
            get
            {
                return "delete from `furniture` where `Id` = @Id;";
            }
        }
    }
}
