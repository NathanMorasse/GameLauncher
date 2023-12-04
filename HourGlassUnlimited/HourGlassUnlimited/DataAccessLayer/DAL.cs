using HourGlassUnlimited.DataAccessLayer.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.DataAccessLayer
{
    public class DAL
    {
        private static UserFactory? _UserFact;
        private static DepartmentFactory? _DepartmentFact;
        private static GameFactory? _GameFact;
        private static ScoreFactory? _ScoreFact;

        public UserFactory? Users
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

        public DepartmentFactory? Departments
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

        public GameFactory? Games
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

        public ScoreFactory? Scores
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
    }
}
