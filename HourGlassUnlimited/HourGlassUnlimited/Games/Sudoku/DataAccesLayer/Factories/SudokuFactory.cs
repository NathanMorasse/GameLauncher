using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HourGlassUnlimited.Games.Sudoku.DataAccesLayer.Factories.Base;
using HourGlassUnlimited.Games.Sudoku.Models;
using Newtonsoft.Json;
using HourGlassUnlimited.Games.Sudoku.Tools;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using MySqlX.XDevAPI;
using HourGlassUnlimited.DataAccessLayer.Factories.Helper;
using HourGlassUnlimited.Models;
using MySql.Data.MySqlClient;
using HourGlassUnlimited.Tools;
using System.Reflection.PortableExecutable;
using HourGlassUnlimited.Exceptions;

namespace HourGlassUnlimited.Games.Sudoku.DataAccesLayer.Factories
{
    public class SudokuFactory : FactoryBase
    {
        private string General = "select Min(`Time`) as `Time` " +
            "from `scores` " +
            "where `Game` = (select `Id` from `games` where `Title` = \"Sudoku\");";
        private string Personnal = "select Min(`Time`) as `Time` " +
            "from `scores` " +
            "where `Game` = (select `Id` from `games` where `Title` = \"Sudoku\") " +
            "and `User` = (select `Id` from `users` where `Username` = @User);";
        private string Daily = "select Min(`Time`) as `Time` " +
            "from `scores` " +
            "where `Game` = (select `Id` from `games` where `Title` = \"Sudoku\") " +
            "and date(`Date`) = CURDATE();";

        public static SudokuGame CreateFromSave(MySqlDataReader reader)
        {
            try
            {
                int id = (int)reader["Id"];
                string title = reader["Title"].ToString() ?? string.Empty;
                string timePassed = reader["Time"].ToString() ?? string.Empty;
                string boardString = reader["Save"].ToString() ?? string.Empty;
                Board board = BoardEncoder.DecodeBoard(boardString);
                board.Seed = reader["Seed"].ToString() ?? string.Empty;
                board.Notes = reader["Notes"].ToString() ?? string.Empty;
                bool isDaily = reader["IsDaily"].ToString() == "True";
                DateTime date = DateTime.Parse(reader["Date"].ToString());
                return new SudokuGame() { Id=id, Title=title, TimePassed = timePassed, GameBoard=board, IsDaily = isDaily, Date = date};
            }
            catch (Exception e)
            {
                throw new ModelCreationFromReaderException("Échec de création d'une nouvelle partie de Sudoku à partir d'une sauvegarde", e);
            }
        }

        public static SudokuGame CreateFromReader(MySqlDataReader reader)
        {
            try
            {
                int id = (int)reader["Id"];
                string title = reader["Title"].ToString() ?? string.Empty;

                return new SudokuGame() { Id = id, Title = title};
            }
            catch (Exception e)
            {

                throw new ModelCreationFromReaderException("Échec de création d'une nouvelle partie de Sudoku", e);
            }
        }

        public async Task<Board> GenerateBoard(string difficulty, bool isDaily, string paramSeed, string notes)
        {
            if (isDaily)
            {
                paramSeed = DateTime.Now.Date.ToString();
            }
            if (paramSeed == null || paramSeed == String.Empty)
            {
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                paramSeed = rand.Next().ToString();
            }
            paramSeed = "&seed=" + paramSeed;

			try
			{
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(BaseUri+$"generate?difficulty={difficulty}"+paramSeed),
                    Headers =
                    {
                        { "X-RapidAPI-Key", "31010f78ecmshbe4226e41a8235fp15f4b5jsn6f1b2efc6143" },
                        { "X-RapidAPI-Host", "sudoku-generator1.p.rapidapi.com" },
                    },
                };
                using (var response = await APIClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    string body = await response.Content.ReadAsStringAsync();
                    dynamic data = JObject.Parse(body);
                    string boardString = data.puzzle;
                    Board board = BoardEncoder.DecodeBoard(boardString);
                    board.Seed = data.seed;
                    board.Difficulty = data.difficulty;
                    board.Notes = notes;
                    return board;
                }
            }
			catch (Exception e)
			{
				throw new Exception("Échec de génération de grille", e);
			}
        }

