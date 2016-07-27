using FileSystemServices;
using FileSystemServices.Entities;
using Messages;
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
            BaseResponse response = SendMessage<CreateRequest, BaseResponse>(request);

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


        private static TResponse SendMessage<TRequest,TResponse>(TRequest request)
            where TRequest : BaseRequest
            where TResponse : BaseResponse
        {
           // HttpContent content =  HttpContent.
            using (_client = new ClientConnection())
            {
                

                HttpContent content;
                HttpResponseMessage response = _client.SendRequest(content).Result;
                ClientChecker.CheckResponse(response);
            }
            return _response;
        }
    
    }
}
