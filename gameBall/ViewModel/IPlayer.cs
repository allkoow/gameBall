using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameBall.ViewModel
{
    public interface IPlayer
    {
        string getInfo();
        void setDisposition();
        void updateMorale();
    }
}
