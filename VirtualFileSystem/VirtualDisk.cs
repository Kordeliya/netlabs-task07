using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualFileSystem.Entities;

namespace VirtualFileSystem
{
    /// <summary>
    /// Виртуальный диск
    /// </summary>
    public class VirtualDisk : Folder
    {
        public VirtualDisk(string name) : base(name)
        {
        }
    }
}
