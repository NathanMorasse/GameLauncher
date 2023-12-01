using DatabaseManager.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.DataAccessLayer.Factories.Helpers
{
    public static class CreateFromReader
    {
        public static Department Department(MySqlDataReader reader)
        {
            int id = (int)reader["Id"];
            string name = reader["Name"].ToString() ?? string.Empty;
            string building = reader["Building"].ToString() ?? string.Empty;
            int floor = (int)reader["Floor"];

            return new Department(id, name, building, floor);
        }

        public static Room Room(MySqlDataReader reader)
        {
            int id = (int)reader["Id"];
            string department = reader["Department_Id"].ToString() ?? string.Empty;

            string number = reader["Number"].ToString() ?? string.Empty;
            bool ac = (bool)reader["HasAirConditioning"];
            bool heater = (bool)reader["HasHeaters"];
            bool phone = (bool)reader["HasPhone"];
            bool sensor = (bool)reader["HasMovementSensor"];

            return new Room(id, department, number, ac, heater, phone, sensor);
        }

        public static Period Period(MySqlDataReader reader)
        {
            int id = (int)reader["Id"];
            int room = (int)reader["Room_Id"];

            TimeSpan start = (TimeSpan)reader["Start"];
            TimeSpan end = (TimeSpan)reader["End"];
            string day = reader["Day"].ToString() ?? string.Empty;
            string teacher = reader["TeacherName"].ToString() ?? string.Empty;
            string className = reader["Class"].ToString() ?? string.Empty;
            string group = reader["Group"].ToString() ?? string.Empty;

            return new Models.Period(id, room, start, end, day, teacher, className, group);
        }

        public static Furniture Furniture(MySqlDataReader reader)
        {
            int id = (int)reader["Id"];
            int room = (int)reader["Room_Id"];

            string brand = reader["Brand"].ToString() ?? string.Empty;
            string type = reader["Type"].ToString() ?? string.Empty;
            string description = reader["Description"].ToString() ?? string.Empty;
            double l = (double)reader["Length"];
            double h = (double)reader["Height"];
            double w = (double)reader["Width"];
            string number = reader["Number"].ToString() ?? string.Empty;

            return new Furniture(id, room, brand, type, description, l, h, w, number);
        }

        public static ElectricOutlet ElectricOutlet(MySqlDataReader reader)
        {
            int id = (int)reader["Id"];
            int room = (int)reader["Room_Id"];

            string location = reader["Location"].ToString() ?? string.Empty;

            return new ElectricOutlet(id, room, location);
        }

        public static NetworkEquipment NetworkEquipment(MySqlDataReader reader)
        {
            int id = (int)reader["Id"];
            int room = (int)reader["Room_Id"];

            string brand = reader["Brand"].ToString() ?? string.Empty;
            string type = reader["Type"].ToString() ?? string.Empty;
            string model = reader["Model"].ToString() ?? string.Empty;
            string spec = reader["Specification"].ToString() ?? string.Empty;

            return new NetworkEquipment(id, room, type, brand, model, spec);
        }

        public static Tool Tool(MySqlDataReader reader)
        {
            int id = (int)reader["Id"];
            int board = (int)reader["Board_Id"];

            string brand = reader["Brand"].ToString() ?? string.Empty;
            string type = reader["Type"].ToString() ?? string.Empty;

            return new Tool(id, board, brand, type);
        }

        public static Board Board(MySqlDataReader reader)
        {
            int id = (int)reader["Id"];
            int room = (int)reader["Room_Id"];

            string brand = reader["Brand"].ToString() ?? string.Empty;
            string type = reader["Type"].ToString() ?? string.Empty;
            double h = (double)reader["Height"];
            double w = (double)reader["Width"];

            return new Board(id, room, type, brand, h, w);
        }

        public static Light Light(MySqlDataReader reader)
        {
            int id = (int)reader["Id"];
            int room = (int)reader["Room_Id"];
            int lightswitch = (int)reader["LightSwitch_Id"];

            string brand = reader["Brand"].ToString() ?? string.Empty;
            string type = reader["Type"].ToString() ?? string.Empty;


            return new Light(id, room, lightswitch, brand, type);
        }

        public static LightSwitch LightSwitch(MySqlDataReader reader)
        {
            int id = (int)reader["Id"];
            int room = (int)reader["Room_Id"];

            string location = reader["Location"].ToString() ?? string.Empty;

            return new LightSwitch(id, room, location);
        }

        public static Computer Computer(MySqlDataReader reader)
        {
            int id = (int)reader["Id"];
            int room = (int)reader["Room_Id"];

            string brand = reader["Brand"].ToString() ?? string.Empty;
            string model = reader["Model"].ToString() ?? string.Empty;
            string os = reader["OperatingSystem"].ToString() ?? string.Empty;

            return new Computer(id, room, brand, model, os);
        }

        public static Display Display(MySqlDataReader reader)
        {
            int id = (int)reader["Id"];
            int room = (int)reader["Room_Id"];
            int computer = (int)reader["Computer_Id"];

            string brand = reader["Brand"].ToString() ?? string.Empty;
            string type = reader["Type"].ToString() ?? string.Empty;
            string model = reader["Model"].ToString() ?? string.Empty;
            string resolution = reader["Resolution"].ToString() ?? string.Empty;

            return new Display(id, room, computer, type, brand, model, resolution);
        }

        public static Peripheral Peripheral(MySqlDataReader reader)
        {
            int id = (int)reader["Id"];
            int room = (int)reader["Room_Id"];
            int computer = (int)reader["Computer_Id"];

            string brand = reader["Brand"].ToString() ?? string.Empty;
            string type = reader["Type"].ToString() ?? string.Empty;
            string model = reader["Model"].ToString() ?? string.Empty;

            bool wifi = (bool)reader["IsWiFi"];
            bool bluetooth = (bool)reader["IsBluetooth"];
            string description = reader["Description"].ToString() ?? string.Empty;

            return new Peripheral(id, room, computer, type, brand, model, wifi, bluetooth, description);
        }

        //public static Department Department(MySqlDataReader reader)
        //{
        //    int id = (int)reader["Id"];
        //    string name = reader["Name"].ToString() ?? string.Empty;
        //    string building = reader["Building"].ToString() ?? string.Empty;
        //    int floor = (int)reader["floor"];

        //    return new Department(id, name, building, floor);
        //}

        //public static Department Department(MySqlDataReader reader)
        //{
        //    int id = (int)reader["Id"];
        //    string name = reader["Name"].ToString() ?? string.Empty;
        //    string building = reader["Building"].ToString() ?? string.Empty;
        //    int floor = (int)reader["floor"];

        //    return new Department(id, name, building, floor);
        //}

        //public static Department Department(MySqlDataReader reader)
        //{
        //    int id = (int)reader["Id"];
        //    string name = reader["Name"].ToString() ?? string.Empty;
        //    string building = reader["Building"].ToString() ?? string.Empty;
        //    int floor = (int)reader["floor"];

        //    return new Department(id, name, building, floor);
        //}

        //public static Department Department(MySqlDataReader reader)
        //{
        //    int id = (int)reader["Id"];
        //    string name = reader["Name"].ToString() ?? string.Empty;
        //    string building = reader["Building"].ToString() ?? string.Empty;
        //    int floor = (int)reader["floor"];

        //    return new Department(id, name, building, floor);
        //}

        //public static Department Department(MySqlDataReader reader)
        //{
        //    int id = (int)reader["Id"];
        //    string name = reader["Name"].ToString() ?? string.Empty;
        //    string building = reader["Building"].ToString() ?? string.Empty;
        //    int floor = (int)reader["floor"];

        //    return new Department(id, name, building, floor);
        //}

        //public static Department Department(MySqlDataReader reader)
        //{
        //    int id = (int)reader["Id"];
        //    string name = reader["Name"].ToString() ?? string.Empty;
        //    string building = reader["Building"].ToString() ?? string.Empty;
        //    int floor = (int)reader["floor"];

        //    return new Department(id, name, building, floor);
        //}

        //public static Department Department(MySqlDataReader reader)
        //{
        //    int id = (int)reader["Id"];
        //    string name = reader["Name"].ToString() ?? string.Empty;
        //    string building = reader["Building"].ToString() ?? string.Empty;
        //    int floor = (int)reader["floor"];

        //    return new Department(id, name, building, floor);
        //}

        //public static Department Department(MySqlDataReader reader)
        //{
        //    int id = (int)reader["Id"];
        //    string name = reader["Name"].ToString() ?? string.Empty;
        //    string building = reader["Building"].ToString() ?? string.Empty;
        //    int floor = (int)reader["floor"];

        //    return new Department(id, name, building, floor);
        //}

        //public static Department Department(MySqlDataReader reader)
        //{
        //    int id = (int)reader["Id"];
        //    string name = reader["Name"].ToString() ?? string.Empty;
        //    string building = reader["Building"].ToString() ?? string.Empty;
        //    int floor = (int)reader["floor"];

        //    return new Department(id, name, building, floor);
        //}

        //public static Department Department(MySqlDataReader reader)
        //{
        //    int id = (int)reader["Id"];
        //    string name = reader["Name"].ToString() ?? string.Empty;
        //    string building = reader["Building"].ToString() ?? string.Empty;
        //    int floor = (int)reader["floor"];

        //    return new Department(id, name, building, floor);
        //}
    }
}
