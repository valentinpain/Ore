using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ore.Models.DatabaseConnection
{
	/// <summary>
	/// The class which instantiates the real database connection 
	/// </summary>
    public class DBConnection
    {
		#region Properties

		#endregion

		#region Methods

		/// <summary>
		/// Instantiate and open the database connection
		/// </summary>
		/// <returns>The real database connection</returns>
		public static SqlConnection openConnection()
        {
			SqlConnection connection = DBUtils.GetDBConnection();

			try
			{ 
				connection.Open();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: " + e.Message);
			}

			return connection;
		}

        #endregion
    }
}
