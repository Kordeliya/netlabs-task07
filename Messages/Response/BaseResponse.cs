using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    /// <summary>
    /// Класс базового ответа
    /// </summary>
    [Serializable]
    public abstract class BaseResponse
    {
        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }
    }
}
