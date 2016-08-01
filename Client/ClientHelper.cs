using FileSystemServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Client
{
    public static class ClientHelper
    {

        /// <summary>
        /// Вывод сообщения об ошибке
        /// </summary>
        public static void WriteErrorMessage()
        {
            Console.WriteLine(">Введены неверные параметры. Для получения справки введите help. Для получения справки по конкретной команде введите help команда");
        }

        /// <summary>
        /// Получение справки
        /// </summary>
        /// <param name="parts"></param>
        /// <returns></returns>
        public static string GetHelpMessage(string[] parts)
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
        public static bool CheckCountArgs(string[] args, int count)
        {
            if (args.Count() == count)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Проверка файл ли это
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool CheckIsItFile(string path)
        {
            Regex regex = new Regex(@"[\s\S]*\.[a-z0-9]{2,7}");
            return regex.IsMatch(path);
        }


        /// <summary>
        /// Вывод информации о дереве
        /// </summary>
        /// <param name="folder"></param>
        public static void PrintTree(FileSystemElement folder)
        {
            int countFiles = 0;
            int countFolders = 0;
            if (folder != null)
            {
                foreach (var item in ((Folder)folder).Elements)
                {
                    if (item is Folder)
                    {
                        countFolders++;
                    }
                    else
                    {
                        countFiles++;
                    }
                }
                Console.WriteLine("Данная директория содержит {0} папок, {1} файлов", countFolders, countFiles);
            }
            else
                Console.WriteLine("Директория не найдена!");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] GetSegments(Uri path)
        {
            if (path != null)
            {
                return path.OriginalString.Split("/\\".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToArray();
            }
            else
            {
                return null;
            }
        }
    }
}
