using FileSystemServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemServices
{
    public interface IVirtualFileSystem
    {
        /// <summary>
        /// Создание элемента
        /// </summary>
        /// <param name="path">путь</param>
        /// <param name="element"></param>
        void Create(Uri path, FileSystemElement element);

        /// <summary>
        /// Удаление элемента
        /// </summary>
        /// <param name="path">путь</param>
        /// <param name="element"></param>
        void Delete(Uri path);

        /// <summary>
        /// Копирование элемента
        /// </summary>
        /// <param name="pathSource">источник</param>
        /// <param name="pathDestination">точка назначения</param>
        void Copy(Uri pathSource, Uri pathDestination);

        /// <summary>
        /// Перемещение элемента
        /// </summary>
        /// <param name="pathSource">источник</param>
        /// <param name="pathDestination">точка назначения</param>
        void Move(Uri pathSource, Uri pathDestination);

        /// <summary>
        /// Получение древа
        /// </summary>
        /// <param name="path">путь</param>
        /// <returns></returns>
        FileSystemElement GetTree(Uri path);
    }
}
