using DALLogs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    /// <summary>
    /// Класс прослушивателя пишущий в базу
    /// </summary>
    public class DbTraceListener : TraceListener
    {
        MappingContext _context = null;
        private List<LoggerMessages> _newArticle = new List<LoggerMessages>();
        private int NCall { get; set; }

        public DbTraceListener(string configSectionName)
        {
            var config = (ConfigurationManager.GetSection(configSectionName) as LoggingConfigSection);
            NCall = config.NCall;
            var nameConn = config.NameConnectionString;
            var connectionString = ConfigurationManager.ConnectionStrings[nameConn].ConnectionString;
            _context = new MappingContext(connectionString);
        }

        public override void Write(string message)
        {
            var newLine = new LoggerMessages(message);
            _newArticle.Add(newLine);
        }

        public override void WriteLine(string message)
        {
            _newArticle.Last().Message = message;
            _newArticle.Last().CreateDate = DateTime.Now;

            if (NCall == _newArticle.Count())
            {
                _context.Logger.AddRange(_newArticle);
                _context.SaveChanges();
                _newArticle = new List<LoggerMessages>();
            }
        }

        ~DbTraceListener()
        {
            //if (_newArticle != null && _newArticle.Count > 0)
            //{
            //    _context.Logger.AddRange(_newArticle);
            //    _context.SaveChanges();
            //}
        }
    }
}
