using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DALLogs
{
    [Table("LoggerMessages")]
    public class LoggerMessages
    {
        public LoggerMessages()
        {
        }

        public LoggerMessages(string type)
        {
            Type = type;
        }
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public string Type { get; set; }

        public string Message { get; set; }
    }
}
