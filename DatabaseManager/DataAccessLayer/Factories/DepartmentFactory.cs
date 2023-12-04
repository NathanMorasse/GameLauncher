using DatabaseManager.DataAccessLayer.Factories.Helpers;
using DatabaseManager.Models;
using DatabaseManager.ViewModels;
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
                command.CommandText = Commands.AllDepartments;

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

        public Department ByName(string name)
        {
            Department department = new Department();
            MySqlConnection? connection = null;
            MySqlDataReader? reader = null;

            try
            {
                connection = new MySqlConnection(DAL.ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = Commands.DepartmentByName;
                command.Parameters.AddWithValue("@Name", name);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    department = CreateFromReader.Department(reader);
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

            return department;
        }

        public Department ById(int id)
        {
            Department department = new Department();
            MySqlConnection? connection = null;
            MySqlDataReader? reader = null;

            try
            {
                connection = new MySqlConnection(DAL.ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = Commands.DepartmentById;
                command.Parameters.AddWithValue("@Id", id);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    department = CreateFromReader.Department(reader);
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

            return department;
        }

        public void Create(Department item)
        {
            MySqlConnection? connection = null;

            try
            {
                connection = new MySqlConnection(DAL.ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = Commands.CreateDepartment;
                command.Parameters.AddWithValue("@Name", item.Name);
                command.Parameters.AddWithValue("@Building", item.Building.ToUpper());
                command.Parameters.AddWithValue("@Floor", item.Floor);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection?.Close();
            }
        }

        public void Update(Department item)
        {
            MySqlConnection? connection = null;

            try
            {
                connection = new MySqlConnection(DAL.ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = Commands.UpdateDepartment;
                command.Parameters.AddWithValue("@Name", item.Name);
                command.Parameters.AddWithValue("@Building", item.Building.ToUpper());
                command.Parameters.AddWithValue("@Floor", item.Floor);
                command.Parameters.AddWithValue("@Id", item.Id);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection?.Close();
            }
        }

        public void Delete(int id) 
        {
            MySqlConnection? connection = null;

            try
            {
                connection = new MySqlConnection(DAL.ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = Commands.DeleteDepartment;
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection?.Close();
            }
        }
    }
}
