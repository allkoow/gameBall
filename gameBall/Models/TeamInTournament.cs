using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameBall
{
    public abstract class TeamInTournament : Team
    {
        private int _matchesPlayed;
        public int matchesPlayed
        {
            get
            {
                _matchesPlayed = _matchesLost + _matchesWon;
                return _matchesPlayed; }
            set
            {
                _matchesPlayed = value;
                OnPropertyChanged(nameof(matchesPlayed));
            }
        }

        private int _matchesWon;
        public int matchesWon
        {
            get { return _matchesWon; }
            set
            {
                _matchesWon = value;
                OnPropertyChanged(nameof(matchesWon));
            }
        }

        private int _matchesLost;
        public int matchesLost
        {
            get { return _matchesLost; }
            set
            {
                _matchesLost = value;
                OnPropertyChanged(nameof(matchesLost));
            }
        }

        private double _matchesRatio;
        public double matchesRatio
        {
            get
            {
                if (_matchesLost > 0)
                    _matchesRatio = (double)_matchesWon / (double)_matchesLost;
                else
                    _matchesRatio = 999.0;

                return _matchesRatio;
            }
            set
            {
                _matchesRatio = value;
                OnPropertyChanged(nameof(matchesRatio));
            }
        }

        private int _setsWon;
        public int setsWon
        {
            get { return _setsWon; }
            set
            {
                _setsWon = value;
                OnPropertyChanged(nameof(setsWon));
            }
        }

        private int _setsLost;
        public int setsLost
        {
            get { return _setsLost; }
            set
            {
                _setsLost = value;
                OnPropertyChanged(nameof(setsLost));
            }
        }

        private double _setsRatio;
        public double setsRatio
        {
            get
            {   
                if (_setsLost > 0)
                    _setsRatio = (double)_setsWon / (double)_setsLost;
                else
                    _setsRatio = 999.0;

                return _setsRatio;
            }
            set
            {
                _setsRatio = value;
                OnPropertyChanged(nameof(setsRatio));
            }
        }

        private int _pointsAchieved;
        public int pointsAchieved
        {
            get { return _pointsAchieved; }
            set
            {
                _pointsAchieved = value;
                OnPropertyChanged(nameof(pointsAchieved));
            }
        }

        private int _pointsLost;
        public int pointsLost
        {
            get { return _pointsLost; }
            set
            {
                _pointsLost = value;
                OnPropertyChanged(nameof(pointsLost));
            }
        }

        private double _pointsRatio;
        public double pointsRatio
        {
            get
            {
                if (_pointsLost > 0)
                    _pointsRatio = (double)pointsAchieved / (double)pointsLost;
                else
                    _pointsRatio = 999.0;

                return _pointsRatio;
            }
            set
            {
                _pointsRatio = value;
                OnPropertyChanged(nameof(pointsRatio));
            }
        }
    }
}
