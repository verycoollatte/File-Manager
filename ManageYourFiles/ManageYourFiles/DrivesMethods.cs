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
        /// Вывод всех дисков.
        /// </summary>
        static void ShowDrives()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("1. Вернуться к телепорту.");
                DriveInfo[] drives = DriveInfo.GetDrives();
                int counter = 2;
                foreach (DriveInfo drive in drives)
                {
                    Console.WriteLine(counter + ". " + drive.Name);
                    counter++;
                }
                ChooseDrives(drives);
            } catch (UnauthorizedAccessException)
            {
                Console.WriteLine("В доступе отказано. Возвращаемся к телепорту.");
                PressEnter("Press Enter to ого ничего себе в компе забанили.");
                ShowChoosePath();
            } catch (IOException)
            {
                Console.WriteLine("С диском что-то не так что ли..");
                PressEnter("Press Enter to у меня от такой новости диск отсох.");
                ShowChoosePath();
            } catch (Exception)
            {
                Console.WriteLine("Вы че тут ломаете...");
                PressEnter("Press Enter to показать эмоцию 0.0");
                ShowChoosePath();
            }
        }

        /// <summary>
        /// Выбор пользователем диска.
        /// </summary>
        /// <param name="drives"> Массив имеющихся дисков. </param>
        static void ChooseDrives(DriveInfo[] drives)
        {
            // Номер действия, выбранного пользователем.
            int number;
            if (drives.Length + 1 <= 9)
                while (!KeyCorrect(Console.ReadKey().Key, drives.Length + 1, out number)) { }
            else
            {
                Console.WriteLine("Откуда так много, ты, черт(есса)?");
                Console.WriteLine("Отправь мне нужный номер.");
                while (!int.TryParse(Console.ReadLine(), out number) || number <= 0 || number > drives.Length + 1)
                {
                    Console.WriteLine("Введите номер нужного диска или выберите выход в меню.");
                    Console.WriteLine("Напоминаю, что число должно лежать в диапазоне от 1 до " + drives.Length + 1 + " .");
                }
            }
            if (number == 1)
                ShowChoosePath();
            else
            {
                number -= 2;
                Console.WriteLine();
                Console.WriteLine(drives[number].ToString());
                path = drives[number].ToString();
                PressEnter("Press enter");
                DirectoryOptions();
            }
        }
    }
}
