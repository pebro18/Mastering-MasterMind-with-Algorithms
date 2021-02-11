using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterMindConsole
{
    class Bots
    {

        List<Tuple<int, int, int, int>> ListCombinations;

        public void StartUp()
        {
            ListCombinations = new List<Tuple<int, int, int, int>>();
            ListCombinations = GenerateAllCombinations(6);
        }

        public int[] GenerateOutput(List<Tuple<int, int>> _feedBack,int _tries)
        {
            int[] Output = new int[4];

            if (_tries == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    var random = new Random();
                    Output[i] = random.Next(0, 6);
                }

                return Output;
            }

            ListCombinations = MakeNewListFromFeedback();



            return Output;
        }

        List<Tuple<int, int, int, int>> MakeNewListFromFeedback()
        {


            return null;
        }

        List<Tuple<int, int, int, int>> GenerateAllCombinations(int _rangeInt)
        {
            List<Tuple<int, int, int, int>> Combinations = new List<Tuple<int, int, int, int>>();

            // Source https://rextester.com/VKA17045 is a alternatife for itertools of python
            // Uses the Linq standard libary
            var range = Enumerable.Range(0, _rangeInt);
            var result = from number1 in range
                         from number2 in range
                         from number3 in range
                         from number4 in range
                         select new { number1, number2, number3, number4 };
            foreach (var item in result)
            {
                Combinations.Add(new Tuple<int, int, int, int> (item.number1, item.number2,item.number3,item.number4));
            }
            return Combinations;
        }

    }
}
