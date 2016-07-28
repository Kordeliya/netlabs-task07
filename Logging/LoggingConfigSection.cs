using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    /// <summary>
    /// Класс описания прослушивателя в конфиге
    /// </summary>
    public class LoggingConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("nCall")]
        public int NCall
        {
            get
            {
                return (int)base["nCall"];
            }
        }

        [ConfigurationProperty("nameConnectionString")]
        public string NameConnectionString
        {
            get
            {
                return (string)base["nameConnectionString"];
            }
        }

    }
}
