using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace gameBall.ViewModel
{
    public class Game : IGame
    {
        Player _playerA = null;
        Player _playerB = null;

        Random random = null;

        enum moraleUpdate { increase, decrease };
        enum playerEnum { playerA, playerB };
        enum constFragment { easy, free, penalty };

        int[] tableOfPoints = { 1, 2, 3, 5 };

        bool flagOfTheEnd = false;

        public StringBuilder texts
        {
            get;
            set;
        }

        public Game(Player playerA, Player playerB)
        {
            _playerA = playerA;
            _playerB = playerB;

            random = new Random();

            // players parameters
            setParametersOfPlayer(playerA);
            setParametersOfPlayer(playerB);

            calculateHitRatioOfPlayer(playerA);
            calculateHitRatioOfPlayer(playerB);
            
            // string builder for messages
            texts = new StringBuilder();
        }

        public void setParametersOfPlayer(Player player)
        {
            player.dayDispostion = 50.0 + random.Next(0, 50);
            player.morale = 80.0 + random.Next(0, 20);
            player.concentration = 80.0 + random.Next(0, 20); ;
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

        public void updateConcentration()
        {
            if (_playerA.pointsInSet - _playerB.pointsInSet > 10)
            {
                _playerA.concentration -= 1.0;
                _playerB.concentration += 1.0;
            }
            if(_playerB.pointsInSet - _playerA.pointsInSet > 10)
            {
                _playerA.concentration += 1.0;
                _playerB.concentration -= 1.0;
            }

            if (_playerA.concentration < 0.0)
                _playerA.concentration = 0.0;
            if(_playerB.concentration < 0.0)
                _playerB.concentration = 0.0;

            if (_playerA.concentration > 100.0)
                _playerA.concentration = 100.0;
            if (_playerB.concentration > 100.0)
                _playerB.concentration = 100.0;
        }

        public void updateTriedness(Player player)
        {
            player.triedness += (1 - player.dayDispostion * 0.01);

            if (player.triedness < 0.0)
                player.triedness = 0.0;
            if (player.triedness > 100.0)
                player.triedness = 100.0;
        }

        public void updateMorale(Player player, int mr)
        {
            if (mr == (int)moraleUpdate.increase)
                player.morale += 1.0;
            else
                player.morale -= 1.0;

            if (player.morale < 0.0)
                player.morale = 0.0;
            if (player.morale > 100.0)
                player.morale = 100.0;
        }

        public void checkHit(Player player, Player opponent, int hit)
        {
            Thread.Sleep(500);

            if (hit <= (10*player.hitRatio))
            {
                texts.AppendLine("Trafienie!");
                if (player.pointsInSet < 50)
                {
                    player.pointsInSet += tableOfPoints[player.flagOfPoints];
                    player.flagOfPoints += 1;
                    if (player.flagOfPoints == 4) player.flagOfPoints = 0;
                }
                else
                    player.pointsInSet += 1;

                updateMorale(player, (int)moraleUpdate.increase);
            }
            else
            {
                texts.AppendLine("Pudło!");
                constantFragment(opponent);
                updateMorale(player, (int)moraleUpdate.decrease);
            }

            updateConcentration();
            updateTriedness(player);
            updateTriedness(opponent);
        }

        public void playGame(object sender, DoWorkEventArgs e)
        {
            playerEnum whichPlayer = playerEnum.playerA;
            int hit = 0;

            var backgroundWorker = (sender as BackgroundWorker);

            do
            {
                if (backgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                calculateHitRatioOfPlayer(_playerA);
                calculateHitRatioOfPlayer(_playerB);

                hit = random.Next(1, 1000);

                texts.AppendLine(_playerA.name + " [" + _playerA.pointsInSet + "]" + " : " + "[" + _playerB.pointsInSet + "] " + _playerB.name);

                if (whichPlayer == playerEnum.playerA)
                {
                    texts.AppendLine("Przy piłce " + _playerA.name + "...");
                    checkHit(_playerA, _playerB, hit);
                    whichPlayer = playerEnum.playerB;
                }

                else
                {
                    texts.AppendLine("Przy piłce " + _playerB.name + "...");
                    checkHit(_playerB, _playerA, hit);
                    whichPlayer = playerEnum.playerA;
                }

                endOfTheSet(_playerA, _playerB);
                endOfTheSet(_playerB, _playerA);

                if (_playerA.sets == 2 || _playerB.sets == 2)
                    flagOfTheEnd = true;

                backgroundWorker.ReportProgress(0);

            } while (!flagOfTheEnd);
        }

        public void constantFragment(Player player)
        {
            double fragmentType = random.Next(1, 100);
            double hit = 0;
            int flagOfConst = 0;

            if (fragmentType >= 0 && fragmentType <= 40)
            {
                flagOfConst = (int)constFragment.easy;
                texts.AppendLine("Drużyna " + player.name + " wykonuje rzut swobodny.");
            }
                
            if (fragmentType > 40 && fragmentType <= 80)
            {
                flagOfConst = (int)constFragment.free;
                texts.AppendLine("Drużyna " + player.name + " wykonuje rzut wolny.");
            }

            if (fragmentType > 80 && fragmentType <= 100)
            {
                flagOfConst = (int)constFragment.penalty;
                texts.AppendLine("Drużyna " + player.name + " wykonuje rzut karny.");
            }
                
            hit = random.Next(1, 1000);

            if (hit <= 10 * (player.hitRatio))
            {
                texts.AppendLine("Trafienie!");
                player.pointsInSet += tableOfPoints[flagOfConst];
                updateMorale(player,(int)moraleUpdate.increase);
            }
            else
            {
                texts.AppendLine("Pudło!");
                updateMorale(player, (int)moraleUpdate.decrease);
            }
        }

        public void endOfTheSet(Player player, Player opponent)
        {
            if(player.pointsInSet >= 51 && (player.pointsInSet-opponent.pointsInSet >=2))
            {
                player.sets += 1;

                updateMorale(player, (int)moraleUpdate.increase);
                updateMorale(opponent, (int)moraleUpdate.decrease);

                Thread.Sleep(2000);
                texts.AppendLine("Koniec seta. Wygrała drużyna " + player.name + ".");

                player.pointsInSet = 0;
                opponent.pointsInSet = 0;

                // update concentration (random)
                player.concentration += random.Next(-10, 10);
                opponent.concentration += random.Next(-10, 10);
            }
        }

        public void endOfTheMatch(object sender, RunWorkerCompletedEventArgs e)
        {
            var backgroundWorker = (sender as BackgroundWorker);

            if (e.Cancelled)
            {

            }
            else
            {
                texts.AppendLine("Koniec meczu.");
            }
        }
    }
}
