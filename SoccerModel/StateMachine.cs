using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerModel
{
    class StateMachine
    {
        private State CurrentState; 
        private State HomeDefence = new State(0,0.5,0.3,0,0,0.2,0,0);
        private State HomeCenter = new State(0.299,0,0.4,0,0.3,0,0.001,0);
        private State HomeAttack = new State(0.1,0.3,0,0.5,0,0,0.1,0);
        private State AwayDefence = new State(0,0,0.3,0,0.4,0.3,0,0);
        private State AwayCenter = new State(0,0.4,0,0.299,0,0.3,0,0.001);
        private State AwayAttack = new State(0.6,0,0,0.1,0.2,0,0,0.1);
        private State HomeGoal = new State(0,0,0,0,1,0,0,0);
        private State AwayGoal = new State(0,1,0,0,0,0,0,0);
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

        public void RunMatch()
        {
            while(MatchTime <= 90)
            {
                MatchTime += 0.5;
                double randNum = NumberGen.NextDouble();

                if((randNum -= CurrentState.ToHomeDefence) < 0)
                    CurrentState = HomeDefence;
                else if ((randNum -= CurrentState.ToHomeCenter) < 0)
                    CurrentState = HomeCenter;
                else if ((randNum -= CurrentState.ToHomeAttack) < 0)
                    CurrentState = HomeAttack;
                else if ((randNum -= CurrentState.ToHomeGoal) < 0) {
                    CurrentState = HomeGoal;
                    Console.WriteLine("Home Goal!");
                }
                    
                else if ((randNum -= CurrentState.ToAwayDefence) < 0)
                    CurrentState = AwayDefence;
                else if ((randNum -= CurrentState.ToAwayAttack) < 0)
                    CurrentState = AwayAttack;
                else if ((randNum -= CurrentState.ToAwayCenter) < 0)
                    CurrentState = AwayCenter;
                else if ((randNum -= CurrentState.ToAwayGoal) < 0)
                {
                    Console.WriteLine("Away Goal!");
                    CurrentState = AwayGoal;
                }
                    
             

            }

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

        public State(double toHomeDefence, double toHomeCenter, double toHomeAttack, double toAwayDefence,
            double toAwayCenter, double toAwayAttack, double toHomeGoal, double toAwayGoal)
        {
            this.ToHomeDefence = toHomeDefence;
            this.ToHomeCenter = toHomeCenter;
            this.ToHomeAttack = toHomeAttack;
            this.ToAwayDefence = toAwayDefence;
            this.ToAwayCenter = toAwayCenter;
            this.ToAwayAttack = toAwayAttack;
            this.ToHomeGoal = toHomeGoal;
            this.ToAwayGoal = toAwayGoal;

            
        }
    }
}
