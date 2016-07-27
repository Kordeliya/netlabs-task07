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
            BaseResponse response = SendMessage<CreateRequest, BaseResponse>(request, "create");

        }

        public void Delete(FileSystemPath path)
        {
            throw new NotImplementedException();
        }

        public void Copy(FileSystemPath pathSource, FileSystemPath pathDestination)
        {
            throw new NotImplementedException();
        }

        public void Move(FileSystemPath pathSource, FileSystemPath pathDestination)
        {
            throw new NotImplementedException();
        }

        public FileSystemElement GetTree(FileSystemPath path)
        {
            throw new NotImplementedException();
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
               // response = ClientChecker.CheckResponse(_client.SendRequest(content).Result);
            }
            return response;
        }
    
    }
}
