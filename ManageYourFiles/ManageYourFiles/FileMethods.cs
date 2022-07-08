using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ManageYourFiles
{
    /// <summary>
    /// Класс программы.
    /// </summary>
    partial class Program
    {
        /// <summary>
        /// "Буфер обмена." Путь к файлу, который мы копируем, или его отсутствие.
        /// </summary>
        static string copy = "";

        /// <summary>
        /// Перемещение файла.
        /// </summary>
        static void FileMove()
        {
            try
            {
                Where();
                Console.WriteLine("Абсолютный путь, куда хотите перенести, включая старое или новое имя файла и расширение. ");
                string pathToMove = Console.ReadLine();
                if (OverwriteFile(pathToMove))
                {
                    File.Move(@path, @pathToMove, true);
                    PressEnter("Press Enter to радоваться. ");
                    GoBack();
                }
                else
                {
                    File.Move(@path, @pathToMove, false);
                    PressEnter("Press Enter to радоваться. ");
                    GoBack();
                }
                GoBack();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Произошла ошибка " + exception.Message);
                PressEnter("Press Enter to попробовать заново.");
                GoBack();
            }
        }

        /// <summary>
        /// Удаление файла.
        /// </summary>
        static void DeleteFile()
        {
            try
            {
                if (File.Exists(@path))
                {
                    File.Delete(@path);
                    Console.WriteLine("Удаление прошло успешно.");
                    PressEnter("Press Enter to запрыгать от счастья.");
                }
                else
                {
                    Console.WriteLine("Файл, который вы хотите удалить не существует.");
                    PressEnter("Press Enter to зарыдать от огорчения.");
                }

                GoBack();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Произошла ошибка " + exception.Message);
                PressEnter("Press Enter to попробовать заново.");
                FileOptions();
            }
        }

        /// <summary>
        /// Создание файла.
        /// </summary>
        static void CreateFile()
        {
            try
            {
                Console.WriteLine("\nВведите имя для нового файла.");
                string name = Console.ReadLine();
                path += @"/" + name + ".txt";

                if (File.Exists(path))
                    Console.WriteLine("Такой файл уже существует, но Вы можете удалить его и попытаться заново.");
                else
                {
                    Encoding encode = Encoding.GetEncoding(ChooseEncoding());
                    string[] createText = InputFile();
                    File.WriteAllLines(path, createText, encode);
                }
                PressEnter("Press Enter to принять тщетность бытия.");
                GoBack();

            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("В доступе отказано. Иди своей дорогой, чужак.");
                PressEnter("Press Enter to продолжить");
                GoBack();
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("О нет. Кто-то украл директорию.");
                PressEnter("Press Enter to продолжить");
                GoBack();
            }
            catch (Exception)
            {
                Console.WriteLine("Что-то пошло не так или не туда.");
                PressEnter("Press Enter to продолжить");
                GoBack();
            }

        }

        /// <summary>
        /// Ввод текста для файла.
        /// </summary>
        /// <returns> Массив строк. </returns>
        static string[] InputFile()
        {
            int numberOfLines;
            string number;
            do
            {
                Console.WriteLine("\nВведите число (int) строк, которое Вы хотите записать в файл.");
                number = Console.ReadLine();
            }
            while (!int.TryParse(number, out numberOfLines));
            string[] lines = new string[numberOfLines];
            for (var i = 0; i < numberOfLines; i++)
            {
                Console.WriteLine("Введите строку " + (i + 1));
                string input = Console.ReadLine();
                lines[i] = input;

            }
            return lines;

        }

        /// <summary>
        /// Копирование файла.
        /// </summary>
        static void CopyFile()
        {
            copy = path;
            Console.WriteLine("Файл скопирован.");
            PressEnter("Press enter to ура.");
            GoBack();
        }

        /// <summary>
        /// Решаем, что делать в случае если файл с таким именем уже существует.
        /// </summary>
        /// <param name="path"> Путь, где проферяем есть ли файл с таким именем. </param>
        /// <returns> true - при согласии на перезапись файла. false - в остальных случаяхю </returns>
        static bool OverwriteFile(string path)
        {
            if (File.Exists(path))
            {
                string[] pathSplit;
                pathSplit = path.Split(@"/", StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine("Файл " + pathSplit[pathSplit.Length - 1] + " уже существует. Хотите перезаписать?");
                Console.WriteLine("1. Yes.");
                Console.WriteLine("2. No.");
                int number;
                while (!KeyCorrect(Console.ReadKey().Key, 2, out number)) { }
                if (number == 1) return true;
            }
            return false;

        }

        /// <summary>
        /// Вставить файл, находясь в определенной директории.
        /// </summary>
        static void InsertFileByHand()
        {
            if (copy == "")
            {
                Console.WriteLine("\nВы не скопировали ни одного файла.");
            }
            else
            {
                Console.WriteLine("\nВведите имя для нового файла, без расширения.");
                string name = Console.ReadLine();
                string copyPath = path + @"/" + name + ".txt";
                if (OverwriteFile(copyPath))
                {
                    File.Copy(copy, copyPath, true);
                    copy = "";
                }
                else
                {
                    while (File.Exists(copyPath))
                    {
                        Console.WriteLine("\nВведите имя файла, которое еще не занято.");
                        name = Console.ReadLine();
                        copyPath = path + @"/" + name + ".txt";
                    }
                    File.Copy(copy, copyPath);
                    copy = "";
                }
            }
            PressEnter("Press Enter");
            DirectoryOptions();

        }


        /// <summary>
        /// Выбор кодировки.
        /// </summary>
        /// <returns> Название выбранной кодировки.</returns>
        static string ChooseEncoding()
        {
            Console.WriteLine("1. UTF-8\n2. UTF-32\n3. US-ASCII");
            int number;
            while (!KeyCorrect(Console.ReadKey().Key, 3, out number)) { }
            if (number == 1)
                return "utf-8";
            else if (number == 2)
                return "utf-32";
            else if (number == 3)
                return "us-ascii";
            else
                return "utf-8";
        }

        /// <summary>
        /// Считать текстовый файл.
        /// </summary>
        static void ReadFile()
        {
            try
            {
                if (!File.Exists(path))
                {
                    Console.WriteLine("Такого файла не существует.");
                    PressEnter("Press Enter: выйти в директорию");
                    GoBack();
                }
                else
                {
                    using (StreamReader streamReader = new StreamReader(@path, Encoding.GetEncoding(ChooseEncoding()), false))
                    {
                        Console.WriteLine();
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                        }
                    }

                    PressEnter("Press Enter to confirm you have read this.");
                    FileOptions();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Произошла ошибка " + exception.Message);
                Console.WriteLine("Убедитесь, что к файлу есть доступ.");
                PressEnter("Press Enter to попробовать заново.");
                FileOptions();
            }
        }


        /// <summary>
        /// Выбор операции, которую можно сделать с файлом.
        /// </summary>
        static void ChooseFileOptions()
        {
            int number;
            while (!KeyCorrect(Console.ReadKey().Key, 5, out number)) { }
            Console.Clear();
            if (number == 1)
            {
                if (File.Exists(path) && Path.GetExtension(path) != ".txt")
                    Console.WriteLine("Просмотр файла недоступен.");
                else if (File.Exists(path))
                    ReadFile();

            }
            else if (number == 2)
            {
                CopyFile();
            }
            else if (number == 3)
                FileMove();
            else if (number == 4)
                DeleteFile();
            else if (number == 5)
                GoBack();
        }

        /// <summary>
        /// Вывод возможных операций пользователя с файлом.
        /// </summary>
        static void FileOptions()
        {
            Where();
            Console.WriteLine("1. Просмотреть файл.\n" +
                "2. Копировать файл.\n" +//сделать вставкууууууууу
                "3. Переместить файл.\n" +
                "4. Удалить файл.\n" +
                "5. Вернуться в директорию.");

            ChooseFileOptions();
        }

        /// <summary>
        /// Посмотреть файлы, подходящие под заданную маску.
        /// </summary>
        /// <param name="allFiles"> Файлы. </param>
        /// <param name="files"> Подходящие файлы. </param>
        /// <returns> Заданная пользователем маска. </returns>
        static string ShowFilesMask(string[] allFiles, out string[] files)
        {
            files = new string[0];
            try
            {
                Console.WriteLine("\nХотите посмотреть файлы, подходящие под определенную маску?");
                Console.WriteLine("1. Да. \n2. Нет.");
                string mask = "";
                int number;
                while (!KeyCorrect(Console.ReadKey().Key, 2, out number)) { }
                if (number == 2)
                {
                    files = allFiles;
                }
                else
                {
                    Console.WriteLine("\nВведите маску в стиле «регулярки шарпов».\nПодробнее: " +
                        "https://docs.microsoft.com/ru-ru/dotnet/standard/base-types/regular-expressions");
                    mask = Console.ReadLine();
                    foreach (string fileName in allFiles)
                    {
                        if (Regex.IsMatch(fileName, @mask))
                        {
                            Array.Resize(ref files, files.Length + 1);
                            files[files.Length - 1] = fileName;

                        }
                    }
                }
                return mask;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Произошла ошибка " + exception.Message);
                Console.WriteLine("Скорее всего, дело в том, что вы вводите неправильно регулярное выражение.");
                PressEnter("Press Enter to попробовать заново.");
                DirectoryOptions();
                return "";
            }

        }

        /// <summary>
        /// Просмотр всех файлов директории и поддиректории.
        /// </summary>
        /// <returns> Файлы. </returns>
        static string[] ShowFilesAllDirectories()
        {
            try
            {
                Console.WriteLine("Хотите увидеть файлы не только данной директории, но и поддиректорий?");
                Console.WriteLine("1. Да.\n2. Нет.");
                int number;
                string[] allFiles = new string[0];
                while (!KeyCorrect(Console.ReadKey().Key, 2, out number)) { }
                if (number == 2)
                {
                    allFiles = Directory.GetFiles(path);

                }
                else
                {
                    allFiles = Directory.GetFiles(path, "", SearchOption.AllDirectories);
                }
                return allFiles;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Произошла ошибка " + exception.Message);
                Console.WriteLine("Совет. Проверьте доступы.");
                PressEnter("Press Enter to попробовать заново.");
                DirectoryOptions();
                string[] allFiles = new string[0];
                return allFiles;
            }

        }

        /// <summary>
        /// Вывод всех файлов по указанному пути.
        /// </summary>
        static void ShowFiles()
        {
            try
            {
                Console.Clear();
                if (Directory.Exists(path))
                {
                    string[] allFiles = ShowFilesAllDirectories();
                    string mask = ShowFilesMask(allFiles, out string[] files);
                    Console.Clear();
                    if (mask != "") Console.WriteLine("Вы просматриваете файлы, соответствующие маске: " + mask);
                    Console.WriteLine("Вы просматриваете файлы, расположенные по адресу: " + path);
                    Console.WriteLine("1. Вернуться к выбору действия.");
                    if (files.Length == 0)
                        Console.WriteLine("Файлы не найдены.");
                    ShowArray(files, 2);
                    int number;
                    if (files.Length + 1 <= 9)
                        while (!KeyCorrect(Console.ReadKey().Key, files.Length + 1, out number)) { }
                    else while (!int.TryParse(Console.ReadLine(), out number) || number < 1 || number > files.Length + 1)
                            Console.WriteLine("Введите номер нужного файла или выберите выход в меню.");
                    if (number == 1) DirectoryOptions();
                    else
                    {
                        Console.Clear();
                        path = files[number - 2].ToString();
                        FileOptions();
                    }
                }
                FileOptions();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Произошла ошибка " + exception.Message);
                Console.WriteLine("Совет: проверьте доступы.");
                PressEnter("Press Enter to попробовать заново.");
                DirectoryOptions();

            }

        }

    }
}
