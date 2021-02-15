using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterMindConsole
{
    class Startup
    {
        // Starts up the program
        static void Main()
        {
            // sets up the instances needed
            var _game = new MasterMindGame();
            var _bot = new Bots();
            // starts game
            _game.SetupGame(true, _bot);
        }
    }

    class MasterMindGame
    {
        private Bots BotInstance;

        private bool BotCodeBreaking;

        private List<int> ListOfNumbers;
        public List<Tuple<int, int>> FeedBack;

        private int[] SecretCode;
        private int[] InputCode;

        public int Range = 6;
        public int MaxTries = 1000;
        public int Tries = 0;
        public int InputLimit = 4;

        // prepares all variables and classes for the game
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

            // Prints code
            Console.WriteLine("\n");
            foreach (var item in SecretCode)
            {
                Console.WriteLine(item);
            }

            PlayGame();
        }


        // Main function of the game
        void PlayGame()
        {
            while (Tries <= MaxTries)
            {
                InputCode = GetInput();
                Tuple<int, int> _feedback;
                if (BotCodeBreaking)
                {
                    _feedback = CheckCode(InputCode);
                }
                else
                {
                    _feedback = HumanFeedBack();
                }

                if (_feedback.Item1 == 4) break;
                FeedBack.Add(_feedback);
                Console.WriteLine("Feedback: " + _feedback.Item1 + " : " + _feedback.Item2);
                Tries++;
            }
            EndGame();
        }
        
        // Get input from console of a human or from the bots class
        int[] GetInput()
        {
            int[] Input = new int[4];
            if (BotCodeBreaking)
            {
                Tuple<int, int> _feedback;
                if (FeedBack.Count() == 0)
                {
                    _feedback = (0, 0).ToTuple();
                }
                else
                {
                    _feedback = FeedBack[^1];
                }
                Input = BotInstance.GenerateOutput(_feedback, SecretCode, Tries, 0);
            }
            else
            {
                Console.WriteLine("Voer in jouw code: ");
                int _input = Convert.ToInt32(Console.ReadLine());
                Input = GetIntArray(_input);
            }
            return Input;
        }


        void EndGame()
        {
            Console.WriteLine("Tries: " + Tries);
        }

        int[] GetIntArray(int num)
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

        Tuple<int, int> HumanFeedBack()
        {
            return null;
        }

        // Gives feedback from input
        Tuple<int, int> CheckCode(int[] UserCode)
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

    }
}
