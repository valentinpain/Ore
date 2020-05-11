using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ore.Models.DatabaseConnection
{
    /// <summary>
    /// A class with only one method named <c>GetDBConnection</c> who returns the connection string to instanciate the database connection
    /// </summary>
    public class DBSQLServerUtils
    {
        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// Use the informations of the <c>DBSQLServerUtils</c> class to create the connection string for the database connection
        /// </summary>
        /// <param name="datasource">Server name</param>
        /// <param name="database">Database name</param>
        /// <param name="username">User name</param>
        /// <param name="password">User password</param>
        /// <returns>The connection string</returns>
        public static SqlConnection GetDBConnectionServer(string datasource, string database, string username, string password)
        {
            string connectionString = @"Data Source=" + datasource + ";Initial Catalog="
                        + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;

            SqlConnection connection = new SqlConnection(connectionString);

            return connection;
        }

        #endregion
    }
}
