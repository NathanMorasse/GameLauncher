using HourGlassUnlimited.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.DataAccessLayer.Factories.Helper
{
    public static class FactoryHelper
    {
        public static string UserByUsernameCMD
        {
            get
            {
                return "SELECT users.`Id`, users.`Username`, users.`Password`, departments.`Department` " +
                    "FROM users " +
                    "JOIN departments " +
                    "ON users.`Department` = departments.`Id` " +
                    "WHERE `Username` = @Username;";
            }
        }

        public static string AddUserCMD
        {
            get
            {
                return "insert into users (`Username`, `Password`, `Department`) " +
                    "values (@Username, @Password, (" +
                    "select `Id` " +
                    "from departments " +
                    "where `Department` = @Department));";
            }
        }

        public static string AllDepartmentCMD
        {
            get
            {
                return "select `Id`, `Department` from departments;";
            }
        }

        public static string UpdateUserCMD
        {
            get
            {
                return "update users " +
                    "set `Username`= @Username, " +
                    "`Password` = @Password, " +
                    "`Department`= (select Id from departments where Department = @Department) " +
                    "where Id = @Id;";
            }
        }

        public static User UserFromReader(MySqlDataReader reader)
        {
            int id = (int)reader["Id"];
            string username = reader["Username"].ToString() ?? string.Empty;
            string password = reader["Password"].ToString() ?? string.Empty;
            string department = reader["Department"].ToString() ?? string.Empty;

            return new User(id, username, password, department);
        }

        public static Department DepartmentFromReader(MySqlDataReader reader)
        {
            int id = (int)reader["Id"];
            string name = reader["Department"].ToString() ?? string.Empty;

            return new Department(id, name);
        }
    }
}
