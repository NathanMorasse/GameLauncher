using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HourGlassUnlimited.Games;

namespace HourGlassUnlimited.Tools
{
    /// <summary>
    /// La classe permet de savoir quelle jeu sont disponible et d'y accèder.
    /// Le but est que lors de l'ajout d'un, les seul choses à changer dans code sois ici.
    /// </summary>
    public static class GameAccess
    {
        public static void LaunchGame(string game)
        {
            switch (game)
            {
                case "Sudoku":
                    {
                        Games.Sudoku.Tools.SudokuNavigator.Start();
                        break;
                    }
            }
        }

        public static string[] ListGames()
        {
            string[] games;

            games = Directory.GetDirectories("../../../Games");

            int cpt = 0;

            foreach (string path in games)
            {
                games[cpt] = Path.GetFileName(path);

                cpt++;
            }

            return games;
        }
    }
}
