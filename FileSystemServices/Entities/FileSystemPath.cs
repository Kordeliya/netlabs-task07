using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileSystemServices
{
    [Serializable]
    public class FileSystemPath
    {
        public FileSystemPath(string path)
        {
            Path = WebUtility.UrlDecode(path);
        }
        public string Path { get; private set; }

        public string[] Segments
        {
            get
            {
                return Path.Split("/\\".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            }
        }
    }
}
