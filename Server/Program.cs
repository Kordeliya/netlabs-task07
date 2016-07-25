using FileSystemServices;
using FileSystemServices.Entities;
using ServicesImplements;
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
    public class Program
    {

        
        static void Main(string[] args)
        {
            string url = "http://localhost:6740/";
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(url);
            try
            {
                listener.Start();
                while (listener.IsListening)
                {
                    byte[] responseBytes = null;

                    HttpListenerContext context = listener.GetContext();

                    HttpListenerRequest request = context.Request;

                    if (request.HttpMethod == "POST")
                    {
                        responseBytes = GetResponseBytes(request);

                        context.Response.ContentType = @"application/xml; charset=\'UTF-8";

                        context.Response.ContentLength64 = responseBytes.Length;
                    }
                    using (Stream output = context.Response.OutputStream)
                    {
                        output.Write(responseBytes, 0, responseBytes.Length);
                    }
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                listener.Close();
            }
        }

        private static byte[] GetResponseBytes(HttpListenerRequest request)
        {
            string paramRequest = String.Empty;
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
            string command = request.RawUrl.Replace(@"/",String.Empty);

            result = QueryExecution(command, paramRequest);

            XmlSerializer serializer = new XmlSerializer(typeof(ResponseFileService));
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.Serialize(ms, result);
                buffer = ms.ToArray();
            }
            return buffer;
        }

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

        private static List<string> GetPath(string paramRequest)
        {
            string[] result = paramRequest.Split("=&".ToCharArray());
            List<string> newResult = new List<string>();
            for (var i=0; i < result.Count(); i++)
                if (((i+1) % 2) == 0)
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
