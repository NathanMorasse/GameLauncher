using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlassUnlimited.Models
{
    public class RankingSection
    {
        private string _title;
        private List<RankingItem> _scores;

        public string Title { get { return _title; } set { _title = value; } }
        public List<RankingItem> Scores { get { return _scores; } set { _scores = value; } }

        public RankingSection() { }

        public RankingSection(string title, List<RankingItem> scores)
        {
            Title = title;
            Scores = scores;
        }
    }
}
