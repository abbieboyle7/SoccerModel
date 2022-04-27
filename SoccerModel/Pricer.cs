using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerModel
{
    class Pricer
    {
        private int HomeWin;
        private int AwayWin;
        private int Draw;

        public void Price(Match match)
        {
            if (match.TeamHomeGoal > match.TeamAwayGoal)
            {

                HomeWin++;

            }
            else if (match.TeamHomeGoal < match.TeamAwayGoal)
            {
                AwayWin++;
            }
            else
            {
                Draw++;
            }
        }

        public override String ToString()
        {
            return $"Home: {HomeWin} Draw: {Draw} Away: {AwayWin}";
        }
    }
}
