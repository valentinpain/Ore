using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ore.Models.DatabaseConnection
{
    public class DBUtils
    {
        public static SqlConnection GetDBConnection()
        {
            string datasource = @"DESKTOP-259SVQ6";

            string database = "Ore";
            string username = "valentin";
            string password = "verniapain27";

            return DBSQLServerUtils.GetDBConnection(datasource, database, username, password);
        }
    }
}
