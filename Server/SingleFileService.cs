using ServicesImplements;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Класс синглтон для файловой системы
    /// </summary>
    public class SingleFileService
    {
        private static readonly object _instanceLock = new object();
        private static volatile SingleFileService _instance;

        public LocalFileSystem Disk {get; private set;}

        private SingleFileService()
        {
            Disk = new LocalFileSystem();
        }
        /// <summary>
        /// Инстанс фасада
        /// </summary>
        /// <returns></returns>
        public static SingleFileService GetInstance()
        {
            if (_instance == null)
            {
                lock (_instanceLock)
                    if (_instance == null)
                    {
                        Trace.TraceInformation("CreateInstance Facade!");
                        _instance = new SingleFileService();
                    }
            }
            return _instance;
        }

    }
}
