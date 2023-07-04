using System;

namespace Game_Words_2._2.GameHelpers
{
    /// <summary>
    /// Статический класс отвечает за работу с курсором.
    /// </summary>
    public static class Cursor
    {
        /// <summary>
        /// Свойство задаёт или возвращает положение курсора в строке.
        /// </summary>
        public static int X { get { return Console.CursorLeft; } set { Console.CursorLeft = value; } }

        /// <summary>
        /// Свойство задаёт или возвращает положение курсора в столбце строк.
        /// </summary>
        public static int Y { get { return Console.CursorTop; } set { Console.CursorTop = value; } }

        /// <summary>
        /// Метод позволяет сместить курсор, сделать запись и вернуть курсор на прежнее место.
        /// </summary>
        /// <param name="x">Номер символа.</param>
        /// <param name="y">Номер строки.</param>
        /// <param name="str">Желаемая запись.</param>
        public static void WriteAt(int x, int y, string str)
        {
            var cursorX = X;
            var cursorY = Y;

            Console.CursorLeft = x;
            Console.CursorTop = y;
            Console.Write(str);
            Console.CursorLeft = cursorX;
            Console.CursorTop = cursorY;
        }

        /// <summary>
        /// Метод позволяет очистить строку с нужного символа.
        /// </summary>
        /// <param name="x">Номер символа.</param>
        /// <param name="y">Номер строки.</param>
        public static void ClearString(int x, int y)
        {
            var cursorX = X;
            var cursorY = Y;

            Console.CursorLeft = x;
            Console.CursorTop = y;
            Console.Write(new string(' ', Console.WindowWidth));
            Console.CursorLeft = cursorX;
            Console.CursorTop = cursorY;
        }
    }
}
