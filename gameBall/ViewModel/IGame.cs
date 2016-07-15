using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameBall.ViewModel
{
    interface IGame
    {
        void setParametersOfPlayer(Player player);
        void calculateHitRatioOfPlayer(Player player);
        void updateConcentration(Player pA, Player pB);
        void updateMorale(Player player, int mr);
        void checkHit(Player pA, Player pB, int hit);
        void playGame();
        void constantFragment(Player player);
        void endOfTheSet(Player pA, Player pB);
        void endOfTheMatch();
    }
}
