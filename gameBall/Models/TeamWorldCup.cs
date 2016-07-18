using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameBall
{
    public class TeamWorldCup : TeamInTournament
    {
        private int _pointsInTournament;
        public int pointsInTournament
        {
            get { return _pointsInTournament; }
            set
            {
                _pointsInTournament = value;
                OnPropertyChanged(nameof(pointsInTournament));
            }
        }

        private group _groupWC;
        public group groupWC
        {
            get
            {
                if (ranked <= 5) _groupWC = group.A;
                if (ranked > 5 && ranked <= 10) _groupWC = group.B;
                if (ranked > 10 && ranked <= 15) _groupWC = group.C;
                if (ranked > 15 && ranked <= 20) _groupWC = group.D;

                return _groupWC;
            }
            set
            {
                OnPropertyChanged(nameof(groupWC));
            }
        }
    }
}
