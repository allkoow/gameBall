using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameBall.ViewModel
{
    public class Team : TeamOverGame
    {
        private string _name;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _ranked;
        public int ranked
        {
            get { return _ranked; }
            set { _ranked = value; }
        }

        private string _sym;
        public string sym
        {
            get { return _sym; }
            set { _sym = value; }
        }
    }
}
