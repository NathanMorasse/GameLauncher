using HourGlassUnlimited.DataAccessLayer;
using HourGlassUnlimited.DataAccessLayer.Factories.Helper;
using HourGlassUnlimited.Models;
using HourGlassUnlimited.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HourGlassUnlimited.ViewModels
{
    public class RankingsVM : VM
    {
        public ObservableCollection<RankingSection> Sections { get; set; }
        public List<string> Categories { get; set; }

        public ICommand GetScores { get; set; }

        public RankingsVM() 
        {
            Sections = new ObservableCollection<RankingSection>();
            this.GetScores = new CommandLink(GetScores_Execute, GetScores_CanExecute);

            GetCategories();

            GetGlobaleScores();
        }

        private bool GetScores_CanExecute(object parameter) { return true; }
        private void GetScores_Execute(object parameter)
        {
            if (parameter.ToString() == "Globale")
            {
                GetGlobaleScores();
            }
            else
            {
                GetGameScores(parameter.ToString());
            }

            string bob = (string)parameter;
        }

        private void GetCategories()
        {
            string[] list = GameAccess.ListGames();

            Categories = new List<string>();

            Categories.Add("Globale");

            foreach (string category in list)
            {
                Categories.Add(category);
            }
        }

        private void GetGlobaleScores()
        {
            DAL dal = new DAL();
            //Top3
            List<Score> scores = dal.Scores.CustomList(null, null, FactoryHelper.Top3PointsUserCMD);

            if (scores[0].Id > 0)
            {
                RankingSection Top3 = new RankingSection();
                Top3.Title = "Top 3 des joueurs avec le plus de points";
                Top3.Scores = new List<RankingItem>();

                int cpt = 0;

                foreach (Score item in scores)
                {
                    cpt++;

                    RankingItem ri = new RankingItem();

                    ri.PrimarySlot1 = cpt.ToString();
                    ri.PrimarySlot2 = item.User;
                    ri.PrimarySlot3 = item.Points.ToString() + "pts";

                    Top3.Scores.Add(ri);
                }

                Sections.Add(Top3);
            }

            ChangeValue("Sections");
        }

        private void GetGameScores(string game)
        {
            Sections = new ObservableCollection<RankingSection>();

            ChangeValue("Sections");
        }
    }
}
