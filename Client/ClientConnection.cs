using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Configuration;
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
            string uri = ConfigurationManager.AppSettings["ServiceUri"];
            _client.BaseAddress = new Uri(uri);
            _client.MaxResponseContentBufferSize = 1024 * 1024 * 8;
        }
        /// <summary>
        /// Отправка запроса
        /// </summary>
        /// <param name="content">содержимое запроса</param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendRequest(HttpContent content, string uri)
        {
            _response = await _client.PostAsync(uri, content);
            return _response;
        }


        public void Dispose()
        {
            _response.Dispose();
            _client.Dispose();

        }
 

    }
}
