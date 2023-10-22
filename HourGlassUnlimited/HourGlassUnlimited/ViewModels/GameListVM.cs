using HourGlassUnlimited.Models;
using HourGlassUnlimited.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HourGlassUnlimited.ViewModels
{
    public class GameListVM : VM
    {
        public List<GameListItem> GameList { get; set; }

        public ICommand LaunchGame { get; set; }

        public GameListVM() 
        {
            this.LaunchGame = new CommandLink(LaunchGame_Execute, LaunchGame_CanExecute);

            GameList = GameAccess.LoadGames();
        }

        private bool LaunchGame_CanExecute(object parameter) { return true; }
        private void LaunchGame_Execute(object parameter) 
        {
            GameAccess.LaunchGame(parameter.ToString());
        }
    }
}
