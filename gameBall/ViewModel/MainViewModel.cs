using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace gameBall.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Team> _teams = null;

        public ObservableCollection<Team> teams
        {
            get { return _teams; }
            set { _teams = value; }
        } 

        public MainViewModel()
        {
            _teams = new ObservableCollection<Team>();

            _teams.Add(new Team { name = "Polska", sym="POL", tournamentDisposition=0.9, ranked=17 });
            
        }
    }
}