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
        private State HomeCenter = new State(new float[] { 0.299f, 0.2f, 0.3f, 0f, 0.2f, 0f, 0.001f, 0f });
        private State HomeAttack = new State(new float[] { 0.0f, -1f, 0.2f, 0.4f, 0f, 0f, -1f, 0f });
        private State AwayDefence = new State(new float[] { 0f, 0f, 0.2f, 0.2f, 0.3f, 0.3f, 0f, 0f });
        private State AwayCenter = new State(new float[] { 0f, 0.2f, 0f, 0.299f, 0.2f, 0.3f, 0f, 0.001f });
        private State AwayAttack = new State(new float[] { 0.5f, 0f, 0f, 0.1f, 0.28f, 0.1f, 0f, 0.03f });
        private State HomeGoal = new State(new float[] { 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f });
        private State AwayGoal = new State(new float[] { 0f, 1f, 0f, 0f, 0f, 0f, 0f, 0f });

        private Random NumberGen = new Random();

        public Pricer _Pricer { get;}

        public StateMachine(float expectedHomeGoals, float expectedAwayGoals)
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

            HomeAttack.ToHomeCenter = (-0.0329 * expectedHomeGoals) + 0.4;
            HomeAttack.ToHomeGoal = (0.0329 * expectedHomeGoals);
            HomeCenter.ToHomeAttack = (0.0329 * expectedHomeGoals) + 0.26;
            HomeCenter.ToAwayCenter = (-0.0329 * expectedHomeGoals) + 0.24;


            AdjustPossessionBasedOnDifferenceInStrength(expectedHomeGoals, expectedAwayGoals);

            TrainStateBasedOnInputProbabilities(expectedHomeGoals, expectedAwayGoals, 5000, 20);

            _Pricer = Price(50000, expectedHomeGoals, expectedAwayGoals);
        }

        public Pricer Price(int num_matches, float expectedHomeGoals, float expectedAwayGoals)
        {
            Match[] matches = new Match[num_matches];

            

            for (int i = 0; i < num_matches; i++)
            {
                matches[i] = RunMatch();
            }

            Pricer pricer = new Pricer();

            pricer.Price(matches);


            return pricer;
        }
        

        public void TrainStateBasedOnInputProbabilities(float ExpectedHomeGoals, float ExpectedAwayGoals, int NumMatches, int Iterations)
        {
            Match[] new_matches = new Match[NumMatches];
            var new_pricer = new Pricer();

            for (int i = 0; i < Iterations; i++)
            {
                new_matches = new Match[NumMatches];

                for (int j = 0; j < NumMatches; j++)
                {
                    new_matches[j] = this.RunMatch();

                }

                new_pricer.Price(new_matches);
                this.AdjustModelBasedOnInputProbabilities(ExpectedHomeGoals, ExpectedAwayGoals, new_pricer.AverageHomeGoals(), new_pricer.AverageAwayGoals());


            }

            
        }

        public void AdjustModelBasedOnInputProbabilities(float ExpectedHomeGoals, float ExpectedAwayGoals, float AverageHomeGoals, float AverageAwayGoals)
        {
            float adjustHomeGoalChances = 0.01f * (ExpectedHomeGoals - AverageHomeGoals);
            float adjustAwayGoalChances = 0.01f * (ExpectedAwayGoals - AverageAwayGoals);

            float newHomeAttackToHomeGoal = (float) HomeAttack.ToHomeGoal + adjustHomeGoalChances;
            float newHomeAttackToHomeCenter = (float) HomeAttack.ToHomeCenter - adjustHomeGoalChances;
            float newHomeCenterToHomeAttack = (float)HomeCenter.ToHomeAttack + adjustHomeGoalChances;
            float newHomeCenterToAwayCenter = (float)HomeCenter.ToAwayCenter - adjustHomeGoalChances;

            //Console.WriteLine($"newHomeAttackToHomeCenter: {newHomeAttackToHomeCenter}");
            //Console.WriteLine($"newHomeAttackToHomeGoal:{newHomeAttackToHomeGoal}");
            //Console.WriteLine($"newHomeCenterToAwayCenter: {newHomeCenterToAwayCenter}");
            //Console.WriteLine($"newHomeCenterToHomeAttack: {newHomeCenterToHomeAttack}");

            float newAwayAttackToAwayGoal = (float)AwayAttack.ToAwayGoal + adjustAwayGoalChances;
            float newAwayAttackToAwayCenter = (float)AwayAttack.ToAwayCenter - adjustAwayGoalChances;
            float newAwayCenterToAwayAttack = (float)AwayCenter.ToAwayAttack + adjustAwayGoalChances;
            float newAwayCenterToHomeCenter = (float)AwayCenter.ToHomeCenter - adjustAwayGoalChances;


            if (newHomeAttackToHomeGoal > 0f && newHomeAttackToHomeCenter > 0f && newHomeCenterToHomeAttack > 0f && newHomeCenterToAwayCenter > 0f)
            {
                HomeAttack.ToHomeGoal = newHomeAttackToHomeGoal;
                HomeAttack.ToHomeCenter = newHomeAttackToHomeCenter;
                HomeCenter.ToHomeAttack = newHomeCenterToHomeAttack;
                HomeCenter.ToAwayCenter = newHomeCenterToAwayCenter;
            }



            if (newAwayAttackToAwayGoal > 0f && newAwayAttackToAwayCenter > 0f && newAwayCenterToAwayAttack > 0f && newAwayCenterToHomeCenter > 0f)
            {
                AwayAttack.ToAwayGoal = newAwayAttackToAwayGoal;
                AwayAttack.ToAwayCenter = newAwayAttackToAwayCenter;
                AwayCenter.ToAwayAttack = newAwayCenterToAwayAttack;
                AwayCenter.ToHomeCenter = newAwayCenterToHomeCenter;
            }

        }

        public void AdjustPossessionBasedOnDifferenceInStrength(float expectedHomeGoals, float expectedAwayGoals)
        {
            float differenceInGoals = expectedHomeGoals - expectedAwayGoals;
            if (differenceInGoals > 2.5f)
            {
                differenceInGoals = 2.5f;
            }
            
            if (differenceInGoals < -2.5f)
            {
                differenceInGoals = -2.5f;
            }


            if (HomeCenter.ToHomeCenter + 0.05 * differenceInGoals > 0 && HomeCenter.ToAwayCenter - (0.05 * differenceInGoals) >0)
            {
                HomeCenter.ToHomeCenter += 0.05 * differenceInGoals;
                HomeCenter.ToAwayCenter -= 0.05 * differenceInGoals;
            }

            if (AwayCenter.ToAwayCenter + 0.05 * differenceInGoals > 0 && AwayCenter.ToHomeCenter - 0.05 * differenceInGoals > 0)
            {
                AwayCenter.ToAwayCenter -= 0.05 * differenceInGoals;
                AwayCenter.ToHomeCenter += 0.05 * differenceInGoals;
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
                {
                    CurrentState = HomeDefence;
                    match.States.Add(0);
                }
                    
                else if ((randNum -= CurrentState.ToHomeCenter) < 0)
                {
                    CurrentState = HomeCenter;
                    match.States.Add(1);
                }
                    
                else if ((randNum -= CurrentState.ToHomeAttack) < 0)
                {
                    CurrentState = HomeAttack;
                    match.States.Add(2);
                }
                    
                else if ((randNum -= CurrentState.ToHomeGoal) < 0) {
                    CurrentState = HomeGoal;

                    match.AddHomeGoal();
                    match.States.Add(6);

                }
                    
                else if ((randNum -= CurrentState.ToAwayDefence) < 0)
                {
                    CurrentState = AwayDefence;
                    match.States.Add(3);
                }

                else if ((randNum -= CurrentState.ToAwayCenter) < 0)
                {
                    CurrentState = AwayCenter;
                    match.States.Add(4);

                }

                else if ((randNum -= CurrentState.ToAwayAttack) < 0)
                {
                    CurrentState = AwayAttack;
                    match.States.Add(5);
                }
                    
                
                    


                else if ((randNum -= CurrentState.ToAwayGoal) < 0)
                {
                    match.AddAwayGoal();
                    match.States.Add(7);
                    CurrentState = AwayGoal;
                }
                    
             

            }
            return match;

        }
    }

    class State
    {
        public double ToHomeDefence { get; set; }
        public double ToHomeCenter { get; set; }
        public double ToHomeAttack { get; set; }
        public double ToAwayDefence { get; set; }
        public double ToAwayCenter { get; set; }
        public double ToAwayAttack { get; set; }
        public double ToHomeGoal { get; set; }
        public double ToAwayGoal { get; set; }

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
