using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HourGlassUnlimited.Games.Sudoku.DataAccesLayer.Factories.Base;
using HourGlassUnlimited.Games.Sudoku.Models;
using Newtonsoft.Json;

namespace HourGlassUnlimited.Games.Sudoku.DataAccesLayer.Factories
{
    public class SudokuFactory : FactoryBase
    {
        public async Task<Board> GenerateBoard(string difficulty)
        {
			try
			{
				HttpResponseMessage response = await APIClient.GetAsync(BaseUri + $"board?difficulty={difficulty}");
                string content = await response.Content.ReadAsStringAsync();
                Board board = JsonConvert.DeserializeObject<Board>(content.Replace("board", "Grid"));
                return board;
            }
			catch (Exception e)
			{
				throw new Exception("Échec de génération de grille", e);
			}
        }
    }
}
