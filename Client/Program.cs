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
                                    Uri path = new Uri(commandArgs[1], UriKind.Relative);
                                    string[] segments = ClientHelper.GetSegments(path);
                                    if (segments != null)
                                    {
                                        _proxy.Create(path, new Folder(segments.Last()));
                                    }
                                    else
                                    {
                                        ClientHelper.WriteErrorMessage();
                                    }
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
                                    Uri path = new Uri(commandArgs[1], UriKind.Relative);
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
                                    Uri path = new Uri(commandArgs[1], UriKind.Relative);
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
                                    Uri path = new Uri(commandArgs[1], UriKind.Relative);
                                    _proxy.Delete(path);

                                }
                                else
                                    ClientHelper.WriteErrorMessage();
                                break;
                             case "copy":
                               if (ClientHelper.CheckCountArgs(commandArgs, 3))
                                {
                                    Uri path = new Uri(commandArgs[1], UriKind.Relative);
                                    Uri pathDest = new Uri(commandArgs[2], UriKind.Relative);
                                    _proxy.Copy(path, pathDest);
                                }
                                else
                                    ClientHelper.WriteErrorMessage();
                                break;
                             case "move":
                                 if (ClientHelper.CheckCountArgs(commandArgs, 3))
                                {
                                    Uri path = new Uri(commandArgs[1], UriKind.Relative);
                                    Uri pathDest = new Uri(commandArgs[2], UriKind.Relative);
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
                                    Uri path = new Uri(commandArgs[1], UriKind.Relative);
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
