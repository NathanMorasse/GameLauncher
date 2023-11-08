using HourGlassUnlimited.DataAccessLayer;
using HourGlassUnlimited.Models;
using HourGlassUnlimited.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.ViewModels
{
    public class RankingsVM : VM
    {
        public ObservableCollection<RankingSection> Sections { get; set; }
        public List<string> Categories { get; set; }

        public RankingsVM() 
        {
            Sections = new ObservableCollection<RankingSection>();

            GetCategories();

            GetExamples();
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

        private void GetExamples()
        {
            List<Score> test;
            DAL dal = new DAL();

            test = dal.Scores.ByUserAndGame(1, "Sudoku");

            RankingSection test1 = new RankingSection();
            test1.Title = "Section #1";
            test1.Scores = GetSome(test);
            
            RankingSection test2 = new RankingSection();
            test2.Title = "Section #2";
            test2.Scores = GetSome(test);
            
            RankingSection test3 = new RankingSection();
            test3.Title = "Long texte";
            test3.Scores = GetSome(test);

            Sections.Add(test1);
            Sections.Add(test2);
            Sections.Add(test3);
        }

        private List<RankingItem> GetSome(List<Score> s)
        {
            RankingItem a = new RankingItem();
            RankingItem b = new RankingItem();
            RankingItem c = new RankingItem();

            a.PrimarySlot1 = s[0].UserId.ToString();
            a.PrimarySlot2 = s[0].Result;
            a.PrimarySlot3 = s[0].Time.ToString();

            b.PrimarySlot1 = s[0].Time.ToString();
            b.SecondarySlot1 = s[0].Points.ToString();
            b.PrimarySlot2 = s[0].Result;
            b.PrimarySlot3 = s[0].UserId.ToString();
            b.SecondarySlot2= s[0].Date.ToString();

            c.SecondarySlot1 = s[0].UserId.ToString();
            c.PrimarySlot2 = s[0].Time.ToString();
            c.SecondarySlot2 = s[0].Date.ToString();


            var test = new List<RankingItem>();

            test.Add(a);
            test.Add(b);
            test.Add(c);
            return test;
        }
    }
}
