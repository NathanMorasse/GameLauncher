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

namespace HourGlassUnlimited.Games.Sudoku.DataAccesLayer.Factories
{
    public class SudokuFactory : FactoryBase
    {
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
                        return "Valide";
                    }
                    return "Invalide";
                }
            }
            catch (Exception e)
            {
                throw new Exception("Échec de la validation de grille", e);
            }
        }
    }
}
