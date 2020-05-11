using Ore.Models.DatabaseConnection;
using Ore.Models.EncryptingData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ore.Models
{
	/// <summary>
	/// The model that contains elements to communicate with the database to retrieve register data
	/// </summary>
	public class RegisterModel
    {
		#region Properties

		#endregion

		#region Methods

		/// <summary>
		/// Finds the id of the user registered in the database if he exists
		/// </summary>
		/// <returns>The last user id</returns>
		public static int lastRowUserNumber()
		{
			int rowNumber;

			SqlConnection connection = DBConnection.openConnection();

			SqlCommand command = connection.CreateCommand();
			command.CommandText = "SELECT MAX(USE_id) AS 'rowNumber' FROM [dbo].[T_USERS]";

			SqlDataReader dataReader = command.ExecuteReader();

			if (dataReader.Read() && int.TryParse(dataReader["rowNumber"].ToString(), out rowNumber))
				return rowNumber;

			return 0;
		}

		/// <summary>
		/// Creates a new user in the database with his connection informations and gives him a personnal id
		/// </summary>
		/// <param name="id">The user id</param>
		/// <param name="username">The user name</param>
		/// <param name="password">The user password</param>
        public static void InsertNewAccountInDatabase(int id, string username, string password)
        {
			SqlConnection connection = DBConnection.openConnection();

			string encryptedPassword = EncryptingUtils.EncryptString(password, "cryptingString");

			// Prepared statement
			string sql = "INSERT INTO [dbo].[T_USERS] (USE_id, USE_name, USE_password) VALUES (@id, @username, @password)";
			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue("@id", id);
				command.Parameters.AddWithValue("@username", username);
				command.Parameters.AddWithValue("@password", encryptedPassword);
				command.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// Checks if a user is already existing in the databases with connection informations
		/// </summary>
		/// <param name="username">The user name</param>
		/// <param name="password">The user password</param>
		/// <returns>The boolean value that indicates whether the user exists or not</returns>
		public static bool isUserAlreadyCreated(string username, string password)
		{
			SqlConnection connection = DBConnection.openConnection();

			string encryptedPassword = EncryptingUtils.EncryptString(password, "cryptingString");

			SqlCommand command = connection.CreateCommand();
			command.CommandText = "SELECT * FROM [dbo].[T_USERS] WHERE USE_name = '" + username + "' AND USE_password = '" + encryptedPassword + "'";

			SqlDataReader dataReader = command.ExecuteReader();

			if (dataReader.Read())
				return true;

			return false;
		}

        #endregion
    }
}
