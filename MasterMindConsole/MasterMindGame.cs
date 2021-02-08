using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MasterMindConsole
{
    class MasterMindGame
    {
        //private bool BotGame;   

        private int[] TheCode;
        private List<int> ListOfNumbers;
        public int[] _inputCode;

        public List<Tuple<int, int>> FeedBack;
        public int Range = 6;
        public int MaxTries = 10;
        public int Tries = 0;
        public int InputLimit = 4;

        static void Main()
        {
            var game = new MasterMindGame();
            game.SetupGame(false);
        }

        void SetupGame(bool IsBot)
        {
            var random = new Random();
            FeedBack = new List<Tuple<int, int>>();
            ListOfNumbers = new List<int>();
            for (int i = 0; i < Range; i++)
            {
                ListOfNumbers.Add(i);
            }

            TheCode = new int[InputLimit];
            for (int i = 0; i < InputLimit; i++)
            {
                int randindex = random.Next(ListOfNumbers.Count);
                TheCode[i] = randindex;
            }

            //  Check if code is assigned
            Console.WriteLine("\n");
            foreach (var item in TheCode)
            {
                Console.WriteLine(item);
            }
            PlayGame();
        }

        void PlayGame()
        {
            while (Tries <= MaxTries)
            {
                _inputCode = GetInput();

                var _feedBack = CheckCode(_inputCode);
                FeedBack.Add(new Tuple<int, int>(_feedBack.Black, _feedBack.White));
                Console.WriteLine("Feedback: " + _feedBack.Black + " : " + _feedBack.White);
                Tries++;
            }
        }

        int[] GetInput()
        {
            // Haalt input van console moet dit zo veranderen naar een versie voor het algoritme
            Console.WriteLine("Voer in jouw code: ");
            int _input = Convert.ToInt32(Console.ReadLine());
            int[] Input = GetIntArray(_input);
            return Input;
        }

        int[] GetIntArray(int num)
        {
            string _numasstring = num.ToString();
            int[] IntArray = new int[_numasstring.Length];
            for (int i = 0; i < _numasstring.Length; i++)
            {
                // krijgt "cannot convert from char to system.readonlyspan char" error as ik het direct van char naar int convert
                // string -> char -> string -> int
                // Yes i know this is stupid
                string temp = _numasstring[i].ToString();
                IntArray[i] = int.Parse(temp);
            }
            return IntArray;
        }

        (int Black, int White) CheckCode(int[] UserCode)
        {
            // Geeft feedback van de input
            // nog niet af omdat nog issues over whites check
            int Black = 0;
            int White = 0;
            List<int> _usedIndex = new List<int>();

            for (int i = 0; i < TheCode.Length; i++)
            {
                for (int j = 0; j < UserCode.Length; j++)
                {
                    if (i == j)
                    {
                        if (TheCode[i] == UserCode[j])
                        {
                            Black++;
                            _usedIndex.Add(j);
                        }
                    }
                    else
                    {
                        if (TheCode[i] == UserCode[j] && _usedIndex.Contains(i))
                        {
                            White++;
                            Console.WriteLine("Index: " + i + " : " + j);
                            Console.WriteLine("Waardes: " + TheCode[i] +" : "+ UserCode[j]);
                        }
                    }
                }
            }
            foreach (var item in _usedIndex)
            {
                Console.WriteLine(item);

            }
            return (Black, White);
        }

    }
}
