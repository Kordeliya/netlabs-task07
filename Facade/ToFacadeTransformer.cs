using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualFileSystem.Entities;

namespace Facade
{
    internal static class ToFacadeTransformer
    {
        public static FacadeFolder GetFacadeFolder(Folder folder)
        {
            return new FacadeFolder
            {
                Files = folder.Files.Select(file => GetFacadeFile(file)).ToList(),
                Directories = folder.Directories.Select(dir=>GetFacadeFolder(dir)).ToList()
            };
        }


        public static FacadeFileItem GetFacadeFile(FileItem file)
        {
            return new FacadeFileItem
            {
                Name = file.Name
            };
        }
    }
}
