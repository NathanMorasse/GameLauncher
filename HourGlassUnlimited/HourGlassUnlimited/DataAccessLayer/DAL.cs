using HourGlassUnlimited.DataAccessLayer.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.DataAccessLayer
{
    public static class DAL
    {
        private static UserFactory? _UserFact;
        private static DepartmentFactory? _DepartmentFact;
        private static GameFactory? _GameFact;
        private static ScoreFactory? _ScoreFact;
        private static SaveFactory? _SaveFact;
        private static DailyFactory? _DailyFact;

        public static UserFactory? Users
        {
            get
            {
                if (_UserFact == null)
                {
                    _UserFact = new UserFactory();
                }
                return _UserFact;
            }
        }

        public static DepartmentFactory? Departments
        {
            get
            {
                if (_DepartmentFact == null)
                {
                    _DepartmentFact = new DepartmentFactory();
                }
                return _DepartmentFact;
            }
        }

        public static GameFactory? Games
        {
            get
            {
                if (_GameFact == null)
                {
                    _GameFact = new GameFactory();
                }
                return _GameFact;
            }
        }

        public static ScoreFactory? Scores
        {
            get
            {
                if (_ScoreFact == null)
                {
                    _ScoreFact = new ScoreFactory();
                }
                return _ScoreFact;
            }
        }

        public static SaveFactory? Saves
        {
            get
            {
                if (_SaveFact == null)
                {
                    _SaveFact = new SaveFactory();
                }
                return _SaveFact;
            }
        }

        public static DailyFactory? Dailies
        {
            get
            {
                if (_DailyFact == null)
                {
                    _DailyFact = new DailyFactory();
                }
                return _DailyFact;
            }
        }
    }
}
