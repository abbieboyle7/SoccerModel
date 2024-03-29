﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerModel
{
    class Match
    {
        public List<float> HomeGoals = new List<float>();

        public List<float> AwayGoals = new List<float>();

        public List <int> States = new List<int>();

        public float CurrentTime { get; set; }

       
        public void AddHomeGoal()
        {
            HomeGoals.Add(CurrentTime);

        }

        public void AddAwayGoal()
        {
            AwayGoals.Add(CurrentTime);

        }

        public int GetHomeGoals()
        {
            return HomeGoals.Count;
        }

        public int GetAwayGoals()
        {
            return AwayGoals.Count;
        }

        public int GetHomeFirstHalfGoals()
        {
            int goals = 0;
            foreach(var goal in HomeGoals)
            {
                if (goal <=45)
                {
                    goals++;
                }
            }
            return goals;
        }

        public int GetAwayFirstHalfGoals()
        {
            int goals = 0;
            foreach (var goal in AwayGoals)
            {
                if (goal <= 45)
                {
                    goals++;
                }
            }
            return goals;
        }

        public float[] GetPossession()
        {
            float homePossesion = 0f;
            float awayPossesion = 0f;
            float num_goals = 0f;

            foreach (int state in States)

            {
                if (state <= 2)
                {
                    homePossesion++;
                }
                else if (state >= 3 && state <= 5)
                {
                    awayPossesion++;
                }
                else
                {
                    num_goals++;
                }
            }

            float[] result = {homePossesion/(States.Count-num_goals),awayPossesion/(States.Count-num_goals)};
            return result;
        }

        public void IncrementTime()
        {
            CurrentTime += 0.5f;
        }

        public bool IsMatchOver()
        {
            return CurrentTime <= 90;
        }

        public bool FirstHalfOver()
        {
            return CurrentTime <= 45;
        }


    }
}
