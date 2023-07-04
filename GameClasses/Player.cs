using System.Runtime.Serialization;

namespace Game_Words_2._2.GameClasses
{
    /// <summary>
    /// Класс отвечающий за модель игрока.
    /// </summary>
    [DataContract]
    public class Player
    {
        /// <summary>
        /// Пол игрока
        /// </summary>
        public enum Gender
        {
            Man = 1,
            Woman
        }

        /// <summary>
        /// Свойсвто позволяющее получить пол игрока.
        /// </summary>
        public Gender Sex { get; }

        /// <summary>
        /// Свойсвто позволяющее получить полное имя игрока.
        /// </summary>
        public string GetFullName
        {
            get
            {
                switch (Sex)
                {
                    case Gender.Man:
                        return Gameplay.GetResourceManager.GetString("Mister") + Name;
                    case Gender.Woman:
                        return Gameplay.GetResourceManager.GetString("Missis") + Name;
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// Имя игрока.
        /// </summary>
        [DataMember]
        public string Name { get; protected set; }

        /// <summary>
        /// Количетсво побед игрока.
        /// </summary>
        [DataMember]
        public int CountOfWins { get; set; }

        /// <summary>
        /// Конструктор задаёт игроку пол и имя.
        /// </summary>
        /// <param name="gender">Пол.</param>
        /// <param name="name">Имя.</param>
        public Player (Gender gender, string name)
        {
            Sex = gender;
            Name = name;
            CountOfWins = 0;
        }
    }
}
