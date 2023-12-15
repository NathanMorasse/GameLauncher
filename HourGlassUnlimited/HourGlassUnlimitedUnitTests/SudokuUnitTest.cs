using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using HourGlassUnlimited.Games.Sudoku.DataAccesLayer;
using HourGlassUnlimited;
using HourGlassUnlimited.Games.Sudoku.Models;
using HourGlassUnlimited.Games.Sudoku.Tools;

namespace GameLauncherUnitTests
{
    [TestClass]
    public class SudokuUnitTest
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {

        }

        [ClassCleanup]
        public static void Cleanup()
        {

        }

        [TestMethod]
        public async Task ValidateBoardTest()
        {
            string validBoardString1 = "167483529394251678582679431418726395235194786679835142823517964956348217741962853";
            string validBoardString2 = "296471583751368924834529716463982175129754638587613249912835467648197352375246891";
            string validBoardString3 = "549832617378641529162975834823196745497583261651724398786319452934258176215467983";
            string validBoardString4 = "469713582132658974875294136743581269651932847298467351386179425514326798927845613";
            string validBoardString5 = "451389726963724185782156943839567214546291378217438569624973851378615492195842637";

            List<string> validBoardStrings = new List<string>() {validBoardString1, validBoardString2, validBoardString3, validBoardString4, validBoardString5 };
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
    }
}
