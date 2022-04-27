using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerModel
{
    class Pricer
    {
        private float HomeWin;
        private float AwayWin;
        private float Draw;
        private float HomeGoals;
        private float AwayGoals;
        private int NumMatches;

        private float maxHomeGoals;
        private float maxAwayGoals;

        private float MinHomeGoals;
        private float MinAwayGoals;

        public void Price(Match[] matches)
        {
            NumMatches = matches.Length;

            foreach(Match match in matches)
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
                HomeGoals += (float) match.TeamHomeGoal;
                AwayGoals += (float) match.TeamAwayGoal;

                if(match.TeamHomeGoal > maxHomeGoals)
                {
                    maxHomeGoals = match.TeamHomeGoal;
                }
                else if(match.TeamHomeGoal < MinHomeGoals)
                {
                    MinHomeGoals = match.TeamHomeGoal;
                }

                if (match.TeamAwayGoal > maxAwayGoals)
                {
                    maxAwayGoals = match.TeamAwayGoal;
                }
                else if(match.TeamAwayGoal < MinAwayGoals)
                {
                    MinAwayGoals = match.TeamAwayGoal;
                }

            }

        }

        private float[] GetWinDrawWinProbabilities()
        {
            return new float[] { HomeWin / NumMatches, Draw / NumMatches, AwayWin / NumMatches };
        }

        private float GetAverageHomeGoals()
        {
            return HomeGoals / NumMatches;
        }

        private float GetAverageAwayGoals()
        {
            return AwayGoals / NumMatches;
        }

        public  String GetResults()
        {
            float[] wdw = GetWinDrawWinProbabilities();
            float AverageHomeGoals = GetAverageHomeGoals();
            float AverageAwayGoals = GetAverageAwayGoals();

            return $"Home: {wdw[0]} Draw: {wdw[1]} Away: {wdw[2]}\nAverage Home Goals: {AverageHomeGoals} " +
                $"\nAverage Away Goals: {AverageAwayGoals} \nMax Home Goals: {maxHomeGoals}\nMax Away Goals: {maxAwayGoals}" +
                $"\nMin Home Goals: {MinHomeGoals}\nMin Away Goals: {MinAwayGoals}";
        }
    }
}
