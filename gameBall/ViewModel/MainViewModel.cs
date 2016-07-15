using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace gameBall.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Variables and collection
        private ObservableCollection<Team> _teams = new ObservableCollection<Team>();
        public ObservableCollection<Team> teams
        {
            get { return _teams; }
            set { _teams = value; }
        }

        string fileAddress = "data.xml";

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

        public Game game = null;
        #endregion

        public MainViewModel()
        {
            SaveDataCommand = new RelayCommand(saveDataToFile);
            LoadDataCommand = new RelayCommand(loadDataFromFile);
            AddTeam1Command = new RelayCommand<object>(addTeam1);
            AddTeam2Command = new RelayCommand(addTeam2);
        }

        public void addTeam1(object sender)
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

        public void addTeam2()
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

        #region Commands
        public ICommand LoadDataCommand
        {
            get;
            private set;
        }
        public ICommand SaveDataCommand
        {
            get;
            private set;
        }

        public ICommand AddTeam1Command
        {
            get;
            private set;
        }
        public ICommand AddTeam2Command
        {
            get;
            private set;
        }
        #endregion

        #region File operations
        public void loadDataFromFile()
        {
            try
            {
                XPathDocument pathDocument = new XPathDocument(fileAddress);
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
                //MessageBox.Show("Zaladowano dane.", "Informacja");
            }
            catch (Exception ex)
            {
                messageBoxError(ex);
            }
        }

        public void saveDataToFile()
        {
            XmlRootAttribute rootAttribute = new XmlRootAttribute();
            rootAttribute.ElementName = "Teams";
            rootAttribute.IsNullable = true;

            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Team>), rootAttribute);

            StreamWriter streamWriter = null;

            try
            {
                streamWriter = new StreamWriter(fileAddress);
                serializer.Serialize(streamWriter, _teams);
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
            MessageBox.Show("Aplikacja wygenerowala wyj¹tek: " + ex, "Blad");
        }

        double convertStringToDouble(string str)
        {
            str = str.Replace(".", ",");
            double i = Convert.ToDouble(str);
            return i;
        }
    }
}