using HourGlassUnlimited.DataAccessLayer.Factories.Base;
using HourGlassUnlimited.Models;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.DataAccessLayer.Factories
{
    public class UserFactory : FactoryBase
    {
        //public User ProduireBulletinEtudiant(string codePermanent)
        //{
        //    List<BulletinItem> listCours = new List<BulletinItem>();
        //    MySqlConnection? mySqlCnn = null;
        //    MySqlDataReader? mySqlDataReader = null;

        //    try
        //    {
        //        mySqlCnn = new MySqlConnection(DAL.ConnectionString);
        //        mySqlCnn.Open();

        //        MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
        //        mySqlCmd.CommandText = ProduireBulletinEtudiantText;
        //        mySqlCmd.Parameters.AddWithValue("@CodePermanent", codePermanent);

        //        mySqlDataReader = mySqlCmd.ExecuteReader();
        //        while (mySqlDataReader.Read())
        //        {
        //            BulletinItem cours = CreateFromReader(mySqlDataReader);
        //            listCours.Add(cours);
        //        }
        //    }
        //    finally
        //    {
        //        mySqlDataReader?.Close();
        //        mySqlCnn?.Close();
        //    }

        //    return listCours;
        //}

        public User ByUsername(string username)
        {
            User? user = null;
            MySqlConnection? connection = null;
            return null;
        }
    }
}
