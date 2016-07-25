using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [Serializable]
    public class ResponseFileService
    {
        public bool IsSuccess { get; set; }

        public string Error { get; set; }
    }
}
