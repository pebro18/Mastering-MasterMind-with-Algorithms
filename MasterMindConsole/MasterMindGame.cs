using System;
using System.Collections;
using System.Collections.Generic;

namespace MasterMindConsole
{
    class MasterMindGame
    {
        private bool BotGame;
        
        private List<int> ListOfNumbers;

        private int[] TheCode;
        public int[] InputCode;
        
        public int Range;
        public int MaxTries;
        public int Tries = 0;
        public int InputLimit = 4;
        public int[,] FeedBack;

        static void Main()
        {
            var game = new MasterMindGame();
            game.SetupGame(false);
        }

        void SetupGame(bool IsBot)
        {
            var random = new Random();

            Console.WriteLine("Vul in hoeveelheid aan range: ");
            Range = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Vul in max pogingen: ");
            MaxTries = Convert.ToInt32(Console.ReadLine());
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
                InputCode = GetInput(); //Console.ReadLine();
                FeedBack.SetValue(CheckCode(InputCode), Tries);

                //Console.WriteLine();
                Tries++;
            }
        }

        int[] GetInput()
        {
            Console.WriteLine("Voer in jouw code: ");
            int input = Convert.ToInt32(Console.ReadLine());
            int[] Input = GetIntArray(input);
            return Input;
        }

        int[] GetIntArray(int num)
        {
            List<int> listOfInts = new List<int>();
            while (num > 0)
            {
                listOfInts.Add(num % 10);
                num /= 10;
            }
            listOfInts.Reverse();
            return listOfInts.ToArray();
        }

        (int Black, int White) CheckCode(int[] UserCode)
        {
            int Black = 0;
            int White = 0;

            for (int i = 0; i < TheCode.Length; i++)
            {
                for (int j = 0; j < UserCode.Length; j++)
                {
                    if (i == j)
                    {
                        if (TheCode[i] == UserCode[j])
                        {
                            Black++;
                        }
                    }
                    else
                    {
                        if (TheCode[i] == UserCode[j])
                        {
                            White++;
                        }
                    }

                }
            }
            return (Black, White);
        }

    }
}
