using FileSystemServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    /// <summary>
    /// Запрос на копирование
    /// </summary>
    [Serializable]
    public class CopyRequest : BaseRequest
    {
        public FileSystemPath PathDestination { get; set; }
    }
}
