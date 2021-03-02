using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MasterMindConsole
{
    class StaticFunctions
    {
        // Gives feedback from input
        public static Tuple<int, int> CheckCode(int[] UserCode, int[] SecretCode)
        {
            int _black = 0;
            int _white = 0;
            List<int> _usedIndex = new List<int>();

            // checks if the nummer are correct and in the right position and adds the index to a list
            for (int i = 0; i < SecretCode.Length; i++)
            {
                if (SecretCode[i] == UserCode[i])
                {
                    _black++;
                    _usedIndex.Add(i);
                }

            }

            // checks if the nummber is correct and checks if index exist of list
            for (int i = 0; i < SecretCode.Length; i++)
            {
                if (!_usedIndex.Contains(i) && SecretCode.Contains(UserCode[i]))
                {
                    _white++;
                }
            }
            return (_black, _white).ToTuple();
        }

        public static int[] GetIntArray(int num)
        {
            string _numasstring = num.ToString();
            int[] IntArray = new int[_numasstring.Length];
            for (int i = 0; i < _numasstring.Length; i++)
            {
                // i get "cannot convert from char to system.readonlyspan char" error if direcly converted from char
                // string -> char -> string -> int
                // Yes i know this is stupid
                string temp = _numasstring[i].ToString();
                IntArray[i] = int.Parse(temp);
            }
            return IntArray;
        }
    }
}
