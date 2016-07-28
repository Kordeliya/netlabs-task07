using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileSystemServices
{
    /// <summary>
    /// Путь элемента в файловой системе
    /// </summary>
    [Serializable]
    public class FileSystemPath
    {
        public FileSystemPath(string path)
        {
            Path = WebUtility.UrlDecode(path);
        }

        /// <summary>
        /// Путь
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Составляющие пути
        /// </summary>
        public List<string> Segments
        {
            get
            {
                return Path.Split("/\\".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            }
        }
    }
}
