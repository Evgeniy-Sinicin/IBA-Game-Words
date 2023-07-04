using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Game_Words_2._2.GameClasses;

namespace GameWords.DataAccess
{
    /// <summary>
    /// Статический класс отвечающий за логику работы с файлами.
    /// </summary>
    public static class FileHandler
    {
        /// <summary>
        /// Метод позволяет получить коллекцию игроков из JSON файла по заданному поти.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <returns>Возвращает коллекцию игроков.</returns>
        public static List<Player> GetPlayersFromJsonFile(string path)
        {
            using (var file = new FileStream(path, FileMode.Open))
            {
                var jsonFormatter = new DataContractJsonSerializer(typeof(List<Player>));
                var newPlayers = jsonFormatter.ReadObject(file) as List<Player>;

                return newPlayers;
            }
        }

        /// <summary>
        /// Метод позволяет записать коллекцию игроков в JSON файла по заданному поти.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <param name="players">Коллекция игроков.</param>
        public static void WritePlayersToJsonFile(string path, List<Player> players)
        {
            var jsonFormatter = new DataContractJsonSerializer(typeof(List<Player>));

            using (var file = new FileStream(path, FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(file, players);
            }
        }
    }
}
