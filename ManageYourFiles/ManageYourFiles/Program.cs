using System;
using System.IO;

namespace ManageYourFiles
{
    /// <summary>
    /// Класс работы программы.
    /// </summary>
    partial class Program
    {
        /// <summary>
        /// Путь пользователя в системе.
        /// </summary>
        public static string path = "";
       
        /// <summary>
        /// Отправляем пользователя в заранее созданную тестовую директорию.
        /// </summary>
        static void TestDirectory() 
        {
            path = @"../../../Test";
        }

        /// <summary>
        /// Пользователь вводит путь сам (или пытается).
        /// </summary>
        static void UserPath()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Что ж, герой, считаешь себя достаточным умным, чтобы ввести " +
                    "абсолютный путь через / ?");
                string inputPath = Console.ReadLine();
                string[] pathSplit = path.Split(@"\", StringSplitOptions.RemoveEmptyEntries);
                path = String.Join(@"/", pathSplit, 0, pathSplit.Length);
                path += @"/";
                if (Directory.Exists(@inputPath))
                {
                    path = inputPath;
                    DirectoryOptions();
                }
                else if (File.Exists(inputPath))
                {
                    Console.WriteLine("Ого! У Вас получилось.");
                    path = inputPath;
                    FileOptions();
                }
                else
                {
                    Console.WriteLine("Ну что с вас взять... Проходимец.\n" +
                        "1. Попробовать еще раз.\n2. Капитулировать.");
                    int number;
                    while (!KeyCorrect(Console.ReadKey().Key, 2, out number)) { }
                    if (number == 1)
                        UserPath();
                    else if (number == 2)
                        ShowChoosePath();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Что Вы натворили здесь?! Идите отсюда.");
                PressEnter("Press Enter to грустно побрести обратно.");
                ShowChoosePath();
            }
        }


        /// <summary>
        /// Выводит местоположение пользователя в системе.
        /// </summary>
        static void Where()
        {
            if (path == @"../../../Test")
            {
                path = path.Trim();
                path = Path.GetFullPath(path);
                string[] pathSplit = path.Split(@"\", StringSplitOptions.RemoveEmptyEntries);
                path = String.Join(@"/", pathSplit, 0, pathSplit.Length);
                path += @"/";
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("Вы здесь: " + path);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }

        /// <summary>
        /// Исходные варианты действий.
        /// </summary>
        static void ShowChoosePath()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(" — На развилине путей-дорог лежит Вещий камень-телепорт, а на нём надпись: \n" +
                "1. Прямо пойдешь — с низов начнешь: выбор диска.\n" +
                "2. Влево завернешь — директорию для тестирования найдешь.\n" +
                "3. Справа лес дремучий: иди куда захочешь.\n" +
                "4. Проснуться.");
            ChoosePath();
        }

        /// <summary>
        /// Обработка выбора пользователя.
        /// </summary>
        static void ChoosePath()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            ConsoleKey pressedKey = Console.ReadKey(true).Key;
            switch (pressedKey)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    ShowDrives();
                    break;

                case ConsoleKey.D2:
                    Console.Clear();
                    TestDirectory();
                    DirectoryOptions();
                    break;

                case ConsoleKey.D3:
                    Console.Clear();
                    UserPath();
                        break;

                case ConsoleKey.D4:
                    Console.Clear();
                    ChapterFour();
                    break;

                default:
                    {
                        Console.WriteLine("Выберите один из доступных вариантов. ");
                        ChoosePath();
                    }
                    break;
            }
        }

        /// <summary>
        /// Делаем шаг назад в пути.
        /// </summary>
        static void GoBack()
        {
            try
            {
                Console.WriteLine(path);
                path = path.Trim();
                Console.WriteLine(path[path.Length - 1]);
                string[] separators = new string[2] { "/", @"\" };
                string [] pathSplit = path.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (pathSplit.Length == 1)
                {
                    ShowDrives();
                }
                else
                {
                    path = String.Join(@"/", pathSplit, 0, pathSplit.Length - 1);
                    path += @"/";
                    DirectoryOptions();
                }
            } catch (Exception)
            {
                Console.WriteLine("Извините, что-то пошло не так. Давайте вернемся к телепорту.");
                PressEnter("Press Enter to согласиться.");
                ShowChoosePath();
            }
        }

        /// <summary>
        /// Точка входа.
        /// </summary>
        /// <param name="args"> Аргументы точки входа. </param>
        static void Main(string[] args)
        {
            ChapterOne();
        }
    }
}
