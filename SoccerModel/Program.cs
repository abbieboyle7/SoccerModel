using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SoccerModel
{
    class Program
    {
        
        static void Main(string[] args)
        {
            float expectedHomeGoals = 2.4f;
            float expectedAwayGoals = 1.2f;
            

            Console.WriteLine($"\nExpected Home Goals: {expectedHomeGoals}");
            Console.WriteLine($"Expected Away Goals: {expectedAwayGoals}");

            StateMachine stateMachine1 = new StateMachine(expectedHomeGoals, expectedAwayGoals);

            Console.WriteLine(stateMachine1._Pricer.GetResults());


            Console.WriteLine($"\nProbablity for 0 - 0: {(int)(stateMachine1._Pricer.PriceExactScore(0f, 0f) * 100f)}%");
            Console.WriteLine($"Probablity for 1 - 0: {(int)(stateMachine1._Pricer.PriceExactScore(1f, 0f) * 100f)}%");
            Console.WriteLine($"Probablity for 0 - 1: {(int)(stateMachine1._Pricer.PriceExactScore(0f, 1f) * 100f)}%");
            Console.WriteLine($"Probablity for 1 - 1: {(int)(stateMachine1._Pricer.PriceExactScore(1f, 1f) * 100f)}%");
            Console.WriteLine($"Probablity for 2 - 1: {(int)(stateMachine1._Pricer.PriceExactScore(2f, 1f) * 100f)}%");
            Console.WriteLine($"Probablity for 1 - 2: {(int)(stateMachine1._Pricer.PriceExactScore(1f, 2f) * 100f)}%");
            Console.WriteLine($"Probablity for 2 - 2: {(int)(stateMachine1._Pricer.PriceExactScore(2f, 2f) * 100f)}%");
            Console.WriteLine($"Probablity for 4 - 0: {(int)(stateMachine1._Pricer.PriceExactScore(4f, 0f) * 100f)}%");
            Console.WriteLine($"Probablity for 0 - 4: {(stateMachine1._Pricer.PriceExactScore(0f, 4f) * 100f)}%");
            Console.WriteLine($"Probablity for 6 - 1: {(stateMachine1._Pricer.PriceExactScore(6f, 1f) * 100f)}%");

            Console.Read();
           




        }
    }

   
}
