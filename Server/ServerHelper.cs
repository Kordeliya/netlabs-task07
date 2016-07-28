using FileSystemServices;
using FileSystemServices.Entities;
using Messages;
using Newtonsoft.Json;
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
            string requestJson = String.Empty;
            BaseResponse result = null;

            byte[] buffer = null;
            if (!request.HasEntityBody)
                return null;
            using (Stream body = request.InputStream)
            {
                using (StreamReader reader = new StreamReader(body, request.ContentEncoding))
                {
                    requestJson = reader.ReadToEnd();
                }
            }
            string command = request.RawUrl.Replace(@"/", String.Empty);

            result = QueryExecution(command, requestJson);
            var jsonResponse = JsonConvert.SerializeObject(result);

            buffer = Encoding.UTF8.GetBytes(jsonResponse);

            return buffer;
        }

        /// <summary>
        /// Выполнение запроса
        /// </summary>
        /// <param name="command">команда</param>
        /// <param name="paramRequest">параметры команды</param>
        /// <returns></returns>
        private static BaseResponse QueryExecution(string command, string jsonRequest)
        {
            BaseRequest request = null;
            BaseResponse result = null;

            SingleFileService instanceService = SingleFileService.GetInstance();

            if (!String.IsNullOrEmpty(jsonRequest))
            {
                try
                {
                    switch (command)
                    {
                        case "create":
                            request = JsonConvert.DeserializeObject<CreateRequest>(jsonRequest);
                            instanceService.Disk.Create(request.Path, ((CreateRequest)request).Element);
                            result = new CreateResponse
                            {
                                IsSuccess = true
                            };
                            break;
                        case "delete":
                            request = JsonConvert.DeserializeObject<DeleteRequest>(jsonRequest);
                            instanceService.Disk.Delete(request.Path);
                            result = new DeleteResponse
                            {
                                IsSuccess = true
                            };
                            break;
                        case "copy":
                            request = JsonConvert.DeserializeObject<CopyRequest>(jsonRequest);
                            instanceService.Disk.Copy(request.Path,((CopyRequest)request).PathDestination);
                            result = new CopyResponse
                            {
                                IsSuccess = true
                            };
                            break;
                        case "move":
                            request = JsonConvert.DeserializeObject<CopyRequest>(jsonRequest);
                            instanceService.Disk.Move(request.Path, ((MoveRequest)request).PathDestination);
                            result = new MoveResponse
                            {
                                IsSuccess = true
                            };
                            break;
                        case "print":
                            request = JsonConvert.DeserializeObject<GetTreeRequest>(jsonRequest);
                            instanceService.Disk.GetTree(request.Path);
                            result = new GetTreeResponse
                            {
                                IsSuccess = true
                            };
                            break;
                    }

                }
                catch (Exception ex)
                {
                    result = new BaseResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = ex.Message
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
