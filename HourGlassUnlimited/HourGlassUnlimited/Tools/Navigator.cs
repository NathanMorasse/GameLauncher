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
    public class Navigator
    {
        #region Singleton

        private static Navigator instance;

        private Navigator()
        {

        }

        public static Navigator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Navigator();
                }

                return instance;
            }
        }

        #endregion

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
        public void Start()
        {
            MainWindow = new MainWindow();
            Holder = MainWindow.Holder;

            SignIn = new SignIn();
            SignUp = new SignUp();
            EditAccount = new EditAccount();

            GameList = new GameList();
            Rankings = new Rankings();


            MainWindow.Show();
            SignInView();
        }

        public void SignInView()
        {
            Holder.NavigationService.Navigate(SignIn);
        }

        public void SignUpView()
        {
            Holder.NavigationService.Navigate(SignUp);
        }

        public void EditAccountView()
        {
            Holder.NavigationService.Navigate(EditAccount);
        }

        public void GameListView()
        {
            Holder.NavigationService.Navigate(GameList);
        }

        public void RankingsView()
        {
            Holder.NavigationService.Navigate(Rankings);
        }

        #endregion
    }
}
