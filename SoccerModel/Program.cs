using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SoccerModel
{
    class Program
    {
        private const int num_matches = 50000;
        
        static void Main(string[] args)
        {


            //            Random NumberGen = new Random();
            //            Console.WriteLine("Hello World");
            float expectedHomeGoals = 3f;
            float expectedAwayGoals = 2f;
            

            Console.WriteLine($"\nExpected Home Goals: {expectedHomeGoals}");
            Console.WriteLine($"Expected Away Goals: {expectedAwayGoals}");

            Match[] matches = new Match[num_matches];

            StateMachine stateMachine1 = new StateMachine(expectedHomeGoals, expectedAwayGoals);

            for (int i = 0; i < num_matches; i++)
            {
                matches[i] = stateMachine1.RunMatch();
            }

            Pricer pricer = new Pricer();

            pricer.Price(matches);

            Console.WriteLine(pricer.GetResults());





            /*
            int iterations = 5;

            StateMachine stateMachine = new StateMachine(expectedHomeGoals,expectedAwayGoals);



            stateMachine.TrainStateBasedOnInputProbabilities(expectedHomeGoals, expectedAwayGoals, num_matches, iterations);


            
            for (int i = 0; i < num_matches; i++)
            {
                matches[i] = stateMachine.RunMatch();
            }

            pricer.Price(matches);
            */


            Console.WriteLine($"\nProbablity for 0 - 0: {(int)(pricer.PriceExactScore(0f, 0f) * 100f)}%");
            Console.WriteLine($"Probablity for 1 - 0: {(int)(pricer.PriceExactScore(1f, 0f) * 100f)}%");
            Console.WriteLine($"Probablity for 0 - 1: {(int)(pricer.PriceExactScore(0f, 1f) * 100f)}%");
            Console.WriteLine($"Probablity for 1 - 1: {(int)(pricer.PriceExactScore(1f, 1f) * 100f)}%");
            Console.WriteLine($"Probablity for 2 - 1: {(int)(pricer.PriceExactScore(2f, 1f) * 100f)}%");
            Console.WriteLine($"Probablity for 1 - 2: {(int)(pricer.PriceExactScore(1f, 2f) * 100f)}%");
            Console.WriteLine($"Probablity for 2 - 2: {(int)(pricer.PriceExactScore(2f, 2f) * 100f)}%");
            Console.WriteLine($"Probablity for 4 - 0: {(int)(pricer.PriceExactScore(4f, 0f) * 100f)}%");
            Console.WriteLine($"Probablity for 0 - 4: {(pricer.PriceExactScore(0f, 4f) * 100f)}%");
            Console.WriteLine($"Probablity for 6 - 1: {(pricer.PriceExactScore(6f, 1f) * 100f)}%");

            Console.Read();
           




        }
    }

   
}
