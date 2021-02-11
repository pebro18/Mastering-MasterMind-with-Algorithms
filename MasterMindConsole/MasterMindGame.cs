using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterMindConsole
{
    class Startup
    {
        static void Main()
        {
            var _game = new MasterMindGame();
            var _bot = new Bots();
            _game.SetupGame(true, _bot);
        }
    }

    class MasterMindGame
    {
        private Bots BotInstance;
        private bool BotCodeBreaking;

        public List<Tuple<int, int>> FeedBack;
        private List<int> ListOfNumbers;
        private int[] SecretCode;
        private int[] InputCode;

        public int Range = 6;
        public int MaxTries = 10;
        public int Tries = 0;
        public int InputLimit = 4;

        public void SetupGame(bool _IsBotGame, Bots _botinstance = null)
        {
            var random = new Random();
            FeedBack = new List<Tuple<int, int>>();
            ListOfNumbers = new List<int>();
            for (int i = 0; i < Range; i++)
            {
                ListOfNumbers.Add(i);
            }

            SecretCode = new int[InputLimit];
            for (int i = 0; i < InputLimit; i++)
            {
                int randindex = random.Next(ListOfNumbers.Count);
                SecretCode[i] = randindex;
            }
            if (_IsBotGame)
            {
                BotInstance = _botinstance;
                BotInstance.StartUp();
                BotCodeBreaking = _IsBotGame;
            }
            //  Check if code is assigned
            Console.WriteLine("\n");
            foreach (var item in SecretCode)
            {
                Console.WriteLine(item);
            }
            PlayGame();
        }

        void PlayGame()
        {
            while (Tries <= MaxTries)
            {
                InputCode = GetInput();

                var (Black, White) = CheckCode(InputCode);
                FeedBack.Add(new Tuple<int, int>(Black, White));
                Console.WriteLine("Feedback: " + Black + " : " + White);
                Tries++;
            }
            EndGame();
        }
        int[] GetInput()
        {
            // Haalt input van console moet dit zo veranderen naar een versie voor het algoritme
            Console.WriteLine("Voer in jouw code: ");
            int _input = Convert.ToInt32(Console.ReadLine());
            int[] Input = GetIntArray(_input);
            return Input;
        }

        void HumanFeedBack()
        {

        }

        void EndGame()
        {

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
            foreach (var item in _usedIndex)
            {
                Console.WriteLine(item);

            }
            return (_black, _white);
        }

    }
}
