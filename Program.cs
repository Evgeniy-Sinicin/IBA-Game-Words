using Game_Words_2._2.GameClasses;
using System;
using System.Text;

namespace Game_Words_2._2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.Unicode;

            Gameplay.ChangeLanguage();
            Gameplay.ShowMenu();
        }
    }
}