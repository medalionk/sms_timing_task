using log4net;
using log4net.Config;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsTimingTask
{
    class Program
    {
        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        private static readonly ILog logger;

        /// <summary>
        /// Initializes the <see cref="Program"/> class.
        /// </summary>
        static Program()
        {
            XmlConfigurator.Configure();
            logger = LogManager.GetLogger(typeof(Program));
        }
        static void Main(string[] args)
        {
        }
    }
}
