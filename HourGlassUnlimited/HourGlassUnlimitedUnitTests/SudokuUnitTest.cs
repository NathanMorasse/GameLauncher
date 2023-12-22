using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using HourGlassUnlimited.Games.Sudoku.DataAccesLayer;
using HourGlassUnlimited;
using HourGlassUnlimited.Games.Sudoku.Models;
using HourGlassUnlimited.Games.Sudoku.Tools;
using System.Collections.ObjectModel;
using HourGlassUnlimited.DataAccessLayer;
using Mysqlx.Resultset;
using HourGlassUnlimited.Tools;
using HourGlassUnlimited.Models;
using MySql.Data.MySqlClient;

namespace GameLauncherUnitTests
{
    [TestClass]
    public class SudokuUnitTest
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            ConnectionHelper.SignIn("TestUser", "12345"); //Le SaveGame se base sur l'utilisateur connecté
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            MySqlConnection? connection = null;
            connection = new MySqlConnection("Server=sql.decinfo-cchic.ca;Port=33306;Database=a23_web3_2133752;Uid=dev-2133752;Pwd=Mateo235");
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "delete from saves where User = @User ORDER BY Date DESC LIMIT 1;";
            command.Parameters.AddWithValue("@User", ConnectionHelper.User.Id);
            command.ExecuteNonQuery();
            connection.Close();
        }

        [TestMethod]
        public async Task BoardGenerationTest()
        {
            SudokuDAL dal = new SudokuDAL();
            try
            {
                Board result = await dal.SudokuFact.GenerateBoard("easy", false, null, "");
                if (result == null) { Assert.Fail(); }
                foreach (var row in result.Grid)
                {
                    foreach (Cell cell in row)
                    {
                        if (cell.Value < 0 || cell.Value > 9)
                        {
                            Assert.Fail();
                        }
                    }
                }
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task ValidateBoardTest()
        {
            string validBoardString1 = "167483529394251678582679431418726395235194786679835142823517964956348217741962853";
            string validBoardString2 = "296471583751368924834529716463982175129754638587613249912835467648197352375246891";
            string validBoardString3 = "549832617378641529162975834823196745497583261651724398786319452934258176215467983";
            string validBoardString4 = "469713582132658974875294136743581269651932847298467351386179425514326798927845613";
            string validBoardString5 = "451389726963724185782156943839567214546291378217438569624973851378615492195842637";

            List<string> validBoardStrings = new List<string>() { validBoardString1, validBoardString2, validBoardString3, validBoardString4, validBoardString5 };
            List<Board> validBoards = new List<Board>();
            foreach (string validBoardString in validBoardStrings)
            {
                validBoards.Add(BoardEncoder.DecodeBoard(validBoardString));
            }

            string invalidBoardString1 = "117483529394251678582679431418726395235194786679835142823517964956348217741962853";
            string invalidBoardString2 = "16748352939425167858267943141872639523519478667983514282351796495634821774196.853";
            string invalidBoardString3 = "1674835293942516785826794314187263952351947866798351428235179649563482177419628534";
            string invalidBoardString4 = "16748352939425167858267943141872639523519478667983514282351796495634821774196285";
            string invalidBoardString5 = "167483529394251578582679431418726395235194786679835542823557964956348215741962853";

            List<string> invalidBoardStrings = new List<string>() { invalidBoardString1, invalidBoardString2, invalidBoardString3, invalidBoardString4, invalidBoardString5 };
            List<Board> invalidBoards = new List<Board>();
            foreach (string invalidBoardString in invalidBoardStrings)
            {
                try
                {
                    invalidBoards.Add(BoardEncoder.DecodeBoard(invalidBoardString));
                }
                catch (ArgumentException e)
                {
                    //Une ArgumentException est attendue dans ce cas mais on veut continuer de tester les autres grilles donc on met un try catch vide
                }
            }

            SudokuDAL dal = new SudokuDAL();
            foreach (Board validBoard in validBoards)
            {
                string result = await dal.SudokuFact.ValidateBoard(validBoard);
                if (result != "valid")
                {
                    Assert.Fail();
                }
            }

            foreach (Board invalidBoard in invalidBoards)
            {
                string result = await dal.SudokuFact.ValidateBoard(invalidBoard);
                if (result != "invalid")
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public async Task SaveAndLoadTest()
        {
            Random rnd = new Random();
            Board expectedBoard = new Board();
            // Génération d'une grille aléatoire
            for (int i = 0; i < 9; i++)
            {
                ObservableCollection<Cell> row = new ObservableCollection<Cell>();
                for (int j = 0; j < 9; j++)
                {
                    Cell cell = new Cell();
                    cell.Value = rnd.Next(1, 10);
                    row.Add(cell);
                }
                expectedBoard.Grid.Add(row);
            }

            SudokuDAL sudokuDal = new SudokuDAL();
            DAL dal = new DAL();
            GameBase gameBase = dal.Games.GetByTitle("Sudoku");
            SudokuGame gameToSave = new SudokuGame { Id = gameBase.Id, Title = gameBase.Title, Date = DateTime.Now };
            gameToSave.GameBoard = expectedBoard;
            gameToSave.IsDaily = false;

            ConnectionHelper.SignIn("TestUser", "12345"); //Le SaveGame se base sur l'utilisateur connecté
            await sudokuDal.SudokuFact.SaveGame(gameToSave, "00:00:13");

            SudokuGame LoadedGame = sudokuDal.SudokuFact.LoadSave(false);
            Board loadedBoard = LoadedGame.GameBoard;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Cell test = loadedBoard.Grid[i][j];
                    if (loadedBoard.Grid[i][j].Value != expectedBoard.Grid[i][j].Value)
                    {
                        Assert.Fail();
                    }
                }
            }
        }
    }
}
