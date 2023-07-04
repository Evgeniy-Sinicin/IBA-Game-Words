using Game_Words_2._2.GameHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;

namespace Game_Words_2._2.GameClasses
{
    /// <summary>
    /// Статический класс отвечающий за игровую логику.
    /// </summary>
    public static class Gameplay
    {
        /// <summary>
        /// Количество строк для ввода игрового слова. Требуются для вывода вводимых символов, подсказок и таймера.
        /// </summary>
        public const int NumberOfStringsForEnter = 3;

        /// <summary>
        /// Разделяющая строка. Требуется для отделения зоны для ввода слов и зоны с выводом информации игровых команд.
        /// </summary>
        public const int NumberOfDividingStrings = 1;

        /// <summary>
        /// Свойство хранит в себе количество занятых строк в зоне для вывода информации с помощью игровых команд.
        /// </summary>
        static public int NumberOfStringsForInfoAfterTimeLine { get; set; } = 0;

        /// <summary>
        /// Свойство позволяет установить или получить текущую локализацию.
        /// </summary>
        static public ResourceManager GetResourceManager { get { return rm; } }

        /// <summary>
        /// Метод позволяет отобразить игровое меню.
        /// </summary>
        static public void ShowMenu()
        {
            const int NumberOfItems = 5;

            var selecter = 0;

            while (selecter != NumberOfItems)
            {
                Console.Clear();

                Console.WriteLine("\t\t\t\t\t" + rm.GetString("Tittle") + "\n");
                Console.WriteLine(rm.GetString("Rules") + "\n");
                Console.WriteLine(rm.GetString("Commands") + "\n");
                Console.WriteLine(rm.GetString("ComplexityStatus") + ComplexityStatus + "\n");
                Console.WriteLine(rm.GetString("NumberOfPlayers") + players.Count + "\n");
                Console.WriteLine(rm.GetString("MenuInfo"));

                selecter = SelectMenuItem(GetPointerOfChoice, GetErrorChoice, NumberOfItems);

                switch (selecter)
                {
                    case 1:
                        {
                            Console.Clear();
                            Start();
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            ChangeNumberOfPlayers();
                            break;
                        }
                    case 3:
                        {
                            Console.Clear();
                            ChangeComplexity();
                            break;
                        }
                    case 4:
                        {
                            Console.Clear();
                            ChangeLanguage();
                            break;
                        }
                    case 5:
                        {
                            break;
                        }
                }
            }
        }

        static ResourceManager rm = new ResourceManager(typeof(ENG));

        static List<Player> players = new List<Player>();
        static int DelayBetweenWords { get; set; } = 30;
        static int PlayerNumber { get; set; } = 0;

        static string GetPointerOfChoice { get { return rm.GetString("PointerOfChoice"); } }
        static string GetErrorChoice { get { return rm.GetString("ErrorChoice"); } }

        static string ComplexityStatus
        {
            get
            {
                switch (DelayBetweenWords)
                {
                    case 60:
                        {
                            return rm.GetString("ComplexityStatusEasy");
                        }
                    case 30:
                        {
                            return rm.GetString("ComplexityStatusMedium");
                        }
                    case 10:
                        {
                            return rm.GetString("ComplexityStatusHard");
                        }
                    default: return rm.GetString("ComplexityStatusMedium");
                }
            }
            set { }
        }

