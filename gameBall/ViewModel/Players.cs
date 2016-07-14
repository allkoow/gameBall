﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameBall.ViewModel
{
    public class Players : Team
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
