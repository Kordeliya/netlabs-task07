using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    /// <summary>
    /// Класс подключения для клиента
    /// </summary>
    public class ClientConnection : IDisposable
    {
        HttpClient _client;
        HttpResponseMessage _response;

        public ClientConnection()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:6740");
            _client.MaxResponseContentBufferSize = 1024 * 1024 * 8;
        }

        public void Dispose()
        {
             _response.Dispose();
            _client.Dispose();

        }
        /// <summary>
        /// Отправка запроса
        /// </summary>
        /// <param name="content"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendRequest(HttpContent content, string uri)
        {
            _response = await _client.PostAsync(uri, content);
            return _response;
        }

        //public async Task<HttpResponseMessage> SendRequest(HttpContent content)
        //{
        //    _response = await _client.PostAsync(String.Empty, content);
        //    return _response;
        //}  

    }
}
