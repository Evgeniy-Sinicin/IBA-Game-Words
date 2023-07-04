using Game_Words_2._2.GameClasses;
using System;
using System.Collections.Generic;

namespace Game_Words_2._2.GameHelpers
{
    /// <summary>
    /// Статический класс отвечает за логику проверки игровых слов.
    /// </summary>
    public static class Validation
    {
        /// <summary>
        /// Метод позволяет проверить правильность длины имени игрока.
        /// </summary>
        /// <param name="name">Имя игрока.</param>
        /// <returns></returns>
        public static bool IsCorrectPlayerName(string name)
        {
            bool result = IsCorrectLength(MinLengthOfPlayerName, MaxLengthOfPlayerName, name);
            return result == true ? true : throw new Exception(Gameplay.GetResourceManager.GetString("PlayerNameError"));
        }

        /// <summary>
        /// Метод для общей проверки главного слова.
        /// </summary>
        /// <param name="str">Значение главного слова.</param>
        /// <returns>Возвращает true, если слово введено корректно.</returns>
        public static bool IsCorrectMainWord(string str)
        {
            bool result;

            if (IsCorrectLength(MinLengthOfMainWord, MaxLengthOfMainWord, str))
                result = true;
            else
            {
                result = false;
                throw new Exception(Gameplay.GetResourceManager.GetString("WrongLongError"));
            }

            if (result == true)
            {
                result = IsCorrectContext(str);

                if (!result)
                    throw new Exception(Gameplay.GetResourceManager.GetString("WrongSymbolError"));
            }

            return result;
        }
        
        /// <summary>
        /// Метод для общей проверки игрового слова.
        /// </summary>
        /// <param name="word">Слово, которое требуется проверить.</param>
        /// <param name="words">Коллекция уже введённых слов.</param>
        /// <param name="mainWord">Главное слово.</param>
        /// <returns></returns>
        public static bool IsCorrectWord(Word word, List<Word> words, Word mainWord)
        {
            bool result;
            string str = word.Value.ToLower();

            if (IsCorrectLength(MinLengthOfWord, MaxLengthOfWord, str))
            {
                result = true;
            }  
            else
            {
                result = false;
                throw new Exception(Gameplay.GetResourceManager.GetString("WrongLongError"));
            }

            if (result)
            {
                result = IsCorrectContext(str);

                if (!result)
                {
                    throw new Exception(Gameplay.GetResourceManager.GetString("WrongSymbolError"));
                }
            }

            if (result)
            {
                result = IsWordMatchMainWord(word, mainWord);

                if (!result)
                {
                    throw new Exception(Gameplay.GetResourceManager.GetString("WrongComplianceError"));
                }  
            }

            if (result)
            {
                result = IsNotRepeated(word, words);

                if (!result)
                {
                    throw new Exception(Gameplay.GetResourceManager.GetString("WrongRepetitionError"));
                } 
            }

            return result;
        }

        const int MinLengthOfWord = 2;
        const int MaxLengthOfWord = 30;
        const int MinLengthOfMainWord = 8;
        const int MaxLengthOfMainWord = 30;
        const int MinLengthOfPlayerName = 1;
        const int MaxLengthOfPlayerName = 30;

        //ПРОВЕРКА ДЛИНЫ СЛОВА
        static bool IsCorrectLength(int minLength, int maxLength, string str)
        {
            if (str.Length >= minLength && str.Length <= maxLength)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // ПРОВЕРКА НА НАЛИЧИЕ В СЛОВЕ НЕДОПУСТИМЫХ СИМВОЛОВ
        static bool IsCorrectContext(string str)
        {
            List<char> chars = new List<char>();

            if (Gameplay.GetResourceManager.BaseName.Equals("RU"))
            {
                chars.AddRange( new char[] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' } );
            }
            else if (Gameplay.GetResourceManager.BaseName.Equals("ENG"))
            {
                chars.AddRange(new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' });
            }

            bool isCorrectSymbol;

            str = str.ToLower();

            for (int i = 0; i < str.Length; i++)
            {
                isCorrectSymbol = false;

                for (int j = 0; j < chars.Count; j++)
                {
                    if (str[i].Equals(chars[j]))
                    {
                        isCorrectSymbol = true;
                    } 
                }

                if (!isCorrectSymbol)
                {
                    return false;
                }   
            }

            return true;
        }

        //ПОДСЧЁТ КОЛ-ВА КАЖДОЙ БУКВЫ АЛФАВИТА В СЛОВЕ
        static int[] GetCountOfEachUniqueChar(string str)
        {
            List<char> chars = new List<char>();
            List<int> countOfChar = new List<int>();

            if (Gameplay.GetResourceManager.BaseName.Equals("RU"))
            {
                chars.AddRange(new char[] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' });
                countOfChar.AddRange(new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            }
            else if (Gameplay.GetResourceManager.BaseName.Equals("ENG"))
            {
                chars.AddRange(new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' });
                countOfChar.AddRange(new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            }

            str = str.ToLower();

            for (int i = 0; i < str.Length; i++)
            {
                for (int j = 0; j < chars.Count; j++)
                {
                    countOfChar[j] += (str[i].Equals(chars[j])) ? 1 : 0;
                }
            }

            return countOfChar.ToArray();
        }

        //ПРОВЕРКА СООТВЕТСТВИЯ СЛОВА ГЛАВНОМУ СЛОВУ
        static bool IsWordMatchMainWord(Word word, Word mainWord)
        {
            bool isCorrect = true;

            string wordValue = word.Value.ToLower();
            string mainWordValue = mainWord.Value.ToLower();

            int[] wordChars = GetCountOfEachUniqueChar(wordValue);
            int[] mainWordChars = GetCountOfEachUniqueChar(mainWordValue);
            int[] difference = new int[mainWordChars.Length];

            for (int i = 0; i < mainWordChars.Length; i++)
            {
                difference[i] = mainWordChars[i] - wordChars[i];
                if (difference[i] < 0)
                {
                    isCorrect = false;
                }
            }

            return isCorrect;
        }

        //ПРОВЕРКА ПОВТОРЕНИЯ СЛОВА
        static bool IsNotRepeated(Word word, List<Word> words)
        {
            bool isNotRepeated = true;

            foreach (Word w in words)
            {
                if (w.Value.ToLower().Equals(word.Value.ToLower()))
                {
                    isNotRepeated = false;
                } 
            }

            return isNotRepeated;
        }
    }
}
