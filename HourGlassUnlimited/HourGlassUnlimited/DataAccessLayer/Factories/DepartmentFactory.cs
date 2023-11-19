using HourGlassUnlimited.DataAccessLayer.Factories.Base;
using HourGlassUnlimited.DataAccessLayer.Factories.Helper;
using HourGlassUnlimited.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.DataAccessLayer.Factories
{
    public class DepartmentFactory : FactoryBase
    {
        public List<Department> All()
        {
            List<Department>? departments = null;
            MySqlConnection? connection = null;
            MySqlDataReader? reader = null;

            try
            {
                departments = new List<Department>();
                connection = new MySqlConnection(ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = FactoryHelper.AllDepartmentCMD;

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    departments.Add(FactoryHelper.DepartmentFromReader(reader));
                }
            }
            catch (Exception) { }
            finally
            {
                connection?.Close();
            }
            return departments;
        }

        public Department GetByUserId(int userId)
        {
            Department? department = null;
            MySqlConnection? connection = null;
            MySqlDataReader? reader = null;

            try
            {
                connection = new MySqlConnection(ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = FactoryHelper.GetDepartmentByUserId;
                command.Parameters.AddWithValue("@User", userId);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    department = FactoryHelper.DepartmentFromReader(reader);
                }

                if (department == null)
                {
                    throw new Exception("Le département n'existe pas");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Chargement du département impossible: " + e.Message);
            }
            finally
            {
                connection?.Close();
            }
            return department;
        }
    }
}
