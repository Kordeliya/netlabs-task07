using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    public class FacadeHelper
    {
        private static readonly string _virtualDiskName = "C:\\";
        /// <summary>
        /// Проверка является ли строка полным путем
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static bool CheckIsFullPath(string dir)
        {
            var template = _virtualDiskName.Remove(_virtualDiskName.LastIndexOf("\\")).ToLower();
            if (dir.ToLower().Contains(template))
                return true;
            else
                return false;
        }
        /// <summary>
        /// является ли источник файлом
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsSourceFile(string source)
        {
            if (source.Contains("."))
            {
                return true;
            }
            else
                return false;
        }

        public static string GetDiskName()
        {
            return _virtualDiskName;
        }

        public static string GetFullDirPath(string path)
        {
            path = path.Replace('/', '\\');
            return String.Format("{0}\\", path);
        }

        public static string GetValidateDirPath(string currentDirectory, string path)
        {
            path = path.Replace('/', '\\');
            return String.Format("{0}{1}\\", currentDirectory, path);
        }

        public static string GetValidateFilePath(string currentDirectory, string path)
        {
            path = path.Replace('/', '\\');
            return String.Format("{0}{1}", currentDirectory, path);
        }

        public static string GetValidateFilePath(string path)
        {
            return path.Replace('/', '\\');
        }
    }
}
