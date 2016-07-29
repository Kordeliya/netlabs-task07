using FileSystemServices;
using FileSystemServices.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Client
{
    public class Program
    {
        public static void Main(string[] args)
        {

            ClientConnection _client;
            FileSystemProxy _proxy = new FileSystemProxy(); ;
            HttpResponseMessage response;
            try
            {
                while (true)
                {
                    Console.Write(">");
                    string test = Console.ReadLine();
                    string[] commandArgs = Regex.Split(test, " ");
                    commandArgs[0] = commandArgs[0].ToLower();
                    if (commandArgs.Length > 0)
                    {

                        string print = String.Empty;
                        switch (commandArgs[0])
                        {
                            case "help":
                                Console.WriteLine(ClientHelper.GetHelpMessage(commandArgs));
                                break;
                            case "md":
                                if (ClientHelper.CheckCountArgs(commandArgs, 2))
                                {
                                    if (ClientHelper.CheckIsItFile(commandArgs[1]))
                                    {
                                        ClientHelper.WriteErrorMessage();
                                        continue;
                                    }
                                    FileSystemPath path = new FileSystemPath(commandArgs[1]);
                                    _proxy.Create(path,new Folder(path.Segments.Last()));
                                }
                                else
                                    ClientHelper.WriteErrorMessage();
                                break;
                            case "rd":
                                if (ClientHelper.CheckCountArgs(commandArgs, 2))
                                {
                                    if (ClientHelper.CheckIsItFile(commandArgs[1]))
                                    {
                                        ClientHelper.WriteErrorMessage();
                                        continue;
                                    }
                                    FileSystemPath path = new FileSystemPath(commandArgs[1]);
                                    _proxy.Delete(path);

                                }
                                else
                                    ClientHelper.WriteErrorMessage();
                                break;
                             case "mf":
                                if (ClientHelper.CheckCountArgs(commandArgs, 2))
                                {
                                    if (!ClientHelper.CheckIsItFile(commandArgs[1]))
                                    {
                                        ClientHelper.WriteErrorMessage();
                                        continue;
                                    }
                                    FileSystemPath path = new FileSystemPath(commandArgs[1]);
                                    _proxy.Create(path, new FileItem(path.Segments.Last()));

                                }
                                else
                                    ClientHelper.WriteErrorMessage();
                                break;
                             case "del":
                               if (ClientHelper.CheckCountArgs(commandArgs, 2))
                                {
                                    if (!ClientHelper.CheckIsItFile(commandArgs[1]))
                                    {
                                        ClientHelper.WriteErrorMessage();
                                        continue;
                                    }
                                    FileSystemPath path = new FileSystemPath(commandArgs[1]);
                                    _proxy.Delete(path);

                                }
                                else
                                    ClientHelper.WriteErrorMessage();
                                break;
                             case "copy":
                               if (ClientHelper.CheckCountArgs(commandArgs, 3))
                                {
                                    FileSystemPath path = new FileSystemPath(commandArgs[1]);
                                    FileSystemPath pathDest = new FileSystemPath(commandArgs[2]);
                                    _proxy.Copy(path, pathDest);
                                }
                                else
                                    ClientHelper.WriteErrorMessage();
                                break;
                             case "move":
                                 if (ClientHelper.CheckCountArgs(commandArgs, 3))
                                {
                                    FileSystemPath path = new FileSystemPath(commandArgs[1]);
                                    FileSystemPath pathDest = new FileSystemPath(commandArgs[2]);
                                    _proxy.Move(path, pathDest);
                                }
                                else
                                    ClientHelper.WriteErrorMessage();
                                break;
                             case "print":
                                 if (ClientHelper.CheckCountArgs(commandArgs, 1))
                                {
                                    if (ClientHelper.CheckIsItFile(commandArgs[1]))
                                    {
                                        ClientHelper.WriteErrorMessage();
                                        continue;
                                    }
                                    FileSystemPath path = new FileSystemPath(commandArgs[1]);
                                    Folder folder = (Folder)_proxy.GetTree(path);
                                    ClientHelper.PrintTree(folder);
                                }
                                else
                                    ClientHelper.WriteErrorMessage();
                                break;                               
                            default:
                                Console.WriteLine(">Команда не распознана");
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("произошла ошибка {0}", ex.Message);
            }
            Console.ReadKey();
        }     
    }
}
