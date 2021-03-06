﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameBall
{
    public class Player : Team, IDisposable
    {
        private int _sets;
        public int sets
        {
            get { return _sets; }
            set
            {
                _sets = value;
                OnPropertyChanged(nameof(sets));
            }
        }

        private int _pointsInSet;
        public int pointsInSet
        {
            get { return _pointsInSet; }
            set
            {
                _pointsInSet = value;
                OnPropertyChanged(nameof(pointsInSet));
            }
        }

        private int _pointsInMatch;
        public int pointsInMatch
        {
            get { return _pointsInMatch; }
            set { _pointsInMatch = value; }
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
            set
            {
                _dayDisposition = value;
                OnPropertyChanged(nameof(dayDispostion));
            }
        }

        private double _morale;
        public double morale
        {
            get { return _morale; }
            set
            {
                _morale = value;
                OnPropertyChanged(nameof(morale));
            }
        }

        private double _triedness;
        public double triedness
        {
            get { return _triedness; }
            set
            {
                _triedness = value;
                OnPropertyChanged(nameof(triedness));
            }
        }

        private double _concentration;
        public double concentration
        {
            get { return _concentration; }
            set
            {
                _concentration = value;
                OnPropertyChanged(nameof(concentration));
            }
        }

        public int flagOfPoints;

        public void Dispose()
        {
            
        }
    }
}
