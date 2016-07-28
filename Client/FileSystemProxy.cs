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
    public class FileSystemProxy : IVirtualFileSystem
    {
        private static ClientConnection _client;

        public FileSystemProxy()
        {
        }

        public void Create(FileSystemPath path, FileSystemServices.Entities.FileSystemElement element)
        {
            CreateRequest request = new CreateRequest
                                    {
                                        Path = path,
                                        Element = element
                                    };
            CreateResponse response = SendMessage<CreateRequest, CreateResponse>(request, "create");
            if (response != null && !response.IsSuccess)
                Console.WriteLine(">{0}",response.ErrorMessage);
        }

        public void Delete(FileSystemPath path)
        {
            DeleteRequest request = new DeleteRequest
            {
                Path = path
            };
            DeleteResponse response = SendMessage<DeleteRequest, DeleteResponse>(request, "delete");
            if (response != null && !response.IsSuccess)
                Console.WriteLine(">{0}", response.ErrorMessage);
        }

        public void Copy(FileSystemPath pathSource, FileSystemPath pathDestination)
        {
            CopyRequest request = new CopyRequest
            {
                Path = pathSource,
                PathDestination = pathDestination
            };
            CopyResponse response = SendMessage<CopyRequest, CopyResponse>(request, "copy");
            if (response != null && !response.IsSuccess)
                Console.WriteLine(">{0}", response.ErrorMessage);
        }

        public void Move(FileSystemPath pathSource, FileSystemPath pathDestination)
        {
            MoveRequest request = new MoveRequest
            {
                Path = pathSource,
                PathDestination = pathDestination
            };
            MoveResponse response = SendMessage<MoveRequest, MoveResponse>(request, "copy");
            if (response != null && !response.IsSuccess)
                Console.WriteLine(">{0}", response.ErrorMessage);
        }

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
