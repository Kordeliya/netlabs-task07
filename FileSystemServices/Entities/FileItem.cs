using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemServices.Entities
{
    /// <summary>
    /// Элемент файловой системы "Файл"
    /// </summary>
    [Serializable]
    public class FileItem : FileSystemElement
    {
        public FileItem()
            :base()
        {
        }

        public FileItem(string name)
            : base(name)
        {
        }
    }
}
