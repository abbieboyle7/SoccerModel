using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerModel
{
    class Match
    {
        public int TeamHomeGoal { get; set; }
        public int TeamAwayGoal { get; set; }
        public double CurrentTime { get; set; }

        public void AddHomeGoal()
        {
            TeamHomeGoal++;

        }

        public void AddAwayGoal()
        {
            TeamAwayGoal++;

        }

        public void IncrementTime()
        {
            CurrentTime += 0.5;
        }

        public bool IsMatchOver()
        {
            return CurrentTime <= 90;
        }


    }
}
