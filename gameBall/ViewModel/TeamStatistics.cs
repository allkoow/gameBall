using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameBall.ViewModel
{
    public abstract class TeamStatistics
    {
        private double _hitRatio;
        public double hitRatio
        {
            get { return _hitRatio; }
            set { _hitRatio = value; }
        }

        private double _dayDisposition;
        public double dayDispostion
        {
            get { return _dayDisposition; }
            set { _dayDisposition = value; }
        }

        private double _tournamentDisposition;
        public double tournamentDisposition
        {
            get { return _tournamentDisposition; }
            set { _tournamentDisposition = value; }
        }

        private double _morale;
        public double morale
        {
            get { return _morale; }
            set { _morale = value; }
        }

        private double _triedness;
        public double triednesse
        {
            get { return _triedness; }
            set { _triedness = value; }
        }

        private double _concentration;
        public double concentration
        {
            get { return _concentration; }
            set { _concentration = value; }
        }

        
    }
}
