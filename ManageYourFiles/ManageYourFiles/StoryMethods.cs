using System;

namespace ManageYourFiles
{
    partial class Program
    {
        static void ChapterOne()
        {
            var arr = new[]
            {
            @"   / \               / \  ",
            @"  /   \             /   \",
            @" (_____)           (_____)",
            @"  |   |  _   _   _  |   |",
            @"  | O |_| |_| |_| |_| O |",
            @"  |-  |          _  | - |"};
            foreach (string line in arr)
                Console.WriteLine(line);
            Console.WriteLine("Вы путник, измотанный долгим странствием, бредете по лесу.");
            Console.WriteLine("Внезапно Вы видите, что на горе впереди возвышается великолепный замок.");
            Console.WriteLine("Но его же минуту назад не было! Впрочем, путь Ваш был тяжел, Вы очень устали в поисках файлового менеджера...");
            PressEnter("...Press Enter to go there. ");
            ChapterTwo();

        }

        static void ChapterTwo()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Вас встречает таинственная незнакомка в черной мантии.\nЛицо ее скрыто за тканевой синей маской с загадочной надписью:");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("NON SCHOLAE SED VITAE");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("Где-то Вы это уже видели... " +
                "Вы задумчиво чешете лоб, мысль так и вертится на языке, " +
                "но Вам так и не удается поймать ее за хвост. \n" +
                "Все плывет перед глазами, и Вы теряете сознание.");
            PressEnter("Press Enter to continue. ");
            ChapterThree();
        }
        static void ChapterThree()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine();
            Console.WriteLine("▒█░░░▄░░▒█░▐█▀▀▒██░░░░▐█▀█▒▐█▀▀█▌▒▐██▄▒▄██▌░▐█▀▀");
            Console.WriteLine("▒█▄░▒█░░▄█░▐█▀▀▒██░░░░▐█──▒▐█▒▒█▌░▒█░▒█░▒█░░▐█▀▀");
            Console.WriteLine("░▒▀▄▀▒▀▄▀░░▐█▄▄▒██▄▄█░▐█▄█▒▐█▄▄█▌▒▐█░░░░▒█▌░▐█▄▄");
            Console.WriteLine();
            Console.WriteLine(" — Добро пожаловать, Гость.");
            Console.WriteLine("Что привело тебя сюда? Впрочем, знаю, что здесь ищешь: власть, контроль.");
            Console.WriteLine("Ты пришел по верному адресу. " +
                "Отсюда ты сможешь управлять всеми файлами видимого мира!");
            Console.WriteLine("Следуй за мной.");
            PressEnter("...Press Enter to follow. ");
            ShowChoosePath();
        }

        static void ChapterFour()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Вы открываете глаза. Сумерки. Вокруг деревья и поломанные ветки куста, в котором Вы так неуклюже расположились.\n" +
                "Нерасторопно встаете. Но рядом нет совсем ничего, что было похоже на места у замка. И замка нет.\n" +
                "В зарослях рядом поблескивают глаза неразличимого во мраке животного.");
            Console.WriteLine("\nВы действительно хотите завершить программу? \n1. Restart \n2. End");
            int number;
            while (!KeyCorrect(Console.ReadKey().Key, 2, out number)) {}
            if (number == 1)
            {
                Console.Clear();
                ChapterOne();
            }
            else
            {
                ChapterFive();
            }
        }

        static void ChapterFive()
        {
            Console.Clear();
            Console.WriteLine(@"     .-.");
            Console.WriteLine(@"     | |");
            Console.WriteLine(@"   __| |__");
            Console.WriteLine(@"  [__   __]");
            Console.WriteLine(@"     | |");
            Console.WriteLine(@"     | |");
            Console.WriteLine(@"     | |");
            Console.WriteLine(@"rip  | |");
            Console.WriteLine(@"     '-'");
            Console.WriteLine("Огромное чудище с синей тряпочкой у лохматой мордашки не удержалось " +
                "и задушило Вас, дабы не оказаться задушенными Вами.\nЕсли вы понимаете о чем я :')");
        }

    }
}
