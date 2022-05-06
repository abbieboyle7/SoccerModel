using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerModel
{
    class Pricer
    {
        private int[][] FullTimeGoals;
        public int[][] HalfTimeGoals;
        private float[][] _Possessions;

       

        public void Price(Match[] matches)
        {
            int[][] fullTimeGoals = new int[matches.Length][];
            int[][] halfTimeGoals = new int[matches.Length][];
            float[][] possessions = new float[matches.Length][];

            for (var i = 0; i < fullTimeGoals.Length; i++) {
                fullTimeGoals[i] = new int[2];
                fullTimeGoals[i][0] = matches[i].GetHomeGoals();
                fullTimeGoals[i][1] = matches[i].GetAwayGoals();

                possessions[i] = matches[i].GetPossession();

                halfTimeGoals[i] = new int[2];
                halfTimeGoals[i][0] = matches[i].GetHomeFirstHalfGoals();
                halfTimeGoals[i][1] = matches[i].GetAwayFirstHalfGoals();





               

            }

            FullTimeGoals = fullTimeGoals;
            HalfTimeGoals = halfTimeGoals;
            _Possessions = possessions;




        }

        private float[] GetWinDrawWinProbabilities(int[][] fullTimeGoalsArray)
        {
            int NumMatches = fullTimeGoalsArray.Length;
            float HomeWins = 0, AwayWins = 0, Draws = 0;
            foreach (var matchFullTimeGoals in fullTimeGoalsArray) {
                if (matchFullTimeGoals[0] > matchFullTimeGoals[1])
                    HomeWins++;
                else if (matchFullTimeGoals[1] > matchFullTimeGoals[0])
                    AwayWins++;
                else
                    Draws++;
            }
            return new float[] { HomeWins / NumMatches, Draws / NumMatches, AwayWins / NumMatches };
        }

        public float GetAverageHomeGoals(int[][] fullTimeGoalsArray)
        {
            float HomeGoals = 0;
            foreach (var matchFullTimeGoals in fullTimeGoalsArray)
            {
                HomeGoals += matchFullTimeGoals[0]; 
            }
                return HomeGoals / fullTimeGoalsArray.Length;
        }

        private float GetAverageAwayGoals(int[][] fullTimeGoalsArray)
        {
            float AwayGoals = 0;
            foreach (var matchFullTimeGoals in fullTimeGoalsArray)
            {
                AwayGoals += matchFullTimeGoals[1];
            }
            return AwayGoals / fullTimeGoalsArray.Length;
        }

        private float[] GetAveragePossesion(float[][] Possessions)
        {
            float homePossession = 0f;
            float awayPossession = 0f;

            foreach(float[] PossessionArray in Possessions)
            {
                homePossession += PossessionArray[0];
                awayPossession += PossessionArray[1];
            }

            homePossession /= Possessions.Length;
            awayPossession /= Possessions.Length;

            float[] result = { homePossession, awayPossession };
            return result;
        }

        public float AverageAwayGoals()
        {
            return GetAverageAwayGoals(FullTimeGoals);
        }

        public float AverageHomeGoals()
        {
            return GetAverageHomeGoals(FullTimeGoals);
        }

        private float GetOverTwoPointFiveProbability(int[][] fullTimeGoalsArray)
        {
            float OverTwoPointFive = 0;
            foreach (var matchFullTimeGoals in fullTimeGoalsArray)
            {
                if (matchFullTimeGoals[0]+ matchFullTimeGoals[1] > 2)
                {
                    OverTwoPointFive++;
                }
            }
            return OverTwoPointFive / fullTimeGoalsArray.Length;
        }

        public  String GetResults()
        {

//             float[] wdw = GetWinDrawWinProbabilities();
//             float AverageHomeGoals = GetAverageHomeGoals();
//             float AverageAwayGoals = GetAverageAwayGoals();

//             return $"Home: {wdw[0]} Draw: {wdw[1]} Away: {wdw[2]}\nAverage Home Goals: {AverageHomeGoals} " +
//                 $"\nAverage Away Goals: {AverageAwayGoals} \nMax Home Goals: {maxHomeGoals}\nMax Away Goals: {maxAwayGoals}" +
//                 $"\nMin Home Goals: {MinHomeGoals}\nMin Away Goals: {MinAwayGoals}";
            float[] wdw = GetWinDrawWinProbabilities(FullTimeGoals);
            float[] HalfTimeWdw = GetWinDrawWinProbabilities(HalfTimeGoals);
            float[] AveragePossession = GetAveragePossesion(_Possessions);

            return $@"FT Home: {wdw[0]} Draw: {wdw[1]} Away: {wdw[2]}
HT Home: {HalfTimeWdw[0]} Draw: {HalfTimeWdw[1]} Away: {HalfTimeWdw[2]}

Average Home Possession: {AveragePossession[0]}
Average Away Possession: {AveragePossession[1]}

Over 2.5 Probability: {GetOverTwoPointFiveProbability(FullTimeGoals)}

Average Home Goals: {GetAverageHomeGoals(FullTimeGoals)}
Average Away Goals: {GetAverageAwayGoals(FullTimeGoals)}";
        }
    }
}
