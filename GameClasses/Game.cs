using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Game_Words_2._2.GameClasses
{
    /// <summary>
    /// Класс описывет модель игры.
    /// </summary>
    [DataContract]
    public class Game
    {
        const string SavePath = "statistics.json";

        /// <summary>
        /// Коллекция слов введённых игроками.
        /// </summary>
        public List<Word> Words = new List<Word>();

        /// <summary>
        /// Массив игроков участвующих в игре.
        /// </summary>
        [DataMember]
        public Player[] Players { get; protected set; }

        /// <summary>
        /// Главное слово
        /// </summary>
        public Word MainWord { get; protected set; }

        /// <summary>
        /// Свойство позволяет получить количетсво введённых игроками слов.
        /// </summary>
        public int GetCountOfWords { get { return Words.Count; } }

        /// <summary>
        /// Свойство позволяет получить количетсво игроков.
        /// </summary>
        public int GetCountOfPlayers { get { return Players.Length; } }

        /// <summary>
        /// Свойство позволяет получить путь к файлу с записями о прошлых играх.
        /// </summary>
        public string GetSavePath { get { return SavePath; } }

        /// <summary>
        /// Конструктор игры.
        /// </summary>
        /// <param name="players">Игроки</param>
        /// <param name="mainWord">Главное слово</param>
        public Game(Player[] players, Word mainWord)
        {
            Players = players;
            MainWord = mainWord;
            Words.Add(mainWord);
        }
    }
}
