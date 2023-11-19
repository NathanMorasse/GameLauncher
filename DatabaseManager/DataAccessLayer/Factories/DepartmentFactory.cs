using DatabaseManager.DataAccessLayer.Factories.Helpers;
using DatabaseManager.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.DataAccessLayer.Factories
{
    public class DepartmentFactory
    {
        public List<Department> All()
        {
            List<Department> departments = new List<Department>();
            MySqlConnection? connection = null;
            MySqlDataReader? reader = null;

            try
            {
                connection = new MySqlConnection(DAL.ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = Commands.GetAllDepartments;

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    departments.Add(CreateFromReader.Department(reader));
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection?.Close();
            }

            return departments;
        }
    }
}
