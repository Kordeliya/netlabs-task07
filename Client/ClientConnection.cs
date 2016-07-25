using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
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

        public async Task<HttpResponseMessage> SendRequest(HttpContent content, string uri)
        {
            _response = await _client.PostAsync(uri, content);
            return _response;
        }

    }
}
