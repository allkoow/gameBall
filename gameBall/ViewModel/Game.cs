using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameBall.ViewModel
{
    public class Game : IGame
    {
        Player playerA = null;
        Player playerB = null;

        Random random = null;

        enum moraleUpdate { increase, decrease };
        enum playerEnum { playerA, playerB };
        int[] tableOfPoints = { 1, 2, 3, 5 };
        enum constFragment { easy, free, penalty };
        bool flagOfTheEnd = false;

        int sets;

        Game(Player playerA, Player playerB)
        {
            this.playerA = playerA;
            this.playerB = playerB;

            random = new Random();

            // players parameters
            setParametersOfPlayer(playerA);
            setParametersOfPlayer(playerB);

            calculateHitRatioOfPlayer(playerA);
            calculateHitRatioOfPlayer(playerB);
        }

        public void setParametersOfPlayer(Player player)
        {
            player.dayDispostion = 50.0 + random.Next(0, 50);
            player.morale = 80.0 + random.Next(0, 20);
        }

        public void calculateHitRatioOfPlayer(Player player)
        {
            double a = -0.5 / 39.0;
            double b = 1.0 - a;
            double c1 = 0.57, c2 = 0.28, c3 = 0.1, c4 = 0.05;

            player.hitRatio = -player.triedness + c1 * player.tournamentDisposition +
                c2 * player.morale + c3 * (a * player.ranked + b) * 100.0 + c4 * player.concentration;

            if (player.hitRatio > 100.0)
                player.hitRatio = 99.0;

            if (player.hitRatio < 40.0)
                player.hitRatio = 50.0;
        }

        public void updateConcentration(Player pA, Player pB)
        {
            if (pA.pointsInSet - pB.pointsInSet > 15)
            {
                pA.concentration -= 1.0;
                pB.concentration += 1.0;
            }
            else
            {
                pA.concentration += 1.0;
                pB.concentration -= 1.0;
            }    
        }

        public void updateMorale(Player player, int mr)
        {
            if (mr == (int)moraleUpdate.increase)
                player.morale += 1.0;
            else
                player.morale -= 1.0;
        }

        public void checkHit(Player player, Player opponent, int hit)
        {
            if(hit <= (10*player.hitRatio))
            {
                if (player.pointsInSet < 50)
                {
                    player.pointsInSet += tableOfPoints[player.flagOfPoints];
                    player.flagOfPoints += 1;
                    if (player.flagOfPoints == 4) player.flagOfPoints = 0;
                }
                else
                    player.pointsInSet += 1;

                updateMorale(playerA, (int)moraleUpdate.increase);
            }
            else
            {
                updateMorale(playerA, (int)moraleUpdate.decrease);
            }

            updateConcentration(player, opponent);
            player.triedness += (1 - player.dayDispostion * 0.01);
            opponent.triedness += (1 - opponent.dayDispostion * 0.01);
        }

        public void playGame()
        {
            playerEnum en = playerEnum.playerA;
            int hit = 0;

            do
            {
                calculateHitRatioOfPlayer(playerA);
                calculateHitRatioOfPlayer(playerB);

                hit = random.Next(1, 1000);

                if (en == playerEnum.playerA)
                {
                    checkHit(playerA, playerB, hit);
                    en = playerEnum.playerB;
                }
                    
                else
                {
                    checkHit(playerB, playerA, hit);
                    en = playerEnum.playerA;
                }
                    
                endOfTheSet(playerA, playerB);
                endOfTheSet(playerB, playerA);

                if (playerA.sets == 2 || playerB.sets == 2)
                    flagOfTheEnd = true;

            } while (!flagOfTheEnd);

            endOfTheMatch();
        }

        public void constantFragment(Player player)
        {
            double fragmentType = random.Next(1, 100);
            double hit = 0;
            int flagOfConst = 0;

            if (fragmentType >= 0 && fragmentType <= 40)
                flagOfConst = (int)constFragment.easy;
            if (fragmentType > 40 && fragmentType <= 80)
                flagOfConst = (int)constFragment.free;
            if (fragmentType > 80 && fragmentType <= 100)
                flagOfConst = (int)constFragment.penalty;

            hit = random.Next(1, 1000);

            if (hit <= 10 * (player.hitRatio))
            {
                player.pointsInSet += tableOfPoints[flagOfConst];
                updateMorale(player,(int)moraleUpdate.increase);
            }
            else
            {
                updateMorale(player, (int)moraleUpdate.decrease);
            }
        }

        public void endOfTheSet(Player player, Player opponent)
        {
            if(player.pointsInSet >= 51 && (player.pointsInSet-opponent.pointsInSet >=2))
            {
                player.sets += 1;

                player.pointsInSet = 0;
                opponent.pointsInSet = 0;

                updateMorale(player, (int)moraleUpdate.increase);
                updateMorale(opponent, (int)moraleUpdate.decrease);
            }
        }

        public void endOfTheMatch()
        {
            
        }
    }
}
