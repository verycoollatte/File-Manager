using System;

namespace ManageYourFiles
{
    /// <summary>
    /// Класс программы.
    /// </summary>
    partial class Program 
    {

        /// <summary>
        /// Вывод пронумерованных элементов массива.
        /// </summary>
        /// <param name="array"> Массив. </param>
        /// <param name="number"> Число, с которого начинается нумерация. </param>
        static void ShowArray(string[] array, int number)
        {
            foreach (string element in array)
            {
                Console.WriteLine(number + ". " + element);
                number++;
            }
        }

        /// <summary>
        /// Ожидание нажатия enter, чтобы продолжить.
        /// </summary>
        /// <param name="text"> Текст, выводимый пользователю. </param>
        static void PressEnter(string text)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(text);
            Console.BackgroundColor = ConsoleColor.Black;
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            Console.Clear();
        }

        /// <summary>
        /// Выбор операции пользователя с помощью цифр на клавиатуре.
        /// </summary>
        /// <param name="choice"> Нажатая клавиша. </param>
        /// <param name="maximum"> Максимальное допустимое число, оно же число доступных операций. </param>
        /// <param name="number"> Номер, выбранный пользователем. </param>
        /// <returns> Выбрал ли пользователь число корректно (попал ли в диапазон и нажал ли вообще цифру, а не букву) - true. Иначе - false. </returns>
        static bool KeyCorrect(ConsoleKey choice, int maximum, out int number)
        {
            // Число, которое выбрал пользователь.
            number = -1;
            if ((choice >= ConsoleKey.D0 && choice <= ConsoleKey.D9) || (choice >= ConsoleKey.NumPad0 && choice <= ConsoleKey.NumPad9))
            {
                // Индекс последнего элемента в названии считываемой клавиши.
                int index = choice.ToString().Length - 1;
                number = choice.ToString()[index] - '0';
                if (number > maximum || number < 1)
                {
                    Console.WriteLine("\nНомер команды лежит не в заданном диапазоне. Попробуйте заново.\n");
                    return false;
                }
                return true;
            }
            Console.WriteLine("Некорректно выбран номер команды. Попробуйте заново.");
            return false;
        }
    }
}
