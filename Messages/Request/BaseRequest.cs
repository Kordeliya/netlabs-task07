using FileSystemServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    /// <summary>
    /// Базовый класс запроса
    /// </summary>
    [Serializable]
    public class BaseRequest
    {
        public FileSystemPath Path { get; set; }
    }
}
