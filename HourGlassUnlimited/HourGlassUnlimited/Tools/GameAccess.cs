using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HourGlassUnlimited.Games;
using HourGlassUnlimited.Models;

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

        public static List<GameListItem> LoadGames()
        {
            string[] gameList = ListGames();
            string mainPath = ToolKit.GetDefaultPath();
            List<GameListItem> games = new List<GameListItem>();

            foreach (string gameName in gameList)
            {
                string[] game = new string[2];
                game[0] = gameName;
                game[1] = ToolKit.DefaultImgPath;

                string traveler = ToolKit.GetDefaultPath() + "/Games/" + gameName;

                if (ImgExists(traveler))
                {
                    traveler = traveler + "/Img";

                    if (LogoExists(traveler))
                    {
                        game[1] = traveler + "/Logo.png";
                    }
                }

                games.Add(new GameListItem(game[0], game[1]));
            }

            return games;
        }

        public static string[] ListGames()
        {
            string[] games;

            games = Directory.GetDirectories(ToolKit.GetDefaultPath() + "/Games");

            int cpt = 0;

            foreach (string path in games)
            {
                games[cpt] = Path.GetFileName(path);

                cpt++;
            }

            return games;
        }

        private static bool ImgExists(string pathToGame)
        {
            string[] directories;

            directories = Directory.GetDirectories(pathToGame);

            foreach (string path in directories)
            {
                string name = Path.GetFileName(path);

                if (name == "Img")
                {
                    return true;
                }
            }

            return false;
        }

        private static bool LogoExists(string pathToImg)
        {
            string[] files;

            files = Directory.GetFiles(pathToImg);

            foreach (string path in files)
            {
                string name = Path.GetFileName(path);

                if (name == "Logo.png")
                {
                    return true;
                }
            }

            return false;
        }
    }
}
