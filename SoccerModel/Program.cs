using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SoccerModel
{
    class Program
    {
        private const int num_matches = 40000;
        
        static void Main(string[] args)
        {
//            Random NumberGen = new Random();
//            Console.WriteLine("Hello World");
            Match[] matches = new Match[num_matches];

            for (int i = 0; i < num_matches; i++)
            {
                matches[i] = new StateMachine().RunMatch();

            }
            var pricer = new Pricer();
            pricer.Price(matches);
            Console.WriteLine(pricer.GetResults());

//            Console.WriteLine(NumberGen.NextDouble());
            Console.Read();
            
          
        


    }
    }

   
}
