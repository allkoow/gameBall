using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace gameBall
{
    public abstract class TeamStatistics
    {
        private double _tournamentDisposition;
        [XmlElement("Disposition")]
        public double tournamentDisposition
        {
            get { return _tournamentDisposition; }
            set { _tournamentDisposition = value; }
        }

        private double _pointsInRanking;
        [XmlElement("Points")]
        public double pointsInRanking
        {
            get { return _pointsInRanking; }
            set { _pointsInRanking = value; }
        }
    }
}
