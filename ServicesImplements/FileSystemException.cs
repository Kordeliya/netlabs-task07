using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ServicesImplements
{
    /// <summary>
    /// Класс исключения с необходимыми сообщениями об ошибках
    /// </summary>
    [Serializable]
    public class FileSystemException : Exception
    {
        public FileSystemException(string message)
            : base(message)
        {
        }

        public FileSystemException(SerializationInfo info, StreamingContext context)
            : base (info, context)
        {
        }

        public FileSystemException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
