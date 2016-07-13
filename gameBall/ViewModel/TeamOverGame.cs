using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameBall.ViewModel
{
    public abstract class TeamOverGame : TeamStatistics
    {
        private int _sets;
        public int sets
        {
            get { return _sets; }
            set { _sets = value; }
        }

        private int _pointsInSet;
        public int pointsInSet
        {
            get { return _pointsInSet; }
            set { _pointsInSet = value; }
        }
    }
}
