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
    public class DbTraceListener : TraceListener
    {
        MappingContext _context = null;
        private List<Logger> _newArticle = new List<Logger>();


        private int NCall { get; set; }

        private int CurrentCall { get; set; }


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
            if (_newArticle.Count > 0)
            {
                string oldMessage = _newArticle.Last().Message;
                _newArticle.Last().Message = String.Format("{0}, {1}", oldMessage, message);
            }
            else
                this.WriteLine(message);
        }

        public override void WriteLine(string message)
        {
            _newArticle.Add(new Logger { Message = message, CreateDate = DateTime.Now });
            CurrentCall++;

            if (NCall == CurrentCall)
            {
                //запись в базу и затем CurrentCall обнуляется
                _context.Logger.AddRange(_newArticle);
                _context.SaveChanges();
                _newArticle = new List<Logger>();
                CurrentCall = 0;
            }
        }

        ~DbTraceListener()
        {
            if (_newArticle != null && _newArticle.Count > 0)
            {
                _context.Logger.AddRange(_newArticle);
                _context.SaveChanges();
            }
        }
    }
}
