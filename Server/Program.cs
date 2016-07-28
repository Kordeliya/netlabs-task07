using Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace Server
{
    public class Program
    {     
        static void Main(string[] args)
        {
            Trace.TraceInformation("StartServer");
            string url = "http://localhost:6740/";
            DbTraceListener list = new DbTraceListener("loggerSetting");
            list.Write(new object(),"");
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(url);
            try
            {
                listener.Start();
                while (listener.IsListening)
                {
                    byte[] responseBytes = null;
                    HttpListenerContext context = listener.GetContext();
                    HttpListenerRequest request = context.Request;
                    if (request.HttpMethod == "POST")
                    {
                        responseBytes = ServerHelper.GetResponseBytes(request);
                        context.Response.ContentType = @"application/json";
                        context.Response.ContentLength64 = responseBytes.Length;
                    }
                    using (Stream output = context.Response.OutputStream)
                    {
                        output.Write(responseBytes, 0, responseBytes.Length);
                    }
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                listener.Close();
            }
        }

    }
}
