using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesImplements
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
                    message = "Неверно прописанный путь";
                    break;
                case 2:
                    message = "Невозможно выполнить действие.Не существует указанного пути";
                    break;
                case 3:
                    message = "Элемент с таким названием уже существует по указанному пути";
                    break;
                case 4:
                    message = "Элемент с таким названием не существует по указанному пути";
                    break;

            }
            Trace.TraceError("Произошла ошибка:{0}", message);
            return message;
        }
    }
}
