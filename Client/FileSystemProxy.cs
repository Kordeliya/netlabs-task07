using FileSystemServices;
using FileSystemServices.Entities;
using Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    /// <summary>
    /// Прокси для файлового сервиса
    /// </summary>
    public class FileSystemProxy : IVirtualFileSystem
    {
        private static ClientConnection _client;

        public FileSystemProxy()
        {
        }

        /// <summary>
        /// Создание элемента
        /// </summary>
        /// <param name="path"></param>
        /// <param name="element"></param>
        public void Create(FileSystemPath path, FileSystemServices.Entities.FileSystemElement element)
        {
            CreateRequest request = new CreateRequest
                                    {
                                        Path = path,
                                        Element = element
                                    };
            CreateResponse response = SendMessage<CreateRequest, CreateResponse>(request, "create");
            if (response != null && !response.IsSuccess)
            {
                Console.WriteLine(">{0}", response.ErrorMessage);
            }
        }

        /// <summary>
        /// Удаление элемента
        /// </summary>
        /// <param name="path"></param>
        public void Delete(FileSystemPath path)
        {
            DeleteRequest request = new DeleteRequest
            {
                Path = path
            };
            DeleteResponse response = SendMessage<DeleteRequest, DeleteResponse>(request, "delete");
            if (response != null && !response.IsSuccess)
            {
                Console.WriteLine(">{0}", response.ErrorMessage);
            }
        }

        /// <summary>
        /// Копирование
        /// </summary>
        /// <param name="pathSource">источник</param>
        /// <param name="pathDestination">пункт назначения</param>
        public void Copy(FileSystemPath pathSource, FileSystemPath pathDestination)
        {
            CopyRequest request = new CopyRequest
            {
                Path = pathSource,
                PathDestination = pathDestination
            };
            CopyResponse response = SendMessage<CopyRequest, CopyResponse>(request, "copy");
            if (response != null && !response.IsSuccess)
            {
                Console.WriteLine(">{0}", response.ErrorMessage);
            }
        }

        /// <summary>
        /// Перемещение
        /// </summary>
        /// <param name="pathSource">источник</param>
        /// <param name="pathDestination">назначение</param>
        public void Move(FileSystemPath pathSource, FileSystemPath pathDestination)
        {
            MoveRequest request = new MoveRequest
            {
                Path = pathSource,
                PathDestination = pathDestination
            };
            MoveResponse response = SendMessage<MoveRequest, MoveResponse>(request, "copy");
            if (response != null && !response.IsSuccess)
            {
                Console.WriteLine(">{0}", response.ErrorMessage);
            }
        }

        /// <summary>
        /// Получение дерева эелемента
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public FileSystemElement GetTree(FileSystemPath path)
        {
            GetTreeRequest request = new GetTreeRequest
            {
                Path = path,
            };
            GetTreeResponse response = SendMessage<GetTreeRequest, GetTreeResponse>(request, "copy");
            if (response != null && !response.IsSuccess)
            {
                Console.WriteLine(">{0}", response.ErrorMessage);
                return null;
            }
            else
            {
                return response.Element;
            }
        }


        /// <summary>
        /// Формирование запроса
        /// </summary>
        /// <typeparam name="TRequest">тип запроса</typeparam>
        /// <typeparam name="TResponse">тип ответа</typeparam>
        /// <param name="request">запрос</param>
        /// <param name="command">ответ</param>
        /// <returns></returns>
        private static TResponse SendMessage<TRequest,TResponse>(TRequest request, string command)
            where TRequest : BaseRequest
            where TResponse : BaseResponse
        {
            TResponse response = null;
            using (_client = new ClientConnection())
            {
                var json = JsonConvert.SerializeObject(request);
                StringContent content = new StringContent(json);
                var resp = _client.SendRequest(content,command).Result;
                response = ClientChecker.CheckResponse<TResponse>(resp);
            }
            return response;
        }
    
    }
}
