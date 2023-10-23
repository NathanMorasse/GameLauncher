using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HourGlassUnlimited.Views;
using System.Windows.Controls;
using System.Windows;

namespace HourGlassUnlimited.Tools
{
    public static class Navigator
    {

        #region Attributes

        private static MainWindow _mainWindow;
        private static Frame _holder;

        private static SignIn _signIn;
        private static SignUp _signUp;
        private static EditAccount _editAccount;

        private static GameList _gameList;
        private static Rankings _rankings;

        #endregion

        #region Properties

        public static MainWindow MainWindow { get { return _mainWindow; } set { _mainWindow = value; } }
        public static Frame Holder { get { return _holder; } set { _holder = value; } }

        public static SignIn SignIn { get { return _signIn; } set { _signIn = value; } }
        public static SignUp SignUp { get { return _signUp; } set { _signUp = value; } }
        public static EditAccount EditAccount { get { return _editAccount; } set { _editAccount = value; } }

        public static GameList GameList { get { return _gameList; } set { _gameList = value; } }
        public static Rankings Rankings { get { return _rankings; } set { _rankings = value; } }

        #endregion

        #region Navigation

        public static void Start()
        {
            MainWindow = new MainWindow();
            Holder = MainWindow.Holder;

            GameList = new GameList();
            Rankings = new Rankings();
        }

        public static void SignInView()
        {
            SignIn = new SignIn();
            Holder.NavigationService.Navigate(SignIn);
        }

        public static void SignUpView()
        {
            SignUp = new SignUp();
            Holder.NavigationService.Navigate(SignUp);
        }

        public static void EditAccountView()
        {
            EditAccount = new EditAccount();
            Holder.NavigationService.Navigate(EditAccount);
        }

        public static void GameListView()
        {
            Holder.NavigationService.Navigate(GameList);
        }

        public static void RankingsView()
        {
            Holder.NavigationService.Navigate(Rankings);
        }

        #endregion
    }
}
