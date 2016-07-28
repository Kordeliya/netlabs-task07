using Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Client
{
    public static class ClientChecker
    {
        /// <summary>
        /// Проверка ответа от сервиса
        /// </summary>
        /// <typeparam name="TResponse">тип ответа</typeparam>
        /// <param name="response">ответ от сервиса</param>
        /// <returns></returns>
        public static TResponse CheckResponse<TResponse>(HttpResponseMessage response)
            where TResponse : BaseResponse
        {
            if (response.IsSuccessStatusCode)
            {
                var buffer = response.Content.ReadAsByteArrayAsync().Result;
                string json = ASCIIEncoding.UTF8.GetString(buffer);
                TResponse jsonResponse = JsonConvert.DeserializeObject<TResponse>(json);

                return jsonResponse;
            }
            else
            {
                Console.WriteLine(">Произошла неизвестная ошибка");
                return default(TResponse);
            }
        }
    }
}
