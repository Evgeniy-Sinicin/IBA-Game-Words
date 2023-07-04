using Game_Words_2._2.GameHelpers;
using System.Collections.Generic;
using System.IO;

namespace Game_Words_2._2.GameClasses
{
    /// <summary>
    /// Класс отвечает за логику игровых команд. 
    /// </summary>
    public class Command : Word
    {
        /// <summary>
        /// Конструктор для команды.
        /// </summary>
        /// <param name="value">Принимает значение команды в качестве параметра.</param>
        public Command(string value) : base(value)
        {
        }

        /// <summary>
        /// Метод вывдодит подсказку для игрока о игровых командах.
        /// </summary>
        public static void GetInfo()
        {
            const int NumberOfStrings = 5;
            const int DividingString = 1;

            Gameplay.ClearPlaceForInfo();
            Gameplay.NumberOfStringsForInfoAfterTimeLine = NumberOfStrings + DividingString;

            Cursor.WriteAt(0, Cursor.Y + Gameplay.NumberOfStringsForEnter + DividingString, Gameplay.GetResourceManager.GetString("Commands"));
        }

        /// <summary>
        /// Метод позволяет получить информацию о текущей и прошлых играх.
        /// </summary>
        /// <param name="game">Принимает текущую игру как параметр.</param>
        public void DoCommand(Game game)
        {
            switch (Value)
            {
                case "/show-words":
                    ShowWords(game);
                    break;
                case "/score":
                    ShowScore(game);
                    break;
                case "/total-score":
                    ShowTotalScore(game);
                    break;
                default:
                    Cursor.ClearString(0, Cursor.Y + 1);
                    Cursor.WriteAt(0, Cursor.Y + 1, Gameplay.GetResourceManager.GetString("WrongCommandError"));
                    break;
            }
        }
        
        void ShowTotalScore(Game game)
        {
            Gameplay.ClearPlaceForInfo();

            Gameplay.NumberOfStringsForInfoAfterTimeLine += Gameplay.NumberOfDividingStrings;
            Cursor.WriteAt(0, Cursor.Y + Gameplay.NumberOfStringsForEnter + Gameplay.NumberOfDividingStrings, Gameplay.GetResourceManager.GetString("NumberOfWins"));
            Gameplay.NumberOfStringsForInfoAfterTimeLine++;

            if (File.Exists(game.GetSavePath))
            {
                List<Player> pastPlayers = FileHandler.GetPlayersFromJsonFile(game.GetSavePath);

                for (int i = 0; i < pastPlayers.Count; i++)
                {
                    Cursor.WriteAt(0, Cursor.Y + Gameplay.NumberOfStringsForEnter + Gameplay.NumberOfStringsForInfoAfterTimeLine++, $"\r{pastPlayers[i].Name}: {pastPlayers[i].CountOfWins}");
                }
            }
            else
            {
                for (int i = 0; i < game.Players.Length; i++)
                {
                    Cursor.WriteAt(0, Cursor.Y + Gameplay.NumberOfStringsForEnter + Gameplay.NumberOfStringsForInfoAfterTimeLine++, $"\r{game.Players[i].Name}: {game.Players[i].CountOfWins}");
                }
            }
        }

        void ShowScore(Game game)
        {
            Gameplay.ClearPlaceForInfo();

            Gameplay.NumberOfStringsForInfoAfterTimeLine += Gameplay.NumberOfDividingStrings;
            Cursor.WriteAt(0, Cursor.Y + Gameplay.NumberOfStringsForEnter + Gameplay.NumberOfDividingStrings, Gameplay.GetResourceManager.GetString("NumberOfWins"));
            Gameplay.NumberOfStringsForInfoAfterTimeLine++;

            if (File.Exists(game.GetSavePath))
            {
                List<Player> pastPlayers = FileHandler.GetPlayersFromJsonFile(game.GetSavePath);

                foreach (var player in game.Players)
                {
                    var playerWasFound = false;

                    for (int i = 0; i < pastPlayers.Count; i++)
                    {
                        if (player.Name.ToLower().Equals(pastPlayers[i].Name.ToLower()))
                        {
                            Cursor.WriteAt(0, Cursor.Y + Gameplay.NumberOfStringsForEnter + Gameplay.NumberOfStringsForInfoAfterTimeLine++, $"\r{pastPlayers[i].Name}: {pastPlayers[i].CountOfWins}");
                            playerWasFound = true;
                        }
                    }

                    if (!playerWasFound)
                    {
                        Cursor.WriteAt(0, Cursor.Y + Gameplay.NumberOfStringsForEnter + Gameplay.NumberOfStringsForInfoAfterTimeLine++ , $"\r{player.Name}: {player.CountOfWins}");
                    }
                }
            }
            else
            {
                for (int i = 0; i < game.Players.Length; i++)
                {
                    Cursor.WriteAt(0, Cursor.Y + Gameplay.NumberOfStringsForEnter + Gameplay.NumberOfStringsForInfoAfterTimeLine++, $"\r{game.Players[i].Name}: {game.Players[i].CountOfWins}");
                }
            }
        }

        void ShowWords(Game game)
        {
            const int NumberOfMainWords = 1;

            Gameplay.ClearPlaceForInfo();

            Gameplay.NumberOfStringsForInfoAfterTimeLine += Gameplay.NumberOfDividingStrings;
            Cursor.WriteAt(0, Cursor.Y + Gameplay.NumberOfStringsForEnter + Gameplay.NumberOfDividingStrings, Gameplay.GetResourceManager.GetString("EnteredWords"));
            Gameplay.NumberOfStringsForInfoAfterTimeLine++;

            for (int i = 0; i < game.Words.Count - NumberOfMainWords; i++)
            {
                Cursor.WriteAt(0, Cursor.Y + Gameplay.NumberOfStringsForEnter + Gameplay.NumberOfStringsForInfoAfterTimeLine++, game.Words[i + NumberOfMainWords].Value);
            }
        }
    }
}
