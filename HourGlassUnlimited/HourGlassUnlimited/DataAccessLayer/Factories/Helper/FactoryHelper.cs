using HourGlassUnlimited.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.DataAccessLayer.Factories.Helper
{
    public static class FactoryHelper
    {
        public static string UserByUsernameCMD
        {
            get
            {
                return "SELECT users.`Id`, users.`Username`, users.`Password`, departments.`Department` " +
                    "FROM users " +
                    "JOIN departments " +
                    "ON users.`Department` = departments.`Id` " +
                    "WHERE `Username` = @Username;";
            }
        }

        public static string AddUserCMD
        {
            get
            {
                return "insert into users (`Username`, `Password`, `Department`) " +
                    "values (@Username, @Password, (" +
                    "select `Id` " +
                    "from departments " +
                    "where `Department` = @Department));";
            }
        }

        public static string AllDepartmentCMD
        {
            get
            {
                return "select `Id`, `Department` from departments;";
            }
        }

        public static string UpdateUserCMD
        {
            get
            {
                return "update users " +
                    "set `Username`= @Username, " +
                    "`Password` = @Password, " +
                    "`Department`= (select Id from departments where Department = @Department) " +
                    "where Id = @Id;";
            }
        }

        public static string GetScoreByUserAndGameCMD
        {
            get
            {
                return "select `Id`, `User`, `Game`, `Category`, `Result`, `Time`, `Points`, `Date` " +
                    "from `scores` " +
                    "where `User` = @User " +
                    "and `Game` = (select `Id` from `games` where `Title` = @Game);";
            }
        }

        public static string ScoresByUserCMD
        {
            get
            {
                return "select `scores`.`Id`, `users`.`Username` as `User`, `scores`.`Game`, `scores`.`Category`, `scores`.`Result`, `scores`.`Time`, `scores`.`Points`, `scores`.`Date`"+
                    "from `scores` "+
                    "join `users`"+
                    "on `scores`.`User` = `users`.`Id` "+
                    "where `scores`.`User` = 2;";
            }
        }

        public static string ScoreByUserCMD
        {
            get
            {
                return "select `Id`, `User`, `Game`, `Category`, `Result`, `Time`, `Points`, `Date` " +
                    "from `scores` " +
                    "where `User` = @User;";
            }
        }

        public static string UsersRankedByPointsCMD
        {
            get
            {
                return "select  `scores`.`Id`, `users`.`Username` as `User`, `Game`, `Category`, `Result`, `Time`, Sum(`Points`) as `Points`, `Date` " +
                    "from `scores` " +
                    "join `users` " +
                    "on `scores`.`User` = `users`.`Id` " +
                    "group by `User` " +
                    "order by Sum(`Points`) desc;";
            }
        }

        public static string Top1PointsUserCMD
        {
            get
            {
                return "select  `Id`, `User`, `Game`, `Category`, `Result`, `Time`, Sum(`Points`) as `Points`, `Date` " +
                    "from `scores` " +
                    "group by `User` " +
                    "order by Sum(`Points`) desc " +
                    "limit 1;";
            }
        }

        public static string BestUsersFromDepartmentCMD
        {
            get
            {
                return "select  `scores`.`Id`, `users`.`Username` as `User`, `Game`, `Category`, `Result`, `Time`, Sum(`Points`) as `Points`, `Date` " +
                    "from `scores` " +
                    "join `users` " +
                    "on `scores`.`User` = `users`.`Id` " +
                    "where `users`.`Department` = @User " +
                    "group by `User` " +
                    "order by Sum(`Points`) desc;";
            }
        }

        public static string BestDepartmentsCMD
        {
            get
            {
                return "select 1 as `Id`, `departments`.`Department` as `User`, 1 as `Game`, null as `Category`, null as `Result`, null as `Time`, Sum(`Points`) as `Points`, null as `Date`" +
                    "from `scores`" +
                    "join `users`" +
                    "on `scores`.`User` = `users`.`Id`" +
                    "join `departments`" +
                    "on `users`.`Department` = `departments`.`Id`" +
                    "group by `departments`.`Department`" +
                    "order by Sum(`Points`) desc;";
            }
        }

        public static string BestDepartmentsByGameCMD
        {
            get
            {
                return "select `scores`.`Id`, `departments`.`Department`, Sum(`Points`) as `Points`" +
                    "from `scores`"+
                    "join `users`"+
                    "on `scores`.`User` = `users`.`Id`"+
                    "join `departments`"+
                    "on `users`.`Department` = `departments`.`Id`"+
                    "where `Game` = (select `Id` from `games` where `Title` = @Game)"+
                    "group by `User`"+
                    "order by Sum(`Points`) desc;";
            }
        }

        public static string GetDepartmentByUserId
        {
            get
            {
                return "select departments.Id, departments.Department " +
                    "from departments " +
                    "join users " +
                    "on users.Department = departments.Id " +
                    "where users.Id = @User;";
            }
        }

        public static string BestUsersByGame
        {
            get
            {
                return "select  `scores`.`Id`, `users`.`Username` as `User`, `Game`, `Category`, `Result`, `Time`, Sum(`Points`) as `Points`, `Date` " +
                    "from `scores` " +
                    "join `users` " +
                    "on `scores`.`User` = `users`.`Id` " +
                    "group by `User` " +
                    "where `Game` = (select `Id` from `games` where `Title` = @Game) " +
                    "order by `Points`;";
            }
        }

        public static User UserFromReader(MySqlDataReader reader)
        {
            int id = (int)reader["Id"];
            string username = reader["Username"].ToString() ?? string.Empty;
            string password = reader["Password"].ToString() ?? string.Empty;
            string department = reader["Department"].ToString() ?? string.Empty;

            return new User(id, username, password, department);
        }

        public static Department DepartmentFromReader(MySqlDataReader reader)
        {
            int id = (int)reader["Id"];
            string name = reader["Department"].ToString() ?? string.Empty;

            return new Department(id, name);
        }

        public static Score ScoreFromReader(MySqlDataReader reader)
        {
            int id = Convert.ToInt32(reader["Id"]);
            string user = reader["User"].ToString() ?? string.Empty;
            int game = Convert.ToInt32(reader["Game"]);
            string category = reader["Category"].ToString() ?? string.Empty;
            string Result = reader["Result"].ToString() ?? string.Empty;
            TimeSpan time;
            if (!TimeSpan.TryParse(reader["Time"].ToString(), out time))
            {
                time = TimeSpan.Zero;
            }
            int points = Convert.ToInt32(reader["Points"]);
            DateTime date;
            if (!DateTime.TryParse(reader["Date"].ToString(), out date))
            {
                date = DateTime.Now;
            }

            return new Score(id, user, game, category, Result, time, points, date);
        }
    }
}
