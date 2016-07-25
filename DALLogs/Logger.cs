using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DALLogs
{
    [Table("Loggers")]
    public class Logger
    {
        public Logger()
        {
        }

        public Logger(string type)
        {
            Type = type;
        }
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public string Type { get; set; }

        public string Message { get; set; }
    }
}
