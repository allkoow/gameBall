using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameBall
{
    interface IGame
    {
        void setParametersOfPlayer(Player player);
        void calculateHitRatioOfPlayer(Player player);
        void updateConcentration();
        void updateMorale(Player player, int mr);
        void checkHit(Player player, Player opponent, int hit);
        void playGame(object sender, DoWorkEventArgs eventArgs);
        void constantFragment(Player player);
        void endOfTheSet(Player pA, Player pB);
        void endOfTheMatch(object sender, RunWorkerCompletedEventArgs eventArgs);
    }
}
