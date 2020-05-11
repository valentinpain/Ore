using Ore.Models.DatabaseConnection;
using Ore.Models.EncryptingData;
using Ore.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ore.Models
{
	/// <summary>
	/// The model that contains elements to communicate with the database to retrieve some login data
	/// </summary>
    public class LoginModel
    {
        #region Properties

        #endregion

        #region Methods

		/// <summary>
		/// The method used to find the user in the database or not for the login feature
		/// </summary>
		/// <param name="username">The user name</param>
		/// <param name="password">The password of the user account</param>
		/// <returns></returns>
        public static int findUserId(string username, string password)
		{
			SqlConnection connection = DBConnection.openConnection();

			string encryptedPassword = EncryptingUtils.EncryptString(password, "cryptingString");

			// The command creation
			SqlCommand command = connection.CreateCommand();
			command.CommandText = "SELECT * FROM [dbo].[T_USERS] WHERE USE_name = '" + username + "' AND USE_password = '" + encryptedPassword + "'";

			// We execute the command
			SqlDataReader dataReader = command.ExecuteReader();

			// We read the data per columns
			while (dataReader.Read())
				return int.Parse(dataReader["USE_id"].ToString());

			return 0;
		}

		#endregion
	}
}
