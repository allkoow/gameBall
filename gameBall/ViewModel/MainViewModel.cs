using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace gameBall
{
    public enum group { A, B, C, D };
    public enum round { I, II, III, IV, final };

    public class MainViewModel : ViewModelBase
    {
        #region Variables and collection
        private ObservableCollection<Team> _teams = new ObservableCollection<Team>();
        public ObservableCollection<Team> teams
        {
            get { return _teams; }
            set { _teams = value; }
        }

        private ObservableCollection<TeamWorldCup> _teamsWorldCup = new ObservableCollection<TeamWorldCup>();
        public ObservableCollection<TeamWorldCup> teamsWorldCup
        {
            get { return _teamsWorldCup; }
            set { _teamsWorldCup = value; }
        }

        string datasFileAddress = "data.xml";
        string worldCupFileAddress = "worldCup.xml";

        // Objects for game
        private Team _selectedPlayer = null;
        public Team selectedPlayer
        {
            get { return _selectedPlayer; }
            set
            {
                _selectedPlayer = value;
                RaisePropertyChanged(nameof(selectedPlayer));
            }
        }

        private Player _playerA = null;
        public Player playerA
        {
            get { return _playerA; }
            set
            {
                _playerA = value;
                RaisePropertyChanged(nameof(playerA));
            }
        }

        private Player _playerB = null;
        public Player playerB
        {
            get { return _playerB; }
            set
            {
                _playerB= value;
                RaisePropertyChanged(nameof(playerB));
            }
        }

        private Game _game = null;
        public Game game
        {
            get { return _game; }
            set
            {
                _game = value;
                RaisePropertyChanged(nameof(game));
            }
        }

        BackgroundWorker _backgroundWorker = null;

        private bool playWorldCup = true;
        #endregion

        public MainViewModel()
        {
            SaveDataCommand = new RelayCommand(saveDataToFile);
            LoadDataCommand = new RelayCommand(loadDataFromFile);
            AddTeam1Command = new RelayCommand(addTeam1);
            AddTeam2Command = new RelayCommand(addTeam2);

            StartGameCommand = new RelayCommand(startGame);
            CancelGameCommand = new RelayCommand(cancelGame);

            NewWorldCupCommand = new RelayCommand(newWorldCup);
            SaveWorldCupCommand = new RelayCommand(saveWorldCup);
            LoadWorldCupCommand = new RelayCommand(loadWorldCup);
        }

        private void loadWorldCup()
        {
            try
            {
                XPathDocument pathDocument = new XPathDocument(worldCupFileAddress);
                XPathNavigator pathNavigator = pathDocument.CreateNavigator();
                XPathNodeIterator pathNodeIterator = pathNavigator.Select("/Teams/TeamWorldCup");

                _teamsWorldCup.Clear();

                foreach (XPathNavigator team in pathNodeIterator)
                {
                    double matchesRatioConvert = convertStringToDouble(team.SelectSingleNode("matchesRatio").Value);
                    double setsRatioConvert = convertStringToDouble(team.SelectSingleNode("setsRatio").Value);
                    double pointsRatioConvert = convertStringToDouble(team.SelectSingleNode("pointsRatio").Value);

                    _teamsWorldCup.Add(new TeamWorldCup()
                    {
                        name = team.SelectSingleNode("Name").Value,
                        sym = team.SelectSingleNode("Symbol").Value,
                        ranked = Convert.ToInt16(team.SelectSingleNode("Ranked").Value),

                        matchesPlayed = Convert.ToInt16(team.SelectSingleNode("matchesPlayed").Value),
                        matchesWon = Convert.ToInt16(team.SelectSingleNode("matchesWon").Value),
                        matchesLost = Convert.ToInt16(team.SelectSingleNode("matchesLost").Value),
                        matchesRatio = matchesRatioConvert,

                        setsWon = Convert.ToInt16(team.SelectSingleNode("setsWon").Value),
                        setsLost = Convert.ToInt16(team.SelectSingleNode("setsLost").Value),
                        setsRatio = setsRatioConvert,

                        pointsAchieved = Convert.ToInt16(team.SelectSingleNode("pointsAchieved").Value),
                        pointsLost = Convert.ToInt16(team.SelectSingleNode("pointsLost").Value),
                        pointsRatio = pointsRatioConvert,

                        pointsInTournament = Convert.ToInt16(team.SelectSingleNode("pointsInTournament").Value)
                    });
                }
            }
            catch (Exception ex)
            {
                messageBoxError(ex);
            }
        }

        private void saveWorldCup()
        {
            if (_teamsWorldCup != null)
                saveCollectionToFile(_teamsWorldCup, typeof(ObservableCollection<TeamWorldCup>), worldCupFileAddress, "Teams");
        }

        private void newWorldCup()
        {
            foreach(Team team in _teams)
            {
                _teamsWorldCup.Add(new TeamWorldCup()
                {
                    name = team.name,
                    sym =team.sym,
                    ranked = team.ranked
                });
            }
        }

        #region adding team methods
        public void addTeam1()
        {
            if(_playerA == null)
            {
                _playerA = new Player()
                {
                    name = selectedPlayer.name,
                    ranked = selectedPlayer.ranked,
                    sym = selectedPlayer.sym,
                    tournamentDisposition = selectedPlayer.tournamentDisposition
                };

                RaisePropertyChanged(nameof(playerA));
            }
        }
        
        public void addTeam2()
        {
            if(_playerB == null)
            {
                _playerB = new Player()
                {
                    name = selectedPlayer.name,
                    ranked = selectedPlayer.ranked,
                    sym = selectedPlayer.sym,
                    tournamentDisposition = selectedPlayer.tournamentDisposition
                };

                RaisePropertyChanged(nameof(playerB));
            }   
        }
        #endregion

        #region methods for game - backgroundworker
        public void startGame()
        {
            if(_backgroundWorker == null)
            {
                MessageBox.Show("Poczatek meczu!");

                if (_game == null)
                {
                    _game = new Game(playerA, playerB, teamsWorldCup);
                    RaisePropertyChanged(nameof(game));
                }

                _backgroundWorker = new BackgroundWorker();
                _backgroundWorker.DoWork += new DoWorkEventHandler(_game.playGame);
                _backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_game.endOfTheMatch);
                _backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(gameProgressChanged);
                _backgroundWorker.WorkerReportsProgress = true;
                _backgroundWorker.WorkerSupportsCancellation = true;

                _backgroundWorker.RunWorkerAsync();
            }
        }

        private void cancelGame()
        {
            if(_backgroundWorker != null)
            {
                _backgroundWorker.CancelAsync();
                _backgroundWorker.Dispose();
                _backgroundWorker = null;

                _playerA.Dispose();
                _playerB.Dispose();
                _playerA = null;
                _playerB = null;

                RaisePropertyChanged(nameof(playerA));
                RaisePropertyChanged(nameof(playerB));

                _game = null;
            }   
        }

        private void gameProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(game));
        }
        #endregion

        #region commands
        public ICommand LoadDataCommand { get; private set; }
        public ICommand SaveDataCommand { get; private set; }
        public ICommand AddTeam1Command { get; private set; }
        public ICommand AddTeam2Command { get; private set; }
        public ICommand StartGameCommand { get; private set; }
        public ICommand CancelGameCommand { get; private set; }
        public ICommand NewWorldCupCommand { get; private set; }
        public ICommand SaveWorldCupCommand { get; private set; }
        public ICommand LoadWorldCupCommand { get; private set; }
        #endregion

        #region file operations
        public void loadDataFromFile()
        {
            try
            {
                XPathDocument pathDocument = new XPathDocument(datasFileAddress);
                XPathNavigator pathNavigator = pathDocument.CreateNavigator();
                XPathNodeIterator pathNodeIterator = pathNavigator.Select("/Teams/Team");

                _teams.Clear();

                foreach (XPathNavigator team in pathNodeIterator)
                {
                    double tournamentDispositionConvert = convertStringToDouble(team.SelectSingleNode("Disposition").Value);
                    double pointsInRankingConvert = convertStringToDouble(team.SelectSingleNode("Points").Value);

                    _teams.Add(new Team()
                    {
                        name = team.SelectSingleNode("Name").Value,
                        sym = team.SelectSingleNode("Symbol").Value,
                        ranked = Convert.ToInt16(team.SelectSingleNode("Ranked").Value),
                        pointsInRanking = pointsInRankingConvert,
                        tournamentDisposition = tournamentDispositionConvert
                    });
                }
            }
            catch (Exception ex)
            {
                messageBoxError(ex);
            }
        }

        public void saveDataToFile()
        {
            saveCollectionToFile(_teams, typeof(ObservableCollection<Team>), datasFileAddress, "Teams");
        }

        private void saveCollectionToFile(object obj, Type type, string fileAddress, string rootAttributeName)
        {
            XmlRootAttribute rootAttribute = new XmlRootAttribute();
            rootAttribute.ElementName = rootAttributeName;
            rootAttribute.IsNullable = true;

            XmlSerializer serializer = new XmlSerializer(type, rootAttribute);

            StreamWriter streamWriter = null;

            try
            {
                streamWriter = new StreamWriter(fileAddress);
                serializer.Serialize(streamWriter, obj);
                streamWriter.Dispose();
                MessageBox.Show("Zapisano dane do pliku.", "Informacja");
            }
            catch (Exception ex)
            {
                messageBoxError(ex);
            }
        }
        #endregion

        void messageBoxError(Exception ex)
        {
            MessageBox.Show("Aplikacja wygenerowala wyjatek: " + ex, "Blad");
        }

        double convertStringToDouble(string str)
        {
            str = str.Replace(".", ",");
            double i = Convert.ToDouble(str);
            return i;
        }
    }
}