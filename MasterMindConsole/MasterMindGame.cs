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
        public int MaxTries = 3;
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
                    _feedback = StaticFunctions.CheckCode(InputCode,SecretCode);
                }
                else
                {
                    _feedback = HumanFeedBack();
                }

                Tries++;
                if (_feedback.Item1 == 4) break;
                FeedBack.Add(_feedback);
                Console.WriteLine("Feedback: " + _feedback.Item1 + " : " + _feedback.Item2);
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
                Input = StaticFunctions.GetIntArray(_input);
            }
            return Input;
        }

        Tuple<int, int> HumanFeedBack()
        {
            Console.WriteLine();
            Console.ReadLine();
            Console.WriteLine();
            Console.ReadLine();
            return null;
        }

        void EndGame()
        {
            Console.WriteLine("Tries: " + Tries);
        }
    }
}
