using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALLogs
{
    public class MappingContext : DbContext
    {
        public DbSet<LoggerMessages> Logger { get; set; }

        #region Constructors

        public MappingContext()
        {
        }

        public MappingContext(string connString)
            : base(connString)
        {
        }

        public MappingContext(System.Data.Common.DbConnection connection)
            : base(connection, true)
        {
        }

        #endregion
    }
}
