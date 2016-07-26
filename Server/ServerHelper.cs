using FileSystemServices;
using FileSystemServices.Entities;
using ResponseMessages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Server
{
    public static class ServerHelper
    {
        /// <summary>
        /// Формирование ответа в байтах
        /// </summary>
        /// <param name="request">запрос</param>
        /// <returns></returns>
        public static byte[] GetResponseBytes(HttpListenerRequest request)
        {
            string paramRequest = String.Empty;
            XmlSerializer serializer = null;
            ResponseFileService result = new ResponseFileService();
            byte[] buffer = null;
            if (!request.HasEntityBody)
                return null;
            using (Stream body = request.InputStream)
            {
                using (StreamReader reader = new StreamReader(body, request.ContentEncoding))
                {
                    paramRequest = reader.ReadToEnd();
                }
            }
            string command = request.RawUrl.Replace(@"/", String.Empty);

            result = QueryExecution(command, paramRequest);
            if (result is ResponseFileService<Folder>)
            {              
                serializer = new XmlSerializer(typeof(ResponseFileService<Folder>));
            }
            else
            {
                serializer = new XmlSerializer(typeof(ResponseFileService));
            }

            using (MemoryStream ms = new MemoryStream())
            {
                serializer.Serialize(ms, result);
                buffer = ms.ToArray();
            }
            return buffer;
        }

        /// <summary>
        /// Выполнение запроса
        /// </summary>
        /// <param name="command">команда</param>
        /// <param name="paramRequest">параметры команды</param>
        /// <returns></returns>
        private static ResponseFileService QueryExecution(string command, string paramRequest)
        {
            SingleFileService facade = SingleFileService.GetInstance();
            ResponseFileService result = new ResponseFileService();
            List<string> path = new List<string>();
            FileSystemPath systemPath = null;
            FileSystemPath systemPathDest = null;
            FileSystemElement elem;
            if (!String.IsNullOrEmpty(paramRequest))
            {
                try
                {
                    switch (command)
                    {
                        case "md":
                        case "mf":
                            path = GetPath(paramRequest);
                            if (path == null)
                                return null;
                            systemPath = new FileSystemPath(path[0]);
                            elem = GetElementSystem(systemPath);
                            facade.Disk.Create(systemPath, elem);
                            result = new ResponseFileService
                            {
                                IsSuccess = true
                            };
                            break;
                        case "rd":
                        case "del":
                            path = GetPath(paramRequest);
                            if (path == null)
                                return null;
                            systemPath = new FileSystemPath(path[0]);
                            facade.Disk.Delete(systemPath);
                            result = new ResponseFileService
                            {
                                IsSuccess = true
                            };
                            break;
                        case "copy":
                            path = GetPath(paramRequest);
                            if (path == null)
                                return null;
                            systemPath = new FileSystemPath(path[0]);
                            systemPathDest = new FileSystemPath(path[1]);
                            facade.Disk.Copy(systemPath, systemPathDest);
                            result = new ResponseFileService
                            {
                                IsSuccess = true
                            };
                            break;
                        case "move":
                            path = GetPath(paramRequest);
                            if (path == null)
                                return null;
                            systemPath = new FileSystemPath(path[0]);
                            systemPathDest = new FileSystemPath(path[1]);
                            facade.Disk.Move(systemPath, systemPathDest);
                            result = new ResponseFileService
                            {
                                IsSuccess = true
                            };
                            break;
                        case "print":
                            path = GetPath(paramRequest);
                            if (path == null)
                                return null;
                            systemPath = new FileSystemPath(path[0]);
                            Folder folder = (Folder)facade.Disk.GetTree(systemPath);
                            result = new ResponseFileService<Folder>
                            {
                                IsSuccess = true,
                                Data = folder
                            };
                            break;
                    }

                }
                catch (Exception ex)
                {
                    result = new ResponseFileService
                    {
                        IsSuccess = false,
                        Error = ex.Message
                    };
                }
                return result;
            }
            else
                return null;
        }

        /// <summary>
        /// Получение писка путей
        /// </summary>
        /// <param name="paramRequest"></param>
        /// <returns></returns>
        private static List<string> GetPath(string paramRequest)
        {
            string[] result = paramRequest.Split("=&".ToCharArray());
            List<string> newResult = new List<string>();
            for (var i = 0; i < result.Count(); i++)
                if (((i + 1) % 2) == 0)
                    newResult.Add(result[i]);
            return newResult;
        }


        /// <summary>
        /// Возвращает по пути FileSystemItem
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static FileSystemElement GetElementSystem(FileSystemPath path)
        {
            FileSystemElement element;
            if (CheckIsFile(path))
                element = new FileItem(path.Segments.Last());
            else
                element = new Folder(path.Segments.Last());
            return element;
        }

        /// <summary>
        /// Проверка файл ли это
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool CheckIsFile(FileSystemPath path)
        {
            Regex regex = new Regex(@"[A-Za-z0-9]*\.[a-z0-9]{2,7}");
            return regex.IsMatch(path.Segments.Last());
        }
    }
}
