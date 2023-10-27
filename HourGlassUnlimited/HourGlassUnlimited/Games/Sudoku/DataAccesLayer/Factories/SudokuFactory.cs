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

namespace HourGlassUnlimited.Games.Sudoku.DataAccesLayer.Factories
{
    public class SudokuFactory : FactoryBase
    {
        public static SudokuGame CreateFromReader(MySqlDataReader reader)
        {
            int id = (int)reader["Id"];
            string title = reader["Title"].ToString() ?? string.Empty;
            string description = reader["Description"].ToString() ?? string.Empty;

            return new SudokuGame() { Id=id, Title=title, Description=description};
        }

        public async Task<Board> GenerateBoard(string difficulty, bool isDaily)
        {
            string paramSeed = string.Empty;
            if (isDaily)
            {
                string seed = DateTime.Now.Date.ToString();
                paramSeed = "&seed=" + seed;
            }

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
                command.CommandText = "INSERT INTO a23_web3_2133752.saves(User, Game, Save, Time, IsDaily, Seed) VALUES(@User, @Game, @Save, @Time, @IsDaily, @Seed);";
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
    }
}
