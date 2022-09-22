namespace SoccerModel
{
    class State
    {
        //Each of these doubles represents the probability of moving from the current state to another state
        //eg: ToHomeDefence = 0.5 , 50% chance of changing from this current State to HomeDefence
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
