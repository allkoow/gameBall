using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameBall.ViewModel
{
    interface ITeam
    {
        string getInfo();
        void setDisposition();
        void updateMorale();
    }
}
