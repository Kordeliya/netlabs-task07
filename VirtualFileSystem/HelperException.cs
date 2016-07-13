using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualFileSystem
{
    /// <summary>
    /// Класс исключения с необходимыми сообщениями об ошибках
    /// </summary>
    public class HelperException : Exception
    {
        public HelperException(string message) : base(message) { }

        public HelperException(int idMessage)
            : this(GetMessage(idMessage)) { }

        private static string GetMessage(int idMessage)
        {
            string message = String.Empty;
            switch (idMessage)
            {
                case 1:
                    message = "Не существует каталога";
                    break;
                case 2:
                    message = "Директории с таким названием не существует";
                    break;
                case 3:
                    message = "Невозможно удалить. Выбранная директория  соддержит поддериктории";
                    break;
                case 4:
                    message = "Файл с таким названием уже существует по указанному пути";
                    break;
                case 5:
                    message = "Указанного файла не существует";
                    break;
                case 6:
                    message = "Невозможно выполнить действие.Не существует указанного пути";
                    break;
                case 7:
                    message = "Невозможно создать директорию с названием уже существующей директории";
                    break;
                case 8:
                    message = "Директория с таким названием уже существует по указанному пути";
                    break;
                default:
                    message = "Неизвестная ошибка";
                    break;
            }
            Trace.TraceError("Произошла ошибка:{0}", message);
            return message;
        }
    }

}
