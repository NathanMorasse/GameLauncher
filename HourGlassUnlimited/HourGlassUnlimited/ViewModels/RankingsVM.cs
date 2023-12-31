﻿using HourGlassUnlimited.DataAccessLayer;
using HourGlassUnlimited.DataAccessLayer.Factories.Helper;
using HourGlassUnlimited.Models;
using HourGlassUnlimited.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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

            if (Sections.Count == 0)
            {
                GetGlobaleScores();
            }
        }

        private bool GetScores_CanExecute(object parameter) { return true; }
        private void GetScores_Execute(object parameter)
        {
            Sections.Clear();
            if (parameter.ToString() == "Global")
            {
                GetGlobaleScores();
            }
            else if (parameter.ToString() == "Personnel")
            {
                GetPersonalScores();
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

            Categories.Add("Global");
            Categories.Add("Personnel");

            foreach (string category in list)
            {
                Categories.Add(category);
            }
        }

        private void GetGlobaleScores()
        {
            DAL dal = new DAL();
            List<Score> topPointsScores = dal.Scores.CustomList(null, null, FactoryHelper.UsersRankedByPointsCMD);
            if (topPointsScores[0].Id > 0)
            {
                RankingSection topPointsSection = CreateGlobalSection(topPointsScores, "Classement des joueurs avec le plus de points");
                Sections.Add(topPointsSection);
            }

            List<Score> bestDepartmentsScore = dal.Scores.CustomList(null, null, FactoryHelper.BestDepartmentsCMD);
            if (bestDepartmentsScore[0].Id > 0)
            {
                RankingSection bestDepartmentSection = CreateGlobalSection(bestDepartmentsScore, "Classement des départements");
                Sections.Add(bestDepartmentSection);
            }

            List<Score> topScores = dal.Scores.CustomList(null, null, FactoryHelper.BestScores);
            if (topScores[0].Id > 0)
            {
                RankingSection topScoresSection = CreateGlobalSection(topScores, "Top 5 des meilleurs scores");
                Sections.Add(topScoresSection);
            }

            ChangeValue("Sections");
        }

        private void GetPersonalScores()
        {
            DAL dal = new DAL();
            List<Score> myScores = dal.Scores.CustomList(ConnectionHelper.User.Id, null, FactoryHelper.ScoresByUserCMD);
            if (myScores[0].Id > 0)
            {
                RankingSection myScoresSection = CreatePersonalSection(myScores, "Mes scores");
                Sections.Add(myScoresSection);
            }

            Department department = dal.Departments.GetByUserId(ConnectionHelper.User.Id);
            List<Score> myDepartmentScores = dal.Scores.CustomList(department.Id, null, FactoryHelper.BestUsersFromDepartmentCMD);
            if (myDepartmentScores[0].Id > 0)
            {
                RankingSection myDepartmentSection = CreateGlobalSection(myDepartmentScores, "Mon département");
                Sections.Add(myDepartmentSection);
            }

            ChangeValue("Sections");
        }

        private void GetGameScores(string game)
        {
            DAL dal = new DAL();
            List<Score> topPointsScores = dal.Scores.CustomList(null, game, FactoryHelper.BestUsersByPointsAndGame);
            if (topPointsScores[0].Id > 0)
            {
                RankingSection topPointsSection = CreateGlobalSection(topPointsScores, "Classement des joueurs avec le plus de points");
                Sections.Add(topPointsSection);
            }

            List<Score> topScores = dal.Scores.CustomList(null, game, FactoryHelper.BestScoresByGame);
            if (topScores[0].Id > 0)
            {
                RankingSection topScoresSection = CreateGameSection(topScores, "Meilleurs scores", false);
                Sections.Add(topScoresSection);
            }

            List<Score> myScores = dal.Scores.CustomList(ConnectionHelper.User.Id, game, FactoryHelper.ScoresByUserAndGameCMD);
            if (myScores[0].Id > 0)
            {
                RankingSection myScoresSection = CreateGameSection(myScores, "Mes scores de Sudoku", true);
                Sections.Add(myScoresSection);
            }

            List<Score> bestDepartmentsScore = dal.Scores.CustomList(null, game, FactoryHelper.BestDepartmentsByGameCMD);
            if (bestDepartmentsScore[0].Id > 0)
            {
                RankingSection bestDepartmentSection = CreateGlobalSection(bestDepartmentsScore, "Classement des départements");
                Sections.Add(bestDepartmentSection);
            }

            ChangeValue("Sections");
        }

        private RankingSection CreateGlobalSection(List<Score> scores, string title)
        {
            RankingSection ranking = new RankingSection();
            ranking.Title = title;
            ranking.Scores = new List<RankingItem>();

            int cpt = 0;

            foreach (Score item in scores)
            {
                cpt++;

                RankingItem ri = new RankingItem();

                ri.PrimarySlot1 = cpt.ToString();
                ri.PrimarySlot2 = item.User;
                ri.PrimarySlot3 = item.Points.ToString() + "pts";

                ranking.Scores.Add(ri);
            }

            return ranking;
        }

        private RankingSection CreatePersonalSection(List<Score> scores, string title)
        {
            RankingSection ranking = new RankingSection();
            DAL dal = new DAL();
            ranking.Title = title;
            ranking.Scores = new List<RankingItem>();

            int cpt = 0;

            foreach (Score item in scores)
            {
                cpt++;

                RankingItem ri = new RankingItem();

                ri.PrimarySlot1 = cpt.ToString();
                GameBase game = dal.Games.GetById(item.GameId);
                ri.PrimarySlot2 = game.Title;
                ri.PrimarySlot3 = item.Points.ToString() + "pts";

                ranking.Scores.Add(ri);
            }

            return ranking;
        }

        private RankingSection CreateGameSection(List<Score> scores, string title, bool isPersonalGame)
        {
            RankingSection ranking = new RankingSection();
            ranking.Title = title;
            ranking.Scores = new List<RankingItem>();

            int cpt = 0;

            foreach (Score item in scores)
            {
                cpt++;

                RankingItem ri = new RankingItem();
                ri.PrimarySlot1 = cpt.ToString();
                if (isPersonalGame)
                {
                    ri.PrimarySlot2 = string.Empty;
                }
                else
                {
                    ri.PrimarySlot2 = item.User;
                }
                ri.PrimarySlot3 = item.Time.ToString();

                ranking.Scores.Add(ri);
            }

            return ranking;
        }
    }
}
