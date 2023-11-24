﻿using DatabaseManager.DataAccessLayer.Factories.Helpers;
using DatabaseManager.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.DataAccessLayer.Factories
{
    public class RoomFactory
    {
        public List<Room> All()
        {
            List<Room> rooms = new List<Room>();
            MySqlConnection? connection = null;
            MySqlDataReader? reader = null;

            try
            {
                connection = new MySqlConnection(DAL.ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = Commands.AllRooms;

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    rooms.Add(CreateFromReader.Room(reader));
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

            return rooms;
        }

        public List<Room> ByDepartment(int department)
        {
            List<Room> rooms = new List<Room>();
            MySqlConnection? connection = null;
            MySqlDataReader? reader = null;

            try
            {
                connection = new MySqlConnection(DAL.ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = Commands.AllRoomsByDepartment;
                command.Parameters.AddWithValue("@Department", department);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    rooms.Add(CreateFromReader.Room(reader));
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

            return rooms;
        }

        public void Create(Room item)
        {
            MySqlConnection? connection = null;

            try
            {
                connection = new MySqlConnection(DAL.ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = Commands.CreateRoom;
                command.Parameters.AddWithValue("@Department", item.Department_Id);
                command.Parameters.AddWithValue("@Number", item.Number);
                command.Parameters.AddWithValue("@AC", item.HasAirConditioning);
                command.Parameters.AddWithValue("@Heaters", item.HasHeaters);
                command.Parameters.AddWithValue("@Phone", item.HasPhone);
                command.Parameters.AddWithValue("@Sensor", item.HasMovementSensor);

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

        public void Update(Room item)
        {
            MySqlConnection? connection = null;

            try
            {
                connection = new MySqlConnection(DAL.ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = Commands.UpdateRoom;
                command.Parameters.AddWithValue("@Department", item.Department_Id);
                command.Parameters.AddWithValue("@Number", item.Number);
                command.Parameters.AddWithValue("@AC", item.HasAirConditioning);
                command.Parameters.AddWithValue("@Heaters", item.HasHeaters);
                command.Parameters.AddWithValue("@Phone", item.HasPhone);
                command.Parameters.AddWithValue("@Sensor", item.HasMovementSensor);
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
                command.CommandText = Commands.DeleteRoom;
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
