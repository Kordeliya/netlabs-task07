using System;
using Facade;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Test
{
    /// <summary>
    /// Работа в консоле с виртуальной файловой системой
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Facade.Facade _instance = Facade.Facade.GetInstance();
            string currentDirectory = "C:\\";
            string error;
            while (true)
            {
                Console.Write("{0}>", currentDirectory);
                string test = Console.ReadLine();
                string[] commandArgs = Regex.Split(test," ");
                commandArgs[0] = commandArgs[0].ToLower();
                if (commandArgs.Length > 0)
                {
                    
                    string print = String.Empty;
                    switch (commandArgs[0])
                    {
                        case "help":
                            Console.WriteLine(GetHelpMessage(commandArgs));
                            break;
                        case "md" :
                            if (CheckCountArgs(commandArgs,2))
                            {
                                if (_instance.TryCreateDirectory(commandArgs[1], currentDirectory, out error))
                                    Console.WriteLine(">Успешно создано");
                                else
                                    Console.WriteLine(">Произошла ошибка:{0}", error);
                            }
                            else
                                WriteErrorMessage();
                            break;
                        case "rd":
                            if (CheckCountArgs(commandArgs, 2))
                            {
                                if (_instance.TryDeleteDirectory(commandArgs[1], currentDirectory, false, out error))
                                    Console.WriteLine(">Директория удалена");
                                else
                                    Console.WriteLine(">Произошла ошибка:{0}", error);
                                
                            }
                            else
                                WriteErrorMessage();
                            break;
                        case "deltree":
                            if (CheckCountArgs(commandArgs, 2))
                            {
                                if (_instance.TryDeleteDirectory(commandArgs[1], currentDirectory, true, out error))
                                    Console.WriteLine(">Директория удалена");
                                else
                                    Console.WriteLine(">Произошла ошибка:{0}", error);
                            }
                            else
                                WriteErrorMessage();
                            break;
                        case "mf":
                            if (CheckCountArgs(commandArgs, 2))
                            {
                                if (_instance.TryCreateFile(commandArgs[1], currentDirectory, out error))
                                    Console.WriteLine(">Успешно создан");
                                else
                                    Console.WriteLine(">Произошла ошибка:{0}", error);
                            }
                            else
                                WriteErrorMessage();
                            break;
                        case "del":
                            if (CheckCountArgs(commandArgs, 2))
                            {
                                if (_instance.TryDeleteFile(commandArgs[1], currentDirectory, out error))
                                    Console.WriteLine(">Успешно удален");
                                else
                                    Console.WriteLine(">Произошла ошибка:{0}", error);
                            }
                            else
                                WriteErrorMessage();
                            break;
                        case "copy":
                            if (CheckCountArgs(commandArgs, 3))
                            {
                                if (_instance.TryCopy(commandArgs[1], commandArgs[2], currentDirectory, out error))
                                    Console.WriteLine(">Успешно скопировано");
                                else
                                    Console.WriteLine(">Произошла ошибка:{0}", error);
                            }
                            else
                                WriteErrorMessage();
                            break;
                        case "move":
                            if (CheckCountArgs(commandArgs, 3))
                            {
                                if (_instance.TryMove(commandArgs[1], commandArgs[2], currentDirectory, out error))
                                    Console.WriteLine(">Успешно скопировано");
                                else
                                    Console.WriteLine(">Произошла ошибка:{0}", error);       
                            }
                            else
                                WriteErrorMessage();
                            break;
                        case "print":
                            if (CheckCountArgs(commandArgs, 2))
                            {
                                FacadeFolder folder = null;
                                if (_instance.TryGetTree(commandArgs[1], currentDirectory,out folder, out error))
                                {
                                    PrintTree(folder);
                                }
                                else
                                    Console.WriteLine(">Произошла ошибка:{0}", error);
                            }
                            else
                                WriteErrorMessage();
                            break;
                        default:
                            Console.WriteLine(">Команда не распознана");
                            break;
                    }
                    error = String.Empty;
                }


        }
    }
        /// <summary>
        /// Информация о дереве
        /// </summary>
        /// <param name="folder"></param>
        private static void PrintTree(FacadeFolder folder)
        {
            Console.WriteLine("Данная директория содержит {0} папок, {1} файлов", folder.Directories.Count(), folder.Files.Count());
        }

        /// <summary>
        /// Вывод сообщения об ошибке
        /// </summary>
        private static void WriteErrorMessage()
        {
            Console.WriteLine(">Введено неверное количество параметров. Для получения справки введите help. Для получения справки по конкретной команде введите help команда");
        }

        /// <summary>
        /// Получение справки
        /// </summary>
        /// <param name="parts"></param>
        /// <returns></returns>
        private static string GetHelpMessage(string[] parts)
        {
            string message = String.Empty;
            if (parts.Count() == 1)
                message = "Для получение сведений об определенной команде наберите help команда"
                           + Environment.NewLine + "md - создание папки."
                           + Environment.NewLine + "rd - удаление папки."
                           + Environment.NewLine + "mf - создание файла."
                           + Environment.NewLine + "del - удаление файла."
                           + Environment.NewLine + "copy - копирование файла или папки."
                           + Environment.NewLine + "print - вывод дерева каталогов для заданного пути.";
            else
            {
                switch (parts[1])
                {
                    case "md":
                        message = "md - создание папки. Структура 'md path', где path путь создаваемой папки";
                        break;
                    case "rd":
                        message = "rd - удаление папки. Структура 'rd path', где path путь удаляемой папки";
                        break;
                    case "mf":
                        message = "mf - создание файла. Структура 'mf path', где path путь создаваемого файла";
                        break;
                    case "del":
                        message = "del - удаление файла. Структура 'del path', где path путь удаляемого файла";
                        break;
                    case "copy":
                        message = "copy - копирование файла или папки. Структура 'copy source destination',"
                                        + "где source путь копируемого объекта, destination - пункт назначение";
                        break;
                    case "move":
                        message = "copy - копирование файла или папки.  Структура 'move source destination',"
                                        + "где source путь перемещаемого объекта, destination - пункт назначение";
                        break;
                    case "print":
                        message = "print - вывод дерева каталогов для заданного пути. Структура 'print path'";
                        break;

                }
            }
            return message;
        }

        /// <summary>
        /// Проверка числа аргументов
        /// </summary>
        /// <param name="args"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private static bool CheckCountArgs(string[] args, int count)
        {
            if (args.Count() == count)
                return true;
            return false;
        }
}
}
