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

namespace HourGlassUnlimited.Games.Sudoku.DataAccesLayer.Factories
{
    public class SudokuFactory : FactoryBase
    {
        public static SudokuGame CreateFromSave(MySqlDataReader reader)
        {
            int id = (int)reader["Id"];
            string title = reader["Title"].ToString() ?? string.Empty;
            string timePassed = reader["Time"].ToString() ?? string.Empty;
            string boardString = reader["Save"].ToString() ?? string.Empty;
            Board board = BoardEncoder.DecodeBoard(boardString);
            board.Seed = reader["Seed"].ToString() ?? string.Empty;

            return new SudokuGame() { Id=id, Title=title, TimePassed = timePassed, GameBoard=board};
        }

        public static SudokuGame CreateFromReader(MySqlDataReader reader)
        {
            int id = (int)reader["Id"];
            string title = reader["Title"].ToString() ?? string.Empty;

            return new SudokuGame() { Id = id, Title = title};
        }

        public async Task<Board> GenerateBoard(string difficulty, bool isDaily, string paramSeed)
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
                throw new Exception("Échec de la validation de grille", e);
            }
        }

        public SudokuGame GetByTitle(string title)
        {
            SudokuGame? game = null;
            MySqlConnection? connection = null;
            MySqlDataReader? reader = null;

            try
            {
                connection = new MySqlConnection(CnnStr);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM a23_web3_2133752.games WHERE Title=@Title;";
                command.Parameters.AddWithValue("@Title", title);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    game = CreateFromReader(reader);
                }

                if (game == null)
                {
                    throw new Exception("Le jeu de sudoku n'existe pas");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Chargement du jeu de sudoku impossible: "+e.Message);
            }

            return game;
        }

        public async void SaveGame(SudokuGame game, string timespan)
        {
            MySqlConnection? connection = null;
            string boardString = BoardEncoder.EncodeBoard(game.GameBoard);
            try
            {
                connection = new MySqlConnection(CnnStr);
                connection.Open();


                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO a23_web3_2133752.saves(User, Game, Save, Time, Date, IsDaily, Seed) VALUES(@User, @Game, @Save, @Time, @Date, @IsDaily, @Seed);";
                command.Parameters.AddWithValue("@User", ConnectionHelper.User.Id);
                command.Parameters.AddWithValue("@Game", game.Id);
                command.Parameters.AddWithValue("@Save", boardString);
                command.Parameters.AddWithValue("@Time", timespan);
                command.Parameters.AddWithValue("@Date", DateTime.Now);
                command.Parameters.AddWithValue("@IsDaily", game.IsDaily);
                command.Parameters.AddWithValue("@Seed", game.GameBoard.Seed);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Echec de la sauvegarde: "+e.Message);
            }
        }

        public SudokuGame LoadSave(bool isdaily)
        {
            MySqlConnection? connection = null;
            MySqlDataReader? reader = null;
            SudokuGame game = null;
            try
            {
                connection = new MySqlConnection(CnnStr);
                connection.Open();


                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT games.Id, games.Title, saves.Save, saves.Seed, saves.Time FROM a23_web3_2133752.saves INNER JOIN games ON saves.Game=games.Id WHERE Game = 1 AND User = @User AND IsDaily = @IsDaily ORDER BY Date DESC LIMIT 1;";
                command.Parameters.AddWithValue("@User", ConnectionHelper.User.Id);
                command.Parameters.AddWithValue("@IsDaily", isdaily);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    game = CreateFromSave(reader);
                }
                return game;
            }
            catch (Exception e)
            {
                throw new Exception("Echec du chargement de sauvegarde: " + e.Message);
            }
        }
    }
}
