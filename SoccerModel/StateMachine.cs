using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerModel
{

    /*
    private State HomeDefence = new State(0.2, 0.3, 0.3, 0, 0, 0.2, 0, 0);
    private State HomeCenter = new State(0.299, 0.1, 0.3, 0, 0.3, 0, 0.001, 0);
    private State HomeAttack = new State(0.1, 0.36, 0.2, 0.3, 0, 0, 0.04, 0);
    private State AwayDefence = new State(0, 0, 0.3, 0.1, 0.3, 0.3, 0, 0);
    private State AwayCenter = new State(0, 0.3, 0, 0.299, 0.1, 0.3, 0, 0.001);
    private State AwayAttack = new State(0.6, 0, 0, 0.1, 0.18, 0.1, 0, 0.02);
    private State HomeGoal = new State(0, 0, 0, 0, 1, 0, 0, 0);
    private State AwayGoal = new State(0, 1, 0, 0, 0, 0, 0, 0);

    Home wins: 65%
    Draw: 20%
    Away wins: 15%

    Average home goals: 1.4
    Average away goals: 0.4

    Max Home Goals: 5
    Max Away Goals: 3
    Min Home Goals: 0
    Min Away Goals: 0

    */

    class StateMachine
    {
        private State CurrentState;

        private float[][] stateTransitionProbabilities = new float[8][];

        /*            {
                    { 0.2f, 0.3f, 0.3f, 0f, 0f, 0.2f, 0f, 0f },
                    { 0.299f, 0.1f, 0.3f, 0f, 0.3f, 0f, 0.001f, 0f},
                    { 0.1f, 0.36f, 0.2f, 0.3f, 0f, 0f, 0.04f, 0f},
                    { 0f, 0f, 0.3f, 0.1f, 0.3f, 0.3f, 0f, 0f},
                    { 0f, 0.3f, 0f, 0.299f, 0.1f, 0.3f, 0f, 0.001f},
                    { 0.6f, 0f, 0f, 0.1f, 0.18f, 0.1f, 0f, 0.02f},
                    { 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f},
                    { 0f, 1f, 0f, 0f, 0f, 0f, 0f, 0f},
                    };
        */
        private State HomeDefence = new State(new float[] { 0.2f, 0.3f, 0.3f, 0f, 0f, 0.2f, 0f, 0f });
        private State HomeCenter = new State(new float[] { 0.299f, 0.1f, 0.3f, 0f, 0.3f, 0f, 0.001f, 0f });
        private State HomeAttack = new State(new float[] { 0.1f, 0.36f, 0.2f, 0.3f, 0f, 0f, 0.04f, 0f });
        private State AwayDefence = new State(new float[] { 0f, 0f, 0.3f, 0.1f, 0.3f, 0.3f, 0f, 0f });
        private State AwayCenter = new State(new float[] { 0f, 0.3f, 0f, 0.299f, 0.1f, 0.3f, 0f, 0.001f });
        private State AwayAttack = new State(new float[] { 0.6f, 0f, 0f, 0.1f, 0.18f, 0.1f, 0f, 0.02f });
        private State HomeGoal = new State(new float[] { 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f });
        private State AwayGoal = new State(new float[] { 0f, 1f, 0f, 0f, 0f, 0f, 0f, 0f });


        private double MatchTime = 0d;

        private Random NumberGen = new Random();

        public StateMachine()
        {
            double StartNum = NumberGen.NextDouble();

           if(StartNum > 0.5)
            {
                CurrentState = HomeCenter;
            }
            else
            {
                CurrentState = AwayCenter;
            }
        }

        public Match RunMatch()
        {
            Match match = new Match();

            while(match.IsMatchOver())
            {
                match.IncrementTime();
                double randNum = NumberGen.NextDouble();

                if((randNum -= CurrentState.ToHomeDefence) < 0)
                    CurrentState = HomeDefence;
                else if ((randNum -= CurrentState.ToHomeCenter) < 0)
                    CurrentState = HomeCenter;
                else if ((randNum -= CurrentState.ToHomeAttack) < 0)
                    CurrentState = HomeAttack;
                else if ((randNum -= CurrentState.ToHomeGoal) < 0) {
                    CurrentState = HomeGoal;

                    match.AddHomeGoal();

                }
                    
                else if ((randNum -= CurrentState.ToAwayDefence) < 0)
                    CurrentState = AwayDefence;
                else if ((randNum -= CurrentState.ToAwayAttack) < 0)
                    CurrentState = AwayAttack;
                else if ((randNum -= CurrentState.ToAwayCenter) < 0)
                    CurrentState = AwayCenter;
                else if ((randNum -= CurrentState.ToAwayGoal) < 0)
                {
                    match.AddAwayGoal();
                    CurrentState = AwayGoal;
                }
                    
             

            }
            return match;

        }
    }

    class State
    {
        public double ToHomeDefence { get; }
        public double ToHomeCenter { get; }
        public double ToHomeAttack { get; }
        public double ToAwayDefence { get; }
        public double ToAwayCenter { get; }
        public double ToAwayAttack { get; }
        public double ToHomeGoal { get; }
        public double ToAwayGoal { get; }

        public State(float[] stateTransitionProbabilities)
        {
            this.ToHomeDefence = stateTransitionProbabilities[0];
            this.ToHomeCenter = stateTransitionProbabilities[1];
            this.ToHomeAttack = stateTransitionProbabilities[2];
            this.ToAwayDefence = stateTransitionProbabilities[3];
            this.ToAwayCenter = stateTransitionProbabilities[4];
            this.ToAwayAttack = stateTransitionProbabilities[5];
            this.ToHomeGoal = stateTransitionProbabilities[6];
            this.ToAwayGoal = stateTransitionProbabilities[7];

            
        }
    }


}
