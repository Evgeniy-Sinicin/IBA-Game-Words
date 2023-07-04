namespace Game_Words_2._2.GameClasses
{
    /// <summary>
    /// Класс отвечающий за модель игрового слова.
    /// </summary>
    public class Word
    {
        /// <summary>
        /// Свойсвто хранящее значение слова.
        /// </summary>
        public string Value { get; protected set; }

        /// <summary>
        /// Конструктор позволяющий задать значение слову.
        /// </summary>
        /// <param name="value">Значение.</param>
        public Word(string value)
        {
            Value = value;
        }
    }
}
