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
            //Facade.Facade _instance = Facade.Facade.GetInstance();
            //string currentDirectory = "C:\\";
            //string error;
            //while (true)
            //{
            //    Console.Write("{0}>", currentDirectory);
            //    string test = Console.ReadLine();
            //    string[] commandArgs = Regex.Split(test," ");
            //    commandArgs[0] = commandArgs[0].ToLower();
            //    if (commandArgs.Length > 0)
            //    {
                    
            //        string print = String.Empty;
            //        switch (commandArgs[0])
            //        {
            //            case "help":
            //                Console.WriteLine(GetHelpMessage(commandArgs));
            //                break;
            //            case "md" :
            //                if (CheckCountArgs(commandArgs,2))
            //                {
            //                    if (_instance.TryCreateDirectory(commandArgs[1], currentDirectory, out error))
            //                        Console.WriteLine(">Успешно создано");
            //                    else
            //                        Console.WriteLine(">Произошла ошибка:{0}", error);
            //                }
            //                else
            //                    WriteErrorMessage();
            //                break;
            //            case "rd":
            //                if (CheckCountArgs(commandArgs, 2))
            //                {
            //                    if (_instance.TryDeleteDirectory(commandArgs[1], currentDirectory, false, out error))
            //                        Console.WriteLine(">Директория удалена");
            //                    else
            //                        Console.WriteLine(">Произошла ошибка:{0}", error);
                                
            //                }
            //                else
            //                    WriteErrorMessage();
            //                break;
            //            case "deltree":
            //                if (CheckCountArgs(commandArgs, 2))
            //                {
            //                    if (_instance.TryDeleteDirectory(commandArgs[1], currentDirectory, true, out error))
            //                        Console.WriteLine(">Директория удалена");
            //                    else
            //                        Console.WriteLine(">Произошла ошибка:{0}", error);
            //                }
            //                else
            //                    WriteErrorMessage();
            //                break;
            //            case "mf":
            //                if (CheckCountArgs(commandArgs, 2))
            //                {
            //                    if (_instance.TryCreateFile(commandArgs[1], currentDirectory, out error))
            //                        Console.WriteLine(">Успешно создан");
            //                    else
            //                        Console.WriteLine(">Произошла ошибка:{0}", error);
            //                }
            //                else
            //                    WriteErrorMessage();
            //                break;
            //            case "del":
            //                if (CheckCountArgs(commandArgs, 2))
            //                {
            //                    if (_instance.TryDeleteFile(commandArgs[1], currentDirectory, out error))
            //                        Console.WriteLine(">Успешно удален");
            //                    else
            //                        Console.WriteLine(">Произошла ошибка:{0}", error);
            //                }
            //                else
            //                    WriteErrorMessage();
            //                break;
            //            case "copy":
            //                if (CheckCountArgs(commandArgs, 3))
            //                {
            //                    if (_instance.TryCopy(commandArgs[1], commandArgs[2], currentDirectory, out error))
            //                        Console.WriteLine(">Успешно скопировано");
            //                    else
            //                        Console.WriteLine(">Произошла ошибка:{0}", error);
            //                }
            //                else
            //                    WriteErrorMessage();
            //                break;
            //            case "move":
            //                if (CheckCountArgs(commandArgs, 3))
            //                {
            //                    if (_instance.TryMove(commandArgs[1], commandArgs[2], currentDirectory, out error))
            //                        Console.WriteLine(">Успешно скопировано");
            //                    else
            //                        Console.WriteLine(">Произошла ошибка:{0}", error);       
            //                }
            //                else
            //                    WriteErrorMessage();
            //                break;
            //            case "print":
            //                if (CheckCountArgs(commandArgs, 2))
            //                {
            //                    FacadeFolder folder = null;
            //                    if (_instance.TryGetTree(commandArgs[1], currentDirectory,out folder, out error))
            //                    {
            //                        PrintTree(folder);
            //                    }
            //                    else
            //                        Console.WriteLine(">Произошла ошибка:{0}", error);
            //                }
            //                else
            //                    WriteErrorMessage();
            //                break;
            //            default:
            //                Console.WriteLine(">Команда не распознана");
            //                break;
            //        }
            //        error = String.Empty;
            //    }


        }
    }
       
}

