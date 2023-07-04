using Game_Words_2._2.GameClasses;
using System;
using System.Timers;

namespace Game_Words_2._2.GameHelpers
{
    /// <summary>
    /// Статический класс отвечает за установку таймера и отображение игрового времени.
    /// </summary>
    public static class TimerHandler
    {
        /// <summary>
        /// Поле хранящее таймер.
        /// </summary>
        public static Timer timer;

        /// <summary>
        /// Свойсво устанавливает/возвращает статус таймера (вкл/выкл)
        /// </summary>
        public static bool IsTimerGoing { get; set; }

        /// <summary>
        /// Свойсво устанавливает/возвращает время на ввод слова в секундах.
        /// </summary>
        public static int Seconds { get; set; }


        /// <summary>
        /// Метод позволяет установить таймер на определённое время.
        /// </summary>
        /// <param name="seconds">Принимает секунды.</param>
        public static void SetTimer(int seconds)
        {
            const int MultiplierOfSeconds = 1000;

            Seconds = seconds;
            SecondsDelay = 1;

            timer = new Timer();
            timer.Interval = SecondsDelay * MultiplierOfSeconds;
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
            IsTimerGoing = true;
        }

        /// <summary>
        /// Метод позволяет выклучить таймер.
        /// </summary>
        public static void StopTimer()
        {
            timer.Stop();
            timer.Dispose();
        }

        static double SecondsDelay { get; set; }

        static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            int nowX = Console.CursorLeft;
            int nowY = Console.CursorTop;

            int nextX = 0;
            int nextY = nowY + 2;

            if (Seconds < 1)
            {
                StopTimer();
                IsTimerGoing = false;
                Console.SetCursorPosition(nextX, nextY);
                Console.Write($"\r{Gameplay.GetResourceManager.GetString("StopTime").PadRight(Console.BufferWidth)}");
                Console.SetCursorPosition(nowX, nowY);
            }
            else
            {
                Console.SetCursorPosition(nextX, nextY);
                Console.Write($"\r{Gameplay.GetResourceManager.GetString("Time")}{Seconds.ToString()}".PadRight(Console.BufferWidth));
                Console.SetCursorPosition(nowX, nowY);
                Seconds--;
            }
        }
    }
}
