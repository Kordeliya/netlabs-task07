using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualFileSystem;
using VirtualFileSystem.Entities;

namespace Facade
{
    /// <summary>
    /// Фасад для работы с виртуальной файловой системой
    /// </summary>
    public class Facade
    {
        private static readonly object _instanceLock =  new object();
        private static volatile Facade _instance;
        private VirtualDisk _disk;

        private Facade()
        {
            _disk = new VirtualDisk(FacadeHelper.GetDiskName());
        }
        /// <summary>
        /// Инстанс фасада
        /// </summary>
        /// <returns></returns>
        public static Facade GetInstance()
        {
            if (_instance == null)
            {
                lock (_instanceLock)
                    if (_instance == null)
                    {
                        Trace.TraceInformation("CreateInstance Facade!");
                        _instance = new Facade();
                    }
            }
            return _instance;
        }

        /// <summary>
        /// Попытка создать файл
        /// </summary>
        /// <param name="path">путь файла</param>
        /// <param name="currentDirectory">текущее положение пользователя в вирт файловой системе</param>
        /// <param name="errorMessage">сообщение об ошибке если операция не удалась</param>
        /// <returns></returns>
        public bool TryCreateFile(string path, string currentDirectory, out string errorMessage)
        {
            errorMessage = default(string);
            try
            {
                string dir;
                if (FacadeHelper.CheckIsFullPath(path))
                    dir = FacadeHelper.GetFullDirPath(path);
                else
                    dir = FacadeHelper.GetValidateDirPath(currentDirectory, path);
                _disk.CreateFile(dir);
                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                errorMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        ///  Попытка создать директорию
        /// </summary>
        /// <param name="path">путь папки</param>
        /// <param name="currentDirectory">текущее положение пользователя в вирт файловой системе</param>
        /// <param name="errorMessage">сообщение об ошибке если операция не удалась</param>
        /// <returns></returns>
        public bool TryCreateDirectory(string path, string currentDirectory, out string errorMessage)
        {
            errorMessage = default(string);
            try
            {
                string dir;
                if (FacadeHelper.CheckIsFullPath(path))
                    dir = FacadeHelper.GetFullDirPath(path);
                else
                    dir = FacadeHelper.GetValidateDirPath(currentDirectory, path);
                _disk.CreateDirectory(dir);
                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                errorMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Попытка копирования
        /// </summary>
        /// <param name="source">источник копирования</param>
        /// <param name="destination">точка назначения</param>
        /// <param name="currentDirectory">текущая директория</param>
        /// <param name="errorMessage">сообщение об ошибке если операция не удалась</param>
        /// <returns></returns>
        public bool TryCopy(string source, string destination, string currentDirectory, out string errorMessage)
        {
            errorMessage = default(string);
            try
            {
                string sourceFull = String.Empty;
                string destinationFull = String.Empty;
                if (FacadeHelper.CheckIsFullPath(source))
                    sourceFull =  FacadeHelper.GetValidateFilePath(source);
                else
                    sourceFull = FacadeHelper.GetValidateFilePath(currentDirectory, source);

                if (FacadeHelper.CheckIsFullPath(destination))
                    destinationFull = FacadeHelper.GetFullDirPath(destination);
                else
                    destinationFull = FacadeHelper.GetValidateDirPath(currentDirectory, destination);


                if (FacadeHelper.IsSourceFile(sourceFull))
                    _disk.CopyFile(sourceFull, destinationFull);
                else
                    _disk.CopyDirectory(FacadeHelper.GetFullDirPath(sourceFull), destinationFull);
                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                errorMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Попытка перемещения
        /// </summary>
        /// <param name="source">источник копирования</param>
        /// <param name="destination">точка назначения</param>
        /// <param name="currentDirectory">текущая директория</param>
        /// <param name="errorMessage">сообщение об ошибке если операция не удалась</param>
        /// <returns></returns>
        public bool TryMove(string source, string destination, string currentDirectory, out string errorMessage)
        {
            errorMessage = default(string);
            try
            {
                string sourceFull = String.Empty;
                string destinationFull = String.Empty;
                if (FacadeHelper.CheckIsFullPath(source))
                    sourceFull = source;
                else
                    sourceFull = FacadeHelper.GetValidateFilePath(currentDirectory, source);

                if (FacadeHelper.CheckIsFullPath(destination))
                    destinationFull = FacadeHelper.GetFullDirPath(destination);
                else
                    destinationFull = FacadeHelper.GetValidateDirPath(currentDirectory, destination);

                
                if (FacadeHelper.IsSourceFile(sourceFull))
                {
                    //_disk.MoveFile(sourceFull, destinationFull);
                    _disk.CopyFile(sourceFull, destinationFull);
                    _disk.DeleteFile(sourceFull);
                }
                else
                {
                    _disk.CopyDirectory(sourceFull, destinationFull);
                    _disk.DeleteDirectory(sourceFull, true);
                    //_disk.MoveDirectory(FacadeHelper.GetFullDirPath(sourceFull), destinationFull);
                }
                    return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                errorMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Попытка удаления файла
        /// </summary>
        /// <param name="path">путь файла</param>
        /// <param name="currentDirectory">текущее положение пользователя в вирт файловой системе</param>
        /// <param name="errorMessage">сообщение об ошибке если операция не удалась</param>
        /// <returns></returns>
        public bool TryDeleteFile(string path, string currentDirectory, out string errorMessage)
        {
            errorMessage = default(string);
            try
            {

                string pathToFile;
                if (FacadeHelper.CheckIsFullPath(path))
                    pathToFile = FacadeHelper.GetValidateFilePath(path);
                else
                    pathToFile = FacadeHelper.GetValidateFilePath(currentDirectory, path);

                _disk.DeleteFile(pathToFile);
                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                errorMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Попытка удаления диреткории
        /// </summary>
        /// <param name="path">путь к директории/param>
        /// <param name="currentDirectory">текущее положение пользователя в вирт файловой системе</param>
        /// <param name="all">флаг указывающий удалять ли директорию если в ней имеются поддиреткории</param>
        /// <param name="errorMessage">сообщение об ошибке если операция не удалась</param>
        /// <returns></returns>
        public bool TryDeleteDirectory(string path, string currentDirectory, bool all, out string errorMessage)
        {
            errorMessage = default(string);
            try
            {
                string directory;
                if (FacadeHelper.CheckIsFullPath(path))
                    directory = FacadeHelper.GetFullDirPath(path);
                else
                    directory = FacadeHelper.GetValidateDirPath(currentDirectory, path);
                _disk.DeleteDirectory(directory, all);
                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                errorMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Попытка получения дерева директорий и файлов
        /// </summary>
        /// <param name="path">путь</param>
        /// <param name="currentDirectory">текущая директория</param>
        /// <param name="folder">элемент папки если получения прошло успешно</param>
        /// <param name="errorMessage">сообщение об ошибке если операция не удалась</param>
        /// <returns></returns>
        public bool TryGetTree(string path, string currentDirectory, out FacadeFolder folder, out string errorMessage)
        {
            errorMessage = default(string);
            folder = null;
            try
            {
                string directory;
                if (FacadeHelper.CheckIsFullPath(path))
                    directory = FacadeHelper.GetFullDirPath(path);
                else
                    directory = FacadeHelper.GetValidateDirPath(currentDirectory, path);
                Folder folderVirtual = _disk.GetFolder(directory);
                folder = ToFacadeTransformer.GetFacadeFolder(folderVirtual);
                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                errorMessage = ex.Message;
                return false;
            }
        }

    }
}
