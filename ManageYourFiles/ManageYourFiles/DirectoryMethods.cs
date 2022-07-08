using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ManageYourFiles
{
    /// <summary>
    /// Классы программы.
    /// </summary>
    partial class Program
    {
        /// <summary>
        /// Совпадает ли файл с заданной маской.
        /// </summary>
        /// <returns> Массив файлов, совпадающих с маской </returns>
        static string[] MaskMatch()
        {
            string[] allFiles = Directory.GetFiles(path, "", SearchOption.AllDirectories);
            string[] files = new string[0];
            Console.WriteLine("\nВведите маску в стиле «регулярки шарпов».\nПодробнее: " +
                    "https://docs.microsoft.com/en-us/dotnet/standard/base-types/" +
                    "regular-expression-language-quick-reference");
            string mask = Console.ReadLine();
            foreach (string fileName in allFiles)
            {
                if (Regex.IsMatch(fileName, @mask))
                {
                    Array.Resize(ref files, files.Length + 1);
                    files[files.Length - 1] = fileName;

                }
            }
            return files;
        }

        /// <summary>
        /// Копировать файлы директорий и поддиректорий.
        /// </summary>
        static void CopyAllFiles()
        {
            try
            {
                string[] allFiles = Directory.GetFiles(path, "", SearchOption.AllDirectories);
                string[] files = MaskMatch();
                Console.WriteLine("Введите абсолютный адрес директории, куда вы хотите скопировать (через /). ");
                string pathCopy = Console.ReadLine();
                if (!pathCopy.EndsWith(@"/"))
                    pathCopy += @"/";
                Directory.CreateDirectory(@pathCopy);
                string[] pathSplit = path.Split(@"/", StringSplitOptions.RemoveEmptyEntries);
                foreach (string file in files)
                {
                    string[] pathFile = file.Split(@"/", StringSplitOptions.RemoveEmptyEntries);
                    string wayToFile = String.Join(@"/", pathFile, pathSplit.Length, pathFile.Length - pathSplit.Length - 1);
                    string newFile = String.Join(@"/", pathFile, pathSplit.Length, pathFile.Length - pathSplit.Length);
                    Directory.CreateDirectory(pathCopy + wayToFile);
                    newFile = pathCopy + newFile;
                    if (OverwriteFile(newFile)) File.Copy(file, newFile, true);
                    else File.Copy(file, newFile, false);
                }
                DirectoryOptions();
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("У вас нет доступа.");
                PressEnter("Press enter to грустно уйти.");
                DirectoryOptions();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Скорее всего что-то не так с путем.");
                Console.WriteLine("Ошибка: " + exception.Message);
                PressEnter("Press enter to грустно уйти.");
                DirectoryOptions();
            }
        }

        static void PathInput(out string pathOne, out string pathTwo)
        {
            pathOne = "";
            pathTwo = "";
            try
            {
                Console.WriteLine("Неправильный формат ввода сейчас может привести к ошибке в будущем.");
                Console.WriteLine("\nВведите абсолютный путь к первому файлу-слагаемому (включая расширение, через /). ");
                pathOne = Console.ReadLine();
                    while (!File.Exists(pathOne) || Path.GetExtension(pathOne) != ".txt")
                {
                    Console.WriteLine("Файл должен существовать и быть текстовым. Попробуйте ввести путь еще раз.");
                    pathOne = Console.ReadLine();
                }
                Console.WriteLine("Введите абсолютный путь к второму файлу-слагаемому (включая расширение, через /).");
                pathTwo = Console.ReadLine();
                while (!File.Exists(pathTwo) || Path.GetExtension(pathTwo) != ".txt")
                {
                    Console.WriteLine("Файл должен существовать и быть текстовым. Попробуйте ввести путь еще раз.");
                    pathTwo = Console.ReadLine();
                }
            } catch (Exception exception)
            {
                Console.WriteLine("Ошибка: " + exception.Message);
                PressEnter("Press enter to грустно уйти.");
                DirectoryOptions();
            }
        }

        /// <summary>
        /// Сложение текстовых файлов.
        /// </summary>
        static void FilesSummarize()
        {
            try
            {
                PathInput(out string pathOne, out string pathTwo);
                string[] file1 = File.ReadAllLines(@pathOne);
                string[] file2 = File.ReadAllLines(@pathTwo);
                Console.WriteLine("Введите название для файла-суммы, без расширения.");
                string nameInput = Console.ReadLine();
                string name = path + @"\" + nameInput + ".txt";
                while (File.Exists(name))
                {
                    Console.WriteLine("Такой файл уже существует. Введите другое имя.");
                    nameInput = Console.ReadLine();
                    name = path + @"\" + nameInput + ".txt";

                }
                using (StreamWriter writer = File.CreateText(name))
                {
                    foreach (string words in file1)
                        writer.WriteLine(words);
                    foreach (string words in file2)
                        writer.WriteLine(words);
                }
                PressEnter("Press enter to show your hapiness for con-cat-^_^-enation suceed.");
                DirectoryOptions();
            } catch (Exception exception)
            {
                Console.WriteLine("Извините, что-то сломалось... " + exception.Message);
                Console.WriteLine("Убедитесь, пожалуйста," +
                    " что все пути прописаны верно, а у программы везде есть необходимый доступ.");
                PressEnter("Press Enter to попробовать заново или сделать что-то еще.");
                DirectoryOptions();
            }
        }

        /// <summary>
        /// Вывод директорий по указанному путю.
        /// </summary>
        static void ShowDirectories()
        {
            try
            {
                Console.Clear();
                if (Directory.Exists(path))
                {
                    string[] directories = Directory.GetDirectories(path);
                    Console.WriteLine("Вы просматриваете директории, расположенные по адресу: " + path);
                    Console.WriteLine("1. Вернуться к выбору действия.");
                    ShowArray(directories, 2);
                    if (directories.Length == 0)
                        Console.WriteLine("Дирррректоррии не найдены.");
                    // Номер действия, выбранного пользователем.
                    int number;
                    if (directories.Length + 1 <= 9)
                        while (!KeyCorrect(Console.ReadKey().Key, directories.Length + 1, out number)) { }
                    else while (!int.TryParse(Console.ReadLine(), out number) || number < 1
                            || number > directories.Length + 1)
                            Console.WriteLine("Введите номер нужной директории или выберите выход в меню.");
                    if (number != 1) 
                        path = directories[number - 2].ToString();
                        DirectoryOptions();
                }
                else
                {
                    PressEnter("Press Enter to погрустить, тк директория не найдена.");
                    DirectoryOptions();
                }
                DirectoryOptions();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Извините, что-то сломалось... " + exception.Message);
                PressEnter("Press Enter to попробовать заново или сделать что-то еще.");
                DirectoryOptions();
            }
        }

        /// <summary>
        /// Обработка пользовательского выбора операции с директорией.
        /// </summary>
        static void ChooseDirectoryOptions()
        {
            // Номер выбранной операции.
            int number;
            while (!KeyCorrect(Console.ReadKey().Key, 8, out number)) { }
            if (number == 1)
                CreateFile();
            else if (number == 2)
                ShowFiles();
            else if (number == 3)
                ShowDirectories();
            else if (number == 4)
                GoBack();
            else if (number == 5)
                ShowChoosePath();
            else if (number == 6)
                InsertFileByHand();
            else if (number == 7)
                FilesSummarize();
            else if (number == 8)
                CopyAllFiles();
        }

        /// <summary>
        /// Вывод опций директории.
        /// </summary>
        static void DirectoryOptions()
        {
            Console.Clear();
            Where();
            Console.WriteLine("1. Создать файл.\n" +
                "2. Посмотреть файлы.\n" +
                "3. Посмотреть директории.\n" +
                "4. Шаг назад.\n" +
                "5. Вернуться к телепорту.\n" +
                "6. Вставить файл.\n"+
                "7. Объединить два текстовых файла.\n" +
                "8. Копировать все файлы директории и поддиректорий.");
            ChooseDirectoryOptions();
            DirectoryOptions();

        }
    }
}
