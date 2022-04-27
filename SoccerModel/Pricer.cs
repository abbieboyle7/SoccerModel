﻿using System;
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

        

        public void Price(Match[] matches)
        {
            int[][] fullTimeGoals = new int[matches.Length][];
            int[][] halfTimeGoals = new int[matches.Length][];

            for (var i = 0; i < fullTimeGoals.Length; i++) {
                fullTimeGoals[i] = new int[2];
                fullTimeGoals[i][0] = matches[i].GetHomeGoals();
                fullTimeGoals[i][1] = matches[i].GetAwayGoals();

                halfTimeGoals[i] = new int[2];
                halfTimeGoals[i][0] = matches[i].GetHomeFirstHalfGoals();
                halfTimeGoals[i][1] = matches[i].GetAwayFirstHalfGoals();



            }

            FullTimeGoals = fullTimeGoals;
            HalfTimeGoals = halfTimeGoals;




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

        private float GetAverageHomeGoals(int[][] fullTimeGoalsArray)
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
            float[] wdw = GetWinDrawWinProbabilities(FullTimeGoals);
            float[] HalfTimeWdw = GetWinDrawWinProbabilities(HalfTimeGoals);

            return $@"FT Home: {wdw[0]} Draw: {wdw[1]} Away: {wdw[2]}
HT Home: {HalfTimeWdw[0]} Draw: {HalfTimeWdw[1]} Away: {HalfTimeWdw[2]}
Average Home Goals: {GetAverageHomeGoals(FullTimeGoals)}
AverageAwayGoals: {GetAverageAwayGoals(FullTimeGoals)}
Over 2.5 Probability: {GetOverTwoPointFiveProbability(FullTimeGoals)}";
        }
    }
}
