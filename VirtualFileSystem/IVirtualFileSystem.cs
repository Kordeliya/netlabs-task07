using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualFileSystem.Entities;

namespace VirtualFileSystem
{
    /// <summary>
    /// Интерфейс (контракт работы файловой системы)
    /// </summary>
    interface IVirtualFileSystem
    {
        void CreateFile(string path);

        void CreateDirectory(string path);

        void CopyFile(string sourceFileName, string destFileName, FileItem copyFile);

        void CopyDirectory(string sourceFolderName, string destFolderName, Folder directory);

       // void MoveFile(string sourceFileName, string destFileName);

       // void MoveDirectory(string sourceFolderName, string destFolderName);

        void DeleteFile(string path);

        void DeleteDirectory(string path, bool recursive);

        Folder GetFolder(string path);
    }
}
