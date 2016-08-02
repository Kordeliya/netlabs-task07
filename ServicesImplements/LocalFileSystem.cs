using FileSystemServices;
using FileSystemServices.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesImplements
{
    /// <summary>
    /// Класс реализующий виртуальный файловый сервер
    /// </summary>
    public class LocalFileSystem : IVirtualFileSystem
    {
        public LocalFileSystem()
        {
            Elements = new List<FileSystemElement>();
        }

        /// <summary>
        /// Список элементов в локальной файловой системе
        /// </summary>
        public List<FileSystemElement> Elements { get; private set; }


        /// <summary>
        /// Создание элемента в файловой системе
        /// </summary>
        /// <param name="path">путь</param>
        /// <param name="element">элемент</param>
        public void Create(Uri path, FileSystemElement element)
        {
            Trace.TraceInformation("Create: {0}", path);
            try
            {
                List<string> segments = GetSegments(path).ToList();
                if (segments.Count() == 1)
                {
                    if (Elements.Where(e => e.Name == segments[0]).FirstOrDefault() == null)
                    {
                        Elements.Add(element);
                    }
                    else
                    {
                        throw new FileSystemException("Элемент с таким названием не существует по указанному пути");
                    }
                }
                else if (segments.Count() > 1)
                {
                    FileSystemElement elem = Elements.Where(e => e.Name == segments[0]).FirstOrDefault();
                    for (var i = 2; i < segments.Count(); i++)
                    {
                        if (elem != null && elem is Folder)
                        {
                            elem = ((Folder)elem).Elements.Where(e => e.Name == segments[i - 1]).FirstOrDefault();
                        }
                        else
                        {
                            throw new FileSystemException("Невозможно выполнить действие.Не существует указанного пути");
                        }
                    }
                    if (elem != null)
                    {
                        if (((Folder)elem).Elements.Where(e => e.Name == element.Name).FirstOrDefault() == null)
                        {
                            element.ModifyDate = DateTime.Now;
                            ((Folder)elem).Elements.Add(element);
                        }
                        else
                        {
                            throw new FileSystemException("Элемент с таким названием уже существует по указанному пути");
                        }
                    }
                    else
                    {
                        throw new FileSystemException("Невозможно выполнить действие.Не существует указанного пути");
                    }

                }
                else
                {
                    throw new FileSystemException("Элемент с таким названием не существует по указанному пути");
                }
            }
            catch (InvalidCastException ex)
            {
                throw new FileSystemException("Неверно прописанный путь");
            }
        }

        /// <summary>
        /// Удалить элемент
        /// </summary>
        /// <param name="path">путь</param>
        public void Delete(Uri path)
        {
            Trace.TraceInformation("Delete: {0}", path);
            FileSystemElement element;
            try
            {
                List<string> segments = GetSegments(path).ToList();
                if (segments.Count() == 1)
                {
                    element = Elements.Where(e => e.Name == segments[0]).FirstOrDefault();
                    if (element != null)
                    {
                        Elements.Remove(element);
                    }
                    else
                    {
                        throw new FileSystemException("Элемент с таким названием не существует по указанному пути");
                    }
                }
                else if (segments.Count() > 1)
                {
                    var elementParent = Elements.Where(e => e.Name == segments[0]).FirstOrDefault();
                    element = Elements.Where(e => e.Name == segments[0]).FirstOrDefault();
                    for (var i = 2; i <= segments.Count(); i++)
                    {
                        if (element != null && elementParent is Folder)
                        {
                            element = ((Folder)elementParent).Elements.Where(e => e.Name == segments[i - 1]).FirstOrDefault();
                        }
                        else
                        {
                            throw new FileSystemException("Невозможно выполнить действие.Не существует указанного пути");
                        }
                        if (i != (segments.Count()))
                        {
                            elementParent = element;
                        }

                    }
                    if (elementParent != null && element != null)
                    {
                        ((Folder)elementParent).Elements.Remove(element);
                    }
                    else
                    {
                        throw new FileSystemException("Невозможно выполнить действие.Не существует указанного пути");
                    }
                }
                else
                {
                    throw new FileSystemException("Элемент с таким названием не существует по указанному пути");
                }
            }
            catch (InvalidCastException ex)
            {
                throw new FileSystemException("Неверно прописанный путь");
            }
        }

        /// <summary>
        /// Копирование элемента
        /// </summary>
        /// <param name="pathSource">путь к источнику</param>
        /// <param name="pathDestination">путь к месту назначения</param>
        public void Copy(Uri pathSource, Uri pathDestination)
        {
            Trace.TraceInformation("Copy from {0} to {1}", pathSource, pathDestination);
            Uri newUri = null;
            FileSystemElement element;
            try
            {
                List<string> segments = GetSegments(pathSource).ToList();
                if (segments.Count() == 1)
                {
                    element = Elements.Where(e => e.Name == segments[0]).FirstOrDefault();

                    if (element == null)
                    {
                        throw new FileSystemException("Элемент с таким названием не существует по указанному пути");
                    }
                }
                else if (segments.Count() > 1)
                {
                    var elementParent = Elements.Where(e => e.Name == segments[0]).FirstOrDefault();
                    element = Elements.Where(e => e.Name == segments[0]).FirstOrDefault();
                    for (var i = 2; i <= segments.Count(); i++)
                    {
                        if (element != null && elementParent is Folder)
                        {
                            element = ((Folder)elementParent).Elements.Where(e => e.Name == segments[i - 1]).FirstOrDefault();
                        }
                        else
                        {
                            throw new FileSystemException("Невозможно выполнить действие.Не существует указанного пути");
                        }
                        if (i != (segments.Count()))
                        {
                            elementParent = element;
                        }

                    }
                    if (elementParent == null && element == null)
                    {
                        throw new FileSystemException("Элемент с таким названием не существует по указанному пути");
                    }
                }
                else
                {
                    throw new FileSystemException("Элемент с таким названием не существует по указанному пути");
                }
                //if (element is FileItem)
                //    newUri = pathDestination;
                //else if(element is Folder)
                newUri = new Uri(String.Format("{0}/{1}", pathDestination.ToString(), element.Name), UriKind.Relative);
                    
                this.Create(newUri, element);

            }
            catch (InvalidCastException ex)
            {
                throw new FileSystemException("Неверно прописанный путь");
            }
        }

        /// <summary>
        /// Перемещение
        /// </summary>
        /// <param name="pathSource">путь к источнику</param>
        /// <param name="pathDestination">путь к месту назначения</param>
        public void Move(Uri pathSource, Uri pathDestination)
        {
            Trace.TraceInformation("Move from {0} to {1}", pathSource, pathDestination);
            this.Copy(pathSource, pathDestination);
            this.Delete(pathSource);
        }

        /// <summary>
        /// Получение дерева
        /// </summary>
        /// <param name="path">путь к элементу</param>
        /// <returns></returns>
        public FileSystemElement GetTree(Uri path)
        {
            Trace.TraceInformation("GetTree: {0}", path);
            FileSystemElement element;
            try
            {
                List<string> segments = GetSegments(path).ToList();
                if (segments.Count() == 1)
                {
                    element = Elements.Where(e => e.Name == segments[0]).FirstOrDefault();

                    if (element != null && element is Folder)
                        return element;
                    else
                        throw new FileSystemException("Элемент с таким названием не существует по указанному пути");
                }
                else if (segments.Count() > 1)
                {
                    element = Elements.Where(e => e.Name == segments[0]).FirstOrDefault();
                    for (var i = 2; i < segments.Count(); i++)
                    {
                        if (element != null && element is Folder)
                            element = ((Folder)element).Elements.Where(e => e.Name == segments[i]).FirstOrDefault();
                        else
                            throw new FileSystemException("Невозможно выполнить действие.Не существует указанного пути");
                    }
                    if (element != null && element is Folder)
                        return element;
                    else
                        throw new FileSystemException("Элемент с таким названием не существует по указанному пути");
                }
                else
                    throw new FileSystemException("Элемент с таким названием не существует по указанному пути");
            }
            catch (InvalidCastException ex)
            {
                throw new FileSystemException("Неверно прописанный путь");
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string[] GetSegments(Uri path)
        {
            if (path != null)
            {
                return path.OriginalString.Split("/\\".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToArray();
            }
            else
            {
                return null;
            }
        }
    }
}
