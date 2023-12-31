﻿using HourGlassUnlimited.DataAccessLayer.Factories.Base;
using HourGlassUnlimited.DataAccessLayer.Factories.Helper;
using HourGlassUnlimited.Models;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.DataAccessLayer.Factories
{
    public class UserFactory : FactoryBase
    {
        public User ByUsername(string username)
        {
            User? user = null;
            MySqlConnection? connection = null;
            MySqlDataReader? reader = null;

            try
            {
                connection = new MySqlConnection(ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = FactoryHelper.UserByUsernameCMD;
                command.Parameters.AddWithValue("@Username", username);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user = FactoryHelper.UserFromReader(reader);
                }

                if (user == null)
                {
                    user = new User(0, "NotFound", "", "L'utilisateur n'existe pas.");
                }
            }
            catch (Exception e)
            {
                user = new User(-1, "Error", "", e.Message);
            }
            finally
            {
                connection?.Close();
            }
            return user;
        }

        public string[] Add(User user)
        {
            MySqlConnection? connection = null;
            MySqlDataReader? reader = null;

            try
            {
                User unknown = ByUsername(user.Username);

                if (unknown.Id > 0)
                {
                    return new string[] {"Unauthorized", "Le nom d'utilisateur est déjà utilisé." };
                }

                if (unknown.Id < 0)
                {
                    return new string[] { unknown.Username, unknown.Department };
                }

                connection = new MySqlConnection(ConnectionString);
                connection.Open();


                MySqlCommand command = connection.CreateCommand();
                command.CommandText = FactoryHelper.AddUserCMD;
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Department", user.Department);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return new string[] { "Error", e.Message };
            }
            finally
            {
                connection?.Close();
            }
            return new string[] { "Success", "Le compte a été créé avec succès." };
        }

        public string[] Update(User user)
        {
            MySqlConnection? connection = null;
            MySqlDataReader? reader = null;

            try
            {
                User unknown = ByUsername(user.Username);

                if (unknown.Id > 0 && unknown.Id != user.Id)
                {
                    return new string[] { "Unauthorized", "Le nom d'utilisateur est déjà utilisé." };
                }

                if (unknown.Id < 0)
                {
                    return new string[] { unknown.Username, unknown.Department };
                }

                connection = new MySqlConnection(ConnectionString);
                connection.Open();


                MySqlCommand command = connection.CreateCommand();
                command.CommandText = FactoryHelper.UpdateUserCMD;
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Department", user.Department);
                command.Parameters.AddWithValue("@Id", user.Id);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return new string[] { "Error", e.Message };
            }

            return new string[] { "Success", "Le compte a été modifié avec succès." };

            return null;
        }
    }
}