        public async Task<string> ValidateBoard(Board board)
        {
            string boardString = BoardEncoder.EncodeBoard(board);
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(BaseUri+$"solve?puzzle={boardString}"),
                    Headers =
                    {
                        { "X-RapidAPI-Key", "31010f78ecmshbe4226e41a8235fp15f4b5jsn6f1b2efc6143" },
                        { "X-RapidAPI-Host", "sudoku-generator1.p.rapidapi.com" },
                    },
                };
                using (var response = await APIClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    dynamic data = JObject.Parse(body);
                    string solvedBoardString = data.solution;
                    if (boardString == solvedBoardString)
                    {
                        return "valid";
                    }
                    return "invalid";
                }
            }
            catch (Exception e)
            {
                throw new BoardValidationException("Échec de la validation de grille", e);
            }
        }

        public async Task SaveGame(SudokuGame game, string timespan)
        {
            MySqlConnection? connection = null;
            string boardString = BoardEncoder.EncodeBoard(game.GameBoard);
            int binaryBool = 0;
            if (game.IsDaily)
            {
                binaryBool = 1;
            }
            try
            {
                connection = new MySqlConnection(CnnStr);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO saves(User, Game, Save, Time, Date, IsDaily, Seed, Notes) VALUES(@User, @Game, @Save, @Time, @Date, @IsDaily, @Seed, @Notes);";
                command.Parameters.AddWithValue("@User", ConnectionHelper.User.Id);
                command.Parameters.AddWithValue("@Game", game.Id);
                command.Parameters.AddWithValue("@Save", boardString);
                command.Parameters.AddWithValue("@Time", timespan);
                command.Parameters.AddWithValue("@Date", game.Date);
                command.Parameters.AddWithValue("@IsDaily", binaryBool);
                command.Parameters.AddWithValue("@Seed", game.GameBoard.Seed);
                command.Parameters.AddWithValue("@Notes", game.GameBoard.Notes);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Échec de la sauvegarde ", e);
            }
        }

        public SudokuGame LoadSave(bool isdaily)
        {
            MySqlConnection? connection = null;
            MySqlDataReader? reader = null;
            SudokuGame game = null;
            int binaryBool = 0;
            if (isdaily)
            {
                binaryBool = 1;
            }

            try
            {
                connection = new MySqlConnection(CnnStr);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT games.Id, games.Title, saves.Save, saves.Seed, saves.Time, saves.IsDaily, saves.Date, saves.Notes FROM saves INNER JOIN games ON saves.Game=games.Id WHERE Game = 1 AND User = @User AND IsDaily = @IsDaily ORDER BY Time DESC LIMIT 1;";
                command.Parameters.AddWithValue("@User", ConnectionHelper.User.Id);
                command.Parameters.AddWithValue("@IsDaily", binaryBool);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    game = CreateFromSave(reader);
                }
                return game;
            }
            catch (Exception e)
            {
                throw new Exception("Échec du chargement de sauvegarde", e);
            }
            finally
            {
                connection?.Close();
            }
        }

        public List<string> GetBestTimes()
        {
            MySqlConnection? connection = null;
            MySqlDataReader? reader = null;
            List<string>? ranking = null;

            try
            {
                ranking = new List<string>();
                connection = new MySqlConnection(CnnStr);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = General;

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string temp = reader["Time"].ToString() ?? string.Empty;
                    ranking.Add(temp);
                }
                reader.Close();

                command.CommandText = Personnal;
                command.Parameters.AddWithValue("@User", ConnectionHelper.User.Username);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string temp = reader["Time"].ToString() ?? string.Empty;
                    ranking.Add(temp);
                }
                reader.Close();

                command = connection.CreateCommand();
                command.CommandText = Daily;

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string temp = reader["Time"].ToString() ?? string.Empty;
                    ranking.Add(temp);
                }
                reader.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Imposible d'avoir les meilleurs temps ", e);
            }
            finally
            {
                connection?.Close();
            }

            return ranking;
        }
    }
}