        static void ChangeNumberOfPlayers()
        {
            const int MinNumberOfPlayers = 2;
            const int MaxNumberOfPlayers = 9;
            const int NumberOfGenders = 2;

            var numberOfPlayers = 0;

            Console.Clear();
            Console.Write(rm.GetString("NumberOfPlayers"));
            players.Clear();
            numberOfPlayers = SelectMenuItem(rm.GetString("NumberOfPlayers"), rm.GetString("NumberOfPlayersError"), MaxNumberOfPlayers, MinNumberOfPlayers);
            Cursor.ClearString(0, Cursor.Y + 1);

            for (int i = 0; i < numberOfPlayers; i++)
            {
                Console.WriteLine(rm.GetString("PlayerNumber" + (i + 1)) + "\n");
                Console.WriteLine((i + 1) + "/" + numberOfPlayers);
                Console.WriteLine(rm.GetString("GenderMenu"));

                var gender = SelectMenuItem(rm.GetString("PointerOfChoice"), rm.GetString("ErrorChoice"), NumberOfGenders);
                var playerName = String.Empty;
                var isValidationPassed = false;

                Console.WriteLine();
                Cursor.ClearString(0, Cursor.Y);

                do
                {
                    try
                    {
                        Console.Write(rm.GetString("PlayerName"));
                        isValidationPassed = Validation.IsCorrectPlayerName(playerName = Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        isValidationPassed = false;
                        Cursor.WriteAt(0, Cursor.Y, ex.Message);
                        Cursor.Y--;
                    }
                } while (!isValidationPassed);

                players.Add(new Player((Player.Gender)gender, playerName));
            }
        }

        static void ChangeComplexity()
        {
            Console.Clear();
            Console.WriteLine(rm.GetString("ComplexityMenu"));

            const int numberOfComplexities = 3;
            const int easy = 60;
            const int medium = 30;
            const int hard = 10;

            var selecter = SelectMenuItem(rm.GetString("PointerOfChoice"), rm.GetString("ErrorChoice"), numberOfComplexities);

            switch (selecter)
            {
                case 1:
                    {
                        DelayBetweenWords = easy;
                        ComplexityStatus = rm.GetString("ComplexityStatusEasy");
                        break;
                    }
                case 2:
                    {
                        DelayBetweenWords = medium;
                        ComplexityStatus = rm.GetString("ComplexityStatusMedium");
                        break;
                    }
                case 3:
                    {
                        DelayBetweenWords = hard;
                        ComplexityStatus = rm.GetString("ComplexityStatusHard");
                        break;
                    }
            }
        }

        public static void ChangeLanguage()
        {
            const int numberOfItems = 2;

            Console.Write(rm.GetString("LanguageInfo"));

            var selecter = SelectMenuItem(GetPointerOfChoice, GetErrorChoice, numberOfItems);

            switch (selecter)
            {
                case 1:
                    {
                        rm = new ResourceManager(typeof(RU));
                        break;
                    }
                case 2:
                    {
                        rm = new ResourceManager(typeof(ENG));
                        break;
                    }
            }
        }

        static void Start()
        {
            if (players.Count == 0)
            {
                ChangeNumberOfPlayers();
                Console.Clear();
            }

            Console.WriteLine(rm.GetString("CreateGame") + "\n");
            Console.Write(rm.GetString("EnterMainWord"));

            var mainWord = new Word(null);
            var isValidationPassed = false;
            var x = Cursor.X;
            var y = Cursor.Y;

            do
            {
                try
                {
                    mainWord = new Word(Console.ReadLine());
                    isValidationPassed = Validation.IsCorrectMainWord(mainWord.Value);
                }
                catch (Exception ex)
                {
                    isValidationPassed = false;
                    Cursor.X = x;
                    Cursor.Y = y;
                    Cursor.ClearString(x,y);
                    Cursor.ClearString(x, Cursor.Y + 1);
                    Cursor.WriteAt(0, Cursor.Y + 1, ex.Message);
                }
            } while (!isValidationPassed);

            Cursor.ClearString(0, Cursor.Y);
            Console.WriteLine("\n" + rm.GetString("StartGame") + "\n");

            var game = new Game(players.ToArray(), mainWord);
            var word = new Word(null);
            var isGameOn = true;
            PlayerNumber = 0;

            TimerHandler.SetTimer(DelayBetweenWords);
            
            while (isGameOn)
            {
                word = EnterWord(players[PlayerNumber], game);

                if (!TimerHandler.IsTimerGoing)
                {
                    isGameOn = false;
                }

                if (isGameOn)
                {
                    PlayerNumber++;

                    if (PlayerNumber > game.GetCountOfPlayers - 1)
                    {
                        PlayerNumber = 0;
                    }

                    game.Words.Add(word);
                    TimerHandler.Seconds = DelayBetweenWords;
                }
            }

            FinishGame(game);
        }

        static Word EnterWord(Player player, Game game)
        {
            const int EnterKey = 13;
            const int BackSpaceKey = 8;
            const int Slash = 47;
            const int Dash = 45;

            string value = String.Empty;

            var symbal = new Char();
            var isValidationPassed = false;
            var isCommand = false;
            var isEndWord = false;

            while (!isValidationPassed && TimerHandler.IsTimerGoing)
            {
                do
                {
                    Cursor.ClearString(0, Cursor.Y);
                    Console.Write($"\r{player.GetFullName}: {value}");
                    symbal = Console.ReadKey().KeyChar;
                    
                    if (symbal.Equals((char)Slash) || symbal.Equals((char)Dash) || Char.IsLetter(symbal))
                    {
                        value += symbal;
                    }
                    else if (symbal.Equals((char)BackSpaceKey) && value.Length > 0)
                    {
                        value = value.Remove(value.Length - 1);
                    }

                    if (value.StartsWith("/".ToString()))
                    {
                        Command.GetInfo();
                        isCommand = true;
                    }
                    else
                    {
                        ClearPlaceForInfo();
                        isCommand = false;
                    }

                    isEndWord = value.ToLower().Equals(Gameplay.GetResourceManager.GetString("Surrender").ToLower());

                    if (value != null && !isCommand && !isEndWord)
                    {
                        try
                        {
                            isValidationPassed = false;
                            isValidationPassed = Validation.IsCorrectWord(new Word(value), game.Words, game.MainWord);
                            Cursor.ClearString(0, Cursor.Y + 1);
                        }
                        catch (Exception ex)
                        {
                            Cursor.ClearString(0, Cursor.Y + 1);
                            Cursor.WriteAt(0, Cursor.Y + 1, ex.Message);
                        }
                    }
                }
                while (!symbal.Equals((char)EnterKey) && TimerHandler.IsTimerGoing);

                ClearPlaceForInfo();

                if (isCommand)
                {
                    var command = new Command(value);
                    command.DoCommand(game);
                    value = String.Empty;
                    isCommand = false;
                }

                if (isEndWord)
                {
                    TimerHandler.StopTimer();
                    TimerHandler.IsTimerGoing = false;
                }
            }

            return new Word(value);
        }

        static void FinishGame(Game game)
        {
            TimerHandler.StopTimer();
            TimerHandler.IsTimerGoing = false;

            Cursor.X = 0;
            Cursor.Y += NumberOfStringsForEnter + NumberOfDividingStrings;

            Console.WriteLine(rm.GetString("EndGame") + "\n");
            Console.WriteLine($"{rm.GetString("Loser")}{game.Players[PlayerNumber].Name}");
            Console.Write(rm.GetString("Winers"));

            for (int i = 0; i < game.GetCountOfPlayers; i++)
            {
                if (!game.Players[i].Equals(game.Players[PlayerNumber]))
                {
                    game.Players[i].CountOfWins++;
                    Console.Write($"{game.Players[i].Name} ");
                }
            }

            Console.ReadKey();

            FileHandler.Save(game);
        }
        
        static int SelectMenuItem(string pointer, string error, int maxNumberOfItems, int minNumberOfItems = 1)
        {
            var selecter = 0;
            var success = false;

            do
            {
                Console.Write($"\r{pointer}: ");

                try
                {
                    selecter = Int32.Parse(Console.ReadKey().KeyChar.ToString());
                    success = true;
                }
                catch
                {
                    Cursor.WriteAt(0, Cursor.Y + 1, error);
                }

                if (selecter < minNumberOfItems || selecter > maxNumberOfItems)
                {
                    Cursor.WriteAt(0, Cursor.Y + 1, error);
                    success = false;
                }
            }
            while (!success);

            return selecter;
        }

        public static void ClearPlaceForInfo()
        {
            for (int i = 0; i < NumberOfStringsForInfoAfterTimeLine; i++)
            {
                Cursor.ClearString(0, Cursor.Y + NumberOfStringsForEnter + i);
            }

            NumberOfStringsForInfoAfterTimeLine = 0;
        }
    }
}
