using System;
using System.Collections.Generic;
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
            HttpResponseMessage response;
            try
            {
                DateTime date = new DateTime(1991, 25, 1);
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
                                    using (_client = new ClientConnection())
                                    {
                                        var content = new FormUrlEncodedContent(new[]
                                        {
                                            new KeyValuePair<string, string>("path",  commandArgs[1]),
                                        });
                                        response = _client.SendRequest(content, "md").Result;
                                        CheckResponse(response);
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
                                    using (_client = new ClientConnection())
                                    {
                                        var content = new FormUrlEncodedContent(new[]
                                        {
                                            new KeyValuePair<string, string>("path",  commandArgs[1]),
                                        });
                                        response = _client.SendRequest(content, "rd").Result;
                                        CheckResponse(response);
                                    }
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
                                    using (_client = new ClientConnection())
                                    {
                                        var content = new FormUrlEncodedContent(new[]
                                        {
                                            new KeyValuePair<string, string>("path",  commandArgs[1]),
                                        });
                                        response = _client.SendRequest(content, "mf").Result;
                                        CheckResponse(response);
                                    }

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
                                    using (_client = new ClientConnection())
                                    {
                                        var content = new FormUrlEncodedContent(new[]
                                        {
                                            new KeyValuePair<string, string>("path",  commandArgs[1]),
                                        });
                                        response = _client.SendRequest(content, "del").Result;
                                        CheckResponse(response);
                                    }
                                }
                                else
                                    ClientHelper.WriteErrorMessage();
                                break;
                            case "copy":
                                if (ClientHelper.CheckCountArgs(commandArgs, 3))
                                {
                                    using (_client = new ClientConnection())
                                    {
                                        var content = new FormUrlEncodedContent(new[]
                                        {
                                            new KeyValuePair<string, string>("path",  commandArgs[1]),
                                            new KeyValuePair<string, string>("pathDest",  commandArgs[2]),
                                        });
                                        response = _client.SendRequest(content, "copy").Result;
                                        CheckResponse(response);
                                    }
                                }
                                else
                                    ClientHelper.WriteErrorMessage();
                                break;
                            case "move":
                                if (ClientHelper.CheckCountArgs(commandArgs, 3))
                                {
                                    using (_client = new ClientConnection())
                                    {
                                        var content = new FormUrlEncodedContent(new[]
                                        {
                                            new KeyValuePair<string, string>("path",  commandArgs[1]),
                                            new KeyValuePair<string, string>("pathDest",  commandArgs[2]),
                                        });
                                        response = _client.SendRequest(content, "move").Result;
                                        CheckResponse(response);
                                    }
                                }
                                else
                                    ClientHelper.WriteErrorMessage();
                                break;
                            //case "print":
                            //    if (CheckCountArgs(commandArgs, 2))
                            //    {
                            //        FacadeFolder folder = null;
                            //        if (_instance.TryGetTree(commandArgs[1], currentDirectory,out folder, out error))
                            //        {
                            //            PrintTree(folder);
                            //        }
                            //        else
                            //            Console.WriteLine(">Произошла ошибка:{0}", error);
                            //    }
                            //    else
                            //        WriteErrorMessage();
                            //    break;
                            default:
                                Console.WriteLine(">Команда не распознана");
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Проверка ответа от сервера
        /// </summary>
        /// <param name="response"></param>
        private static void CheckResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                ResponseFileService responseXml = new ResponseFileService();
               // response.Content
                XmlSerializer serializer = new XmlSerializer(typeof(ResponseFileService));
                Stream xmlStream = ((StreamContent)response.Content).ReadAsStreamAsync().Result;
                responseXml = (ResponseFileService)serializer.Deserialize(xmlStream);
                if(!responseXml.IsSuccess)
                    Console.WriteLine(">{0}", responseXml.Error ?? "Произошла неизвестная ошибка");

            }
            else
                Console.WriteLine(">Произошла неизвестная ошибка");
        }

       
    }
}
