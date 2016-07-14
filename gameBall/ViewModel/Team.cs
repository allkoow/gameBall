using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace gameBall.ViewModel
{
    public class Team : TeamStatistics
    {
        private string _name;
        [XmlElement("Name")]
        public string name
        {
            get { return _name; }
            set
            { _name = value;
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
            }
        }

    }
}
