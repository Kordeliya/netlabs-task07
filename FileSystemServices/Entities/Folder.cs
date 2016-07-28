using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemServices.Entities
{
    /// <summary>
    /// Элемент файловой системы "Папка"
    /// </summary>
    [Serializable]
    public class Folder : FileSystemElement
    {

        public Folder()
            : base()
        {
        }

        public Folder(string name)
            : base(name)
        {
            Elements = new List<FileSystemElement>();
        }

        /// <summary>
        /// Лист элементов
        /// </summary>
        public List<FileSystemElement> Elements { get; set; }
    }
}
