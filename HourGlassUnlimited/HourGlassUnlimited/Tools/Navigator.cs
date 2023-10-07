using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HourGlassUnlimited.Views;
using System.Windows.Controls;

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

        public static MainWindow MainWindowView { get { return _mainWindow; } set { _mainWindow = value; } }
        public static Frame Holder { get { return _holder; } set { _holder = value; } }

        public static SignIn SignInView { get { return _signIn; } set { _signIn = value; } }
        public static SignUp SignUpView { get { return _signUp; } set { _signUp = value; } }
        public static EditAccount EditAccountView { get { return _editAccount; } set { _editAccount = value; } }

        public static GameList GameListView { get { return _gameList; } set { _gameList = value; } }
        public static Rankings RankingsView { get { return _rankings; } set { _rankings = value; } }

        #endregion

        #region Initialization
        public static void InitializeService()
        {
            MainWindowView = new MainWindow();
            Holder = MainWindowView.Holder;

            SignInView = new SignIn();
            SignUpView = new SignUp();
            EditAccountView = new EditAccount();
            GameListView = new GameList();
            RankingsView = new Rankings();
        }
        #endregion

        #region Navigation
        public static void SignIn()
        {
            Holder.NavigationService.Navigate(SignInView);
        }

        public static void SignUp()
        {
            Holder.NavigationService.Navigate(SignUpView);
        }

        public static void EditAccount()
        {
            Holder.NavigationService.Navigate(EditAccountView);
        }

        public static void GameList()
        {
            Holder.NavigationService.Navigate(GameListView);
        }

        public static void Rankings()
        {
            Holder.NavigationService.Navigate(RankingsView);
        }
        #endregion
    }
}
