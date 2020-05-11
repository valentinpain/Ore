using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ore.Models.DatabaseConnection
{
    /// <summary>
    /// A class that contains the basics informations in clear to instantiate the database connection
    /// </summary>
    public class DBUtils
    {
        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// Contains the database informations and uses the <c>DBSQLServerUtils</c> class to format the string
        /// </summary>
        /// <returns>The formatted string for the database connection</returns>
        public static SqlConnection GetDBConnection()
        {
            string datasource = @"DESKTOP-259SVQ6";
            string database = "Ore";
            string username = "valentin";
            string password = "verniapain27";

            return DBSQLServerUtils.GetDBConnectionServer(datasource, database, username, password);
        }

        #endregion
    }
}
