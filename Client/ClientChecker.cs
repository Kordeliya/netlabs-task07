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

        ///// <summary>
        ///// Проверка ответа от сервера
        ///// </summary>
        ///// <param name="response"></param>
        //public static void CheckResponse(HttpResponseMessage response)
        //{
        //    if (response.IsSuccessStatusCode)
        //    {
        //        ResponseFileService responseXml = new ResponseFileService();
        //        // response.Content
        //        XmlSerializer serializer = new XmlSerializer(typeof(ResponseFileService));
        //        Stream xmlStream = ((StreamContent)response.Content).ReadAsStreamAsync().Result;
        //        responseXml = (ResponseFileService)serializer.Deserialize(xmlStream);
        //        if (!responseXml.IsSuccess)
        //            Console.WriteLine(">{0}", responseXml.Error ?? "Произошла неизвестная ошибка");

        //    }
        //    else
        //        Console.WriteLine(">Произошла неизвестная ошибка");
        //}

        ///// <summary>
        ///// Проверка ответа
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="response"></param>
        ///// <returns></returns>
        //public static T CheckResponse<T>(HttpResponseMessage response)
        //{
        //    if (response.IsSuccessStatusCode)
        //    {
        //        ResponseFileService<T> responseXml = new ResponseFileService<T>();
        //        // response.Content
        //        XmlSerializer serializer = new XmlSerializer(typeof(ResponseFileService<T>));
        //        Stream xmlStream = ((StreamContent)response.Content).ReadAsStreamAsync().Result;
        //        responseXml = (ResponseFileService<T>)serializer.Deserialize(xmlStream);
        //        if (!responseXml.IsSuccess)
        //            Console.WriteLine(">{0}", responseXml.Error ?? "Произошла неизвестная ошибка");
        //        return responseXml.Data;
        //    }
        //    else
        //        Console.WriteLine(">Произошла неизвестная ошибка");
        //    return default(T);
        //}
    }
}
