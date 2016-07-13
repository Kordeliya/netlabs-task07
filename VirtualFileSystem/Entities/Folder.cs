using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualFileSystem.Entities
{
    /// <summary>
    /// Класс элемента папка
    /// </summary>
    public class Folder : IVirtualFileSystem
    {
        public Folder()
        {
            Directories = new List<Folder>();
            Files = new List<FileItem>();
        }

        public Folder(string name)
        {
            Name = name;
            Directories = new List<Folder>();
            Files = new List<FileItem>();
        }

        public string Name { get; set; }

        public List<Folder> Directories { get; set; }

        public List<FileItem> Files { get; set; }

        /// <summary>
        /// Создание файла
        /// </summary>
        /// <param name="path">путь</param>
        public void CreateFile(string path)
        {
            string[] partsOfPath = GetPartsOfPath(path);
            List<string> names = partsOfPath.ToList();
            path = DeleteFirstPartOfPath(path, names[0]);
            names.RemoveAt(0);
            if (names.Count > 1)
            {
                var directory = Directories.Where(d => d.Name == names.First()).FirstOrDefault();
                if (directory != null)
                    directory.CreateFile(path);
                else
                    throw new HelperException(6);
            }
            else
            {
                var name = partsOfPath.Last();
                if (Files.Where(d => d.Name == name).FirstOrDefault() == null)
                    Files.Add(new FileItem { Name = name });
                else
                    throw new HelperException(4);
            }
        }

        /// <summary>
        /// Создание директории
        /// </summary>
        /// <param name="path">путь</param>
        public void CreateDirectory(string path)
        {
            string[] partsOfPath = GetPartsOfPath(path);
            List<string> names = partsOfPath.ToList();
            path = DeleteFirstPartOfPath(path, names[0]);
            names.RemoveAt(0);
            if (names.Count > 1)
            {
                var directory = Directories.Where(d => d.Name == names.First()).FirstOrDefault();
                if (directory != null)
                    directory.CreateDirectory(path);
                else
                    throw new HelperException(6);
            }
            else
            {
                if (Directories.Where(d => d.Name == names.First()).FirstOrDefault() == null)
                    Directories.Add(new Folder(names.First()));
                else
                    throw new HelperException(7);
            }
        }

        /// <summary>
        /// Копирование файла
        /// </summary>
        /// <param name="sourceFileName">источник</param>
        /// <param name="destFileName">назначение</param>
        /// <param name="copyFile"></param>
        public void CopyFile(string sourceFileName, string destFileName, FileItem copyFile = default(FileItem))
        {
            if (copyFile == null)
            {
                string[] sourceParts = GetPartsOfPath(sourceFileName);
                List<string> namesSource = sourceParts.ToList();
                copyFile = FindFile(namesSource);
                if (copyFile == null)
                    throw new HelperException(5);
            }
            string[] destParts = GetPartsOfPath(destFileName);
            List<string> namesDest = destParts.ToList();
            destFileName = DeleteFirstPartOfPath(destFileName, namesDest[0]);
            namesDest.RemoveAt(0);
            if (namesDest.Count > 0)
            {
                var directory = Directories.Where(d => d.Name == namesDest.First()).FirstOrDefault();
                if (directory != null)
                    directory.CopyFile(sourceFileName, destFileName, copyFile);
                else
                    throw new HelperException(6);
            }
            else
            {
                if (Files.Where(d => d.Name == copyFile.Name).FirstOrDefault() == null)
                    Files.Add(copyFile);
                else
                    throw new HelperException(4);
            }
        }

        /// <summary>
        /// Копирование директории
        /// </summary>
        /// <param name="sourceFolderName">источник</param>
        /// <param name="destFolderName">назначение</param>
        /// <param name="directory"></param>
        public void CopyDirectory(string sourceFolderName, string destFolderName, Folder directory = default(Folder))
        {
            if (directory == null)
            {
                string[] sourceParts = GetPartsOfPath(sourceFolderName);
                List<string> namesSource = sourceParts.ToList();
                directory = FindDirectory(namesSource);
                if (directory == null)
                     throw new HelperException(5);                  
            }
            string[] destParts = GetPartsOfPath(destFolderName);
            List<string> namesDest = destParts.ToList();
            destFolderName = DeleteFirstPartOfPath(destFolderName,namesDest[0]);
            namesDest.RemoveAt(0);
            if (namesDest.Count > 1)
            {
                var dir = Directories.Where(d => d.Name == namesDest.First()).FirstOrDefault();
                if (dir!= null)
                    dir.CopyDirectory(sourceFolderName, destFolderName,directory);
                else
                    throw new HelperException(6);
            }
            else
            {
                if (Files.Where(d => d.Name == directory.Name).FirstOrDefault() == null)
                    Directories.Add(directory);
                else
                    throw new HelperException(4);
            }

        }

        ///// <summary>
        ///// Перемещение файла
        ///// </summary>
        ///// <param name="sourceFileName"></param>
        ///// <param name="destFileName"></param>
        //public void MoveFile(string sourceFileName, string destFileName)
        //{
        //    string[] destParts = GetPartsOfPath(destFileName);
        //    List<string> namesDest = destParts.ToList();
        //    destFileName = DeleteFirstPartOfPath(destFileName, namesDest[0]);
        //    namesDest.RemoveAt(0);
        //    if (namesDest.Count > 1)
        //    {
        //        var directory = Directories.Where(d => d.Name == namesDest.First()).FirstOrDefault();
        //        if (directory != null)
        //            directory.CopyFile(sourceFileName, destFileName);
        //        else
        //            throw new HelperException(6);
        //    }
        //    else
        //    {
        //        string[] sourceParts = GetPartsOfPath(sourceFileName);
        //        List<string> namesSource = sourceParts.ToList();
        //        FileItem file = FindFile(namesSource);
        //        if (file != null)
        //        {
        //            file.Name = namesSource.Last();
        //            if (Files.Where(d => d.Name == file.Name).FirstOrDefault() == null)
        //            {
        //                Files.Add(file);
        //                DeleteFile(sourceFileName);
        //            }
        //            else
        //                throw new HelperException(4);
        //        }
        //        else
        //            throw new HelperException(5);

        //    }
        //}

        ///// <summary>
        ///// Перемещение директории
        ///// </summary>
        ///// <param name="sourceFolderName"></param>
        ///// <param name="destFolderName"></param>
        //public void MoveDirectory(string sourceFolderName, string destFolderName)
        //{
        //    string[] destParts = GetPartsOfPath(destFolderName);
        //    List<string> namesDest = destParts.ToList();
        //    destFolderName = DeleteFirstPartOfPath(destFolderName, namesDest[0]);
        //    namesDest.RemoveAt(0);
        //    if (namesDest.Count > 1)
        //    {
        //        var directory = Directories.Where(d => d.Name == namesDest.First()).FirstOrDefault();
        //        if (directory != null)
        //            directory.CopyDirectory(sourceFolderName, destFolderName);
        //        else
        //            throw new HelperException(6);
        //    }
        //    else
        //    {
        //        string[] sourceParts = GetPartsOfPath(sourceFolderName);
        //        List<string> namesSource = sourceParts.ToList();
        //        Folder directory = FindDirectory(namesSource);
        //        if (directory != null)
        //        {
        //            directory.Name = namesSource.Last();
        //            if (Files.Where(d => d.Name == directory.Name).FirstOrDefault() == null)
        //            {
        //                Directories.Add(directory);
        //                DeleteDirectory(sourceFolderName, true);
        //            }
        //            else
        //                throw new HelperException(4);
        //        }
        //        else
        //            throw new HelperException(5);
        //    }
        //}

        /// <summary>
        /// Удаление файла
        /// </summary>
        /// <param name="path">путь</param>
        public void DeleteFile(string path)
        {
            string[] partsOfPath = GetPartsOfPath(path);
            List<string> namesPath = partsOfPath.ToList();
            path = DeleteFirstPartOfPath(path, partsOfPath[0]);
            namesPath.RemoveAt(0);
            if (namesPath.Count() > 1)
            {
                var directory = Directories.Where(d => d.Name == namesPath.First()).FirstOrDefault();
                if (directory != null)
                    directory.DeleteFile(path);
                else
                    throw new HelperException(6);
            }
            else
            {
                var name = namesPath.Last();
                var file = Files.Where(d => d.Name == name).FirstOrDefault();
                if (file != null)
                    Files.Remove(file);
                else
                    throw new HelperException(5);
            }
        }

        /// <summary>
        /// Удаление директории
        /// </summary>
        /// <param name="path">путь</param>
        /// <param name="recursive">Значение true позволяет удалить каталоги, подкаталоги и файлы по заданному path,
        /// в противном случае — значение false.</param>
        public void DeleteDirectory(string path, bool recursive)
        {
            string[] partsOfPath = GetPartsOfPath(path);
            path = DeleteFirstPartOfPath(path, partsOfPath[0]);
            List<string> namesPath = GetPartsOfPath(path).ToList();
            if (namesPath.Count() > 1)
            {
                var directory = Directories.Where(d => d.Name == namesPath.First()).FirstOrDefault();
                if (directory != null)
                    directory.DeleteDirectory(path, recursive);
                else
                    throw new HelperException(6);
            }
            else
            {
                var name = namesPath.Last();
                var dir = Directories.Where(d => d.Name == name).FirstOrDefault();
                if (dir != null)
                {
                    if (dir.Directories.Count > 0)
                        if (recursive)
                            Directories.Remove(dir);
                        else
                            throw new HelperException(3);
                    else
                        Directories.Remove(dir);
                }
                else
                    throw new HelperException(2);
            }
        }

        /// <summary>
        /// Ищем папку по пути
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Folder GetFolder(string path)
        {
            string[] partsOfPath = GetPartsOfPath(path);
            List<string> namesPath = partsOfPath.ToList();
            if (namesPath.Count() < 2)
                return this;
            Folder findedFolder = this.FindDirectory(namesPath);
            return findedFolder;
        }

        /// <summary>
        /// Поиск файла
        /// </summary>
        /// <param name="sources">путь по которому ищется</param>
        /// <returns></returns>
        private FileItem FindFile(List<string> sources)
        {
            sources.RemoveAt(0);
            if (sources.Count > 1)
            {                
                var directory = Directories.Where(d => d.Name == sources.First()).FirstOrDefault();
                if (directory != null)
                    return directory.FindFile(sources);
                else
                    throw new HelperException(6);
            }
            else
            {
                var file = Files.Where(d => d.Name == sources.First()).FirstOrDefault();
                return file;
            }

        }

        /// <summary>
        /// Поиск директории
        /// </summary>
        /// <param name="sources">путь по которому ищется</param>
        /// <returns></returns>
        private Folder FindDirectory(List<string> sources)
        {
            sources.RemoveAt(0);
            if (sources.Count > 1)
            {
                var directory = Directories.Where(d => d.Name == sources.First()).FirstOrDefault();
                if (directory != null)
                    return directory.FindDirectory(sources);
                else
                    throw new HelperException(6);
            }
            else
            {
                var dir = Directories.Where(d => d.Name == sources.First()).FirstOrDefault();
                return dir;
            }
        }

        /// <summary>
        /// Получение массива имен составляющий путь
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string[] GetPartsOfPath(string path)
        {
            return path.Split(@"\".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Удаление части пути
        /// </summary>
        /// <param name="path">путь</param>
        /// <param name="deletedParts">удаляемая часть</param>
        /// <returns></returns>
        private string DeleteFirstPartOfPath(string path, string deletedParts)
        {
            int index = path.IndexOf(deletedParts);
            return path.Remove(index, deletedParts.Length);
            //return path.Replace(String.Format(@"{0}\", deletedParts), "");
        }


    }
}
