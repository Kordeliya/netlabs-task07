using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemServices.Entities
{
    [Serializable]
    public class FileSystemElement
    {
        public FileSystemElement()
        {
        }

        public FileSystemElement(string name)
        {
            Name = name;
            CreateDate = DateTime.Now;
        }

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
