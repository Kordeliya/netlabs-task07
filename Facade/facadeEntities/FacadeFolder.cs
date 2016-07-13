using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
     /// <summary>
    /// Класс элемента папка
    /// </summary>
    public class FacadeFolder
    {

        public string Name { get; set; }

        public List<FacadeFolder> Directories { get; set; }

        public List<FacadeFileItem> Files { get; set; }
    }
}
