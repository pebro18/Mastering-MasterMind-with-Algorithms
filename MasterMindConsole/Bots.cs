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

        public int[] GenerateOutput(Tuple<int, int> _feedBack, int[] SecretCode, int _tries, int TypeSolution)
        {
            Random rng = new Random();
            int ItemIndex;


            if (_tries == 0)
            {
                ItemIndex = rng.Next(ListCombinations.Count);
                return MakeArrayFromListItem(ListCombinations[ItemIndex]);
            }

            switch (TypeSolution)
            {
                case 0:
                    ListCombinations = ReduceListFromFeedback(_feedBack, SecretCode);
                    ItemIndex = rng.Next(ListCombinations.Count);
                    return MakeArrayFromListItem(ListCombinations[ItemIndex]);
                case 1:
                    return null;

                case 2:
                    return null;
            }

            return null;
        }


        // Simple algorithm method
        List<Tuple<int, int, int, int>> ReduceListFromFeedback(Tuple<int, int> _feedBack, int[] _secretcode)
        {
            List<Tuple<int, int, int, int>> _possibleNewCodes = new List<Tuple<int, int, int, int>>();

            foreach (var PossibleGuess in ListCombinations)
            {
                int[] temparray = MakeArrayFromListItem(PossibleGuess);
                Tuple<int, int> _feedbackcode = CheckCode(temparray, _secretcode);
                if (_feedBack.Item1 == _feedbackcode.Item1 && _feedBack.Item2 == _feedbackcode.Item2)
                {
                    _possibleNewCodes.Add(PossibleGuess);
                }
            }
            return _possibleNewCodes;
        }

        // One step ahead method
        List<Tuple<int, int, int, int>> OneStepAhead(Tuple<int, int> _feedBack, int[] _secretcode)
        {
            foreach (var PossibleGuess in ListCombinations)
            {
                
            }

            return null;
        }

        // Own method
        List<Tuple<int, int, int, int>> OwnMethod(Tuple<int, int> _feedBack, int[] _secretcode)
        {



            return null;
        }


        // Copy of the same check code in the MasterMind class
        // i dont have to deal with annoying inheritance so yeah
        // i could make this into a static function
        Tuple<int, int> CheckCode(int[] UserCode, int[] SecretCode)
        {
            int _black = 0;
            int _white = 0;
            List<int> _usedIndex = new List<int>();

            for (int i = 0; i < SecretCode.Length; i++)
            {
                if (SecretCode[i] == UserCode[i])
                {
                    _black++;
                    _usedIndex.Add(i);
                }

            }
            for (int i = 0; i < SecretCode.Length; i++)
            {
                if (!_usedIndex.Contains(i) && SecretCode.Contains(UserCode[i]))
                {
                    _white++;
                }
            }
            //Console.WriteLine("Black: " + _black + " White: " + _white);
            return (_black, _white).ToTuple();
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
                Combinations.Add(new Tuple<int, int, int, int>(item.number1, item.number2, item.number3, item.number4));
            }
            return Combinations;
        }

        private int[] MakeArrayFromListItem(Tuple<int, int, int, int> ListItem)
        {
            int[] Output = new int[4];
            Output[0] = ListItem.Item1;
            Output[1] = ListItem.Item2;
            Output[1] = ListItem.Item3;
            Output[3] = ListItem.Item4;
            return Output;
        }
    }
}
