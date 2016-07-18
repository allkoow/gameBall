using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace gameBall
{
    public class Team : TeamStatistics, INotifyPropertyChanged
    {
        private string _name;
        [XmlElement("Name")]
        public string name
        {
            get { return _name; }
            set
            { _name = value;
                OnPropertyChanged(nameof(name));
            }
        }

        private int _ranked;
        [XmlElement("Ranked")]
        public int ranked
        {
            get { return _ranked; }
            set
            {
                _ranked = value;
                OnPropertyChanged(nameof(ranked));
            }
        }

        private string _sym;
        [XmlElement("Symbol")]
        public string sym
        {
            get { return _sym; }
            set
            {
                _sym = value;
                OnPropertyChanged(nameof(sym));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
                
        }
    }
}
