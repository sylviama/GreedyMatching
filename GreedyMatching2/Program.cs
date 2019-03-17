using System;
using System.Collections.Generic;

namespace GreedyMatching2
{
    class Program
    {
        static void Main(string[] args)
        {
            string s1 = "all is well";
            string s2 = "ell that en";
            string s3 = "hat end";
            string s4 = "t ends well";

            List<string> inputStringList = new List<string> { s1, s2, s3, s4 };

            //act
            GreedyMatchClass greedyMatch = new GreedyMatchClass();
            string actual = greedyMatch.GreedyMatching(inputStringList);

            Console.WriteLine(actual);
            Console.ReadKey();
        }
    }
}
