using FileSystemServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    /// <summary>
    /// ЗАпрос на перемещение
    /// </summary>
    [Serializable]
    public class MoveRequest : BaseRequest
    {
        public FileSystemPath PathDestination { get; set; }
    }
}
