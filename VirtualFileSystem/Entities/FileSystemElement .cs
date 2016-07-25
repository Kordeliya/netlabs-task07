using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualFileSystem
{
    public class FileSystemElement
    {
        /// <summary>
        /// Имя элемента файловой системы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Дата модификации
        /// </summary>
        public DateTime ModifyDate { get; set; }
    }
}
