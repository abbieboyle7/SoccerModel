using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SoccerModel
{
    class Program
    {
        private const int num_matches = 400000;
        
        static void Main(string[] args)
        {
//            Random NumberGen = new Random();
//            Console.WriteLine("Hello World");
            /*

            Match[] matches = new Match[num_matches];

            

            for (int i = 0; i < num_matches; i++)
            {
                matches[i] = new StateMachine().RunMatch();
            }
            */

            float expectedHomeGoals = 2f;
            float expectedAwayGoals = 2f;
            int iterations = 30;

            Console.WriteLine($"Expected Home Goals: {expectedHomeGoals}");
            Console.WriteLine($"Expected Away Goals: {expectedAwayGoals}");

            StateMachine stateMachine = new StateMachine();

            stateMachine.TrainStateBasedOnInputProbabilities(expectedHomeGoals, expectedAwayGoals, num_matches, iterations);

            

            Console.Read();
            
          
        


    }
    }

   
}
