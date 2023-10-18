using HourGlassUnlimited.Models;
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
        public static string SignIn(string username, string password)
        {
            //Get the user with DAL via Username;

            //Temp user
            User user = new User();

            if (user == null)
            {
                return "Unavalable";
            }

            if (!ValidateHashedPassword(password, user.Password))
            {
                return "Refused";
            }

            return "Success";
        }

        public static string SignUp(User user)
        {
            if (user == null)
            {

            }

            //Verify if the Username is already taken

            //Create user with the DAL.

            return "Success";
        }

        private static string HashPassword(string passwordToHash)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            var pdkdf2 = new Rfc2898DeriveBytes(passwordToHash, salt, 4855);
            byte[] hash = pdkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        private static bool ValidateHashedPassword(string password, string hashedPassword)
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
