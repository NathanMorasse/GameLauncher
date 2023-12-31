﻿using HourGlassUnlimited.DataAccessLayer;
using HourGlassUnlimited.DataAccessLayer.Factories.Helper;
using HourGlassUnlimited.Models;
using HourGlassUnlimited.ViewModels;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.Tools
{
    public static class ConnectionHelper
    {
        public static User User { get; set; }

        public static string SignIn(string username, string password)
        {
            DAL dal = new DAL();
            User user = dal.Users.ByUsername(username);

            if (user == null) { return "Unavalable"; }

            if (user.Id == 0) { return "Wrong"; }

            if (user.Id < 0) { return "Error"; }

            if (!ValidateHashedPassword(password, user.Password)) { return "Unauthorized"; }

            User = user;

            return "Success";
        }

        public static string[] SignUp(User user)
        {
            DAL dal = new DAL();
            if (user == null)
            {
                return new string[] { "NotFound", "Aucune information n'a été fourni." };
            }

            //Verify if the Username is already taken
            if (user.Username == dal.Users.ByUsername(user.Username).Username)
            {
                return new string[] { "Unauthorized", "Le nom d'utilisateur est déjà utilisé." };
            }


            user.Password =  HashPassword(user.Password);
            //Create user with the DAL.

            return dal.Users.Add(user);
        }

        public static string[] UpdateAccount()
        {
            if (User == null)
            {
                return new string[] { "NotFouund", "Il y a un problème avec l'utilisateur actuel." };
            }

            return null;
        }

        public static string HashPassword(string passwordToHash)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            var pdkdf2 = new Rfc2898DeriveBytes(passwordToHash, salt, 4855);
            byte[] hash = pdkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        public static bool ValidateHashedPassword(string password, string hashedPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);

            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 4855);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
