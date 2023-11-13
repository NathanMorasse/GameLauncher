using HourGlassUnlimited.DataAccessLayer.Factories.Base;
using HourGlassUnlimited.Models;
using HourGlassUnlimited.Games.Sudoku.Tools;
using HourGlassUnlimited.Tools;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using HourGlassUnlimited.DataAccessLayer.Factories.Helper;

namespace HourGlassUnlimited.DataAccessLayer.Factories
{
    public class ScoreFactory : FactoryBase
    {
        public async void SaveScore(Score newScore)
        {
            MySqlConnection? connection = null;
            try
            {
                connection = new MySqlConnection(ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO scores(User, Game, Category, Time, Date) VALUES(@User, @Game, @Category, @Time, @Date);";
                command.Parameters.AddWithValue("@User", newScore.UserId);
                command.Parameters.AddWithValue("@Game", newScore.GameId);
                command.Parameters.AddWithValue("@Category", newScore.Category);
                command.Parameters.AddWithValue("@Time", newScore.Time);
                command.Parameters.AddWithValue("@Date", newScore.Date);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Echec de l'enregistrement: " + e.Message);
            }
        }

        public List<Score> ByUserAndGame(int user, string game)
        {
            List<Score>? scores = new List<Score>();
            MySqlConnection? connection = null;
            MySqlDataReader? reader = null;

            try
            {
                connection = new MySqlConnection(ConnectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = FactoryHelper.GetScoreByUserAndGameCMD;
                command.Parameters.AddWithValue("@User", user);
                command.Parameters.AddWithValue("@Game", game);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var score = FactoryHelper.ScoreFromReader(reader);
                    scores.Add(score);
                }

                if (scores.Count == 0)
                {
                    var score = new Score(0, 0, 0, "NotFound", "Aucun score n'a été trouvé.", TimeSpan.Zero, 0, DateTime.Now);
                    scores.Add(score);
                }
            }
            catch (Exception e)
            {
                var score = new Score(-1, -1, -1, "Error", e.Message, TimeSpan.Zero, -1, DateTime.Now);
                scores.Add(score);
            }

            return scores;
        }

        //public bool UserCompletedDaily()
        //{
        //    MySqlConnection? connection = null;
        //    MySqlDataReader? reader = null;

        //    try
        //    {
        //        connection = new MySqlConnection(ConnectionString);
        //        connection.Open();

        //        MySqlCommand command = connection.CreateCommand();
        //        command.CommandText = "SELECT * FROM dbdev.scores Where User=@User AND Game=@Game AND DATE(Date)=@Date AND Category='Daily';";
        //        command.Parameters.AddWithValue("@User", ConnectionHelper.User.Id);
        //        command.Parameters.AddWithValue("@Game", );
        //        command.Parameters.AddWithValue("@Date", DateTime.Now.Date);

        //        reader = command.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            var score = FactoryHelper.ScoreFromReader(reader);
        //            scores.Add(score);
        //        }

        //        if (scores.Count == 0)
        //        {
        //            var score = new Score(0, 0, 0, "NotFound", "Aucun score n'a été trouvé.", TimeSpan.Zero, 0, DateTime.Now);
        //            scores.Add(score);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        var score = new Score(-1, -1, -1, "Error", e.Message, TimeSpan.Zero, -1, DateTime.Now);
        //        scores.Add(score);
        //    }

        //    return scores;
        //}
    }
}
