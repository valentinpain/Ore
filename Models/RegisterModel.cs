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
    public class RegisterModel
    {
		public static int lastRowNumberUser()
		{
			int rowNumber;

			SqlConnection connection = DBUtils.GetDBConnection();

			try
			{
				connection.Open();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: " + e.Message);
			}

			SqlCommand command = connection.CreateCommand();
			command.CommandText = "SELECT MAX(USE_id) AS 'rowNumber' FROM [dbo].[T_USERS]";

			SqlDataReader dataReader = command.ExecuteReader();

			if (dataReader.Read() && int.TryParse(dataReader["rowNumber"].ToString(), out rowNumber))
				return rowNumber;

			return 0;
		}

        public static void InsertNewAccountInDatabase(int id, string username, string password)
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

			string encryptedPassword = EncryptingUtils.EncryptString(password, "test");

			string sql = "INSERT INTO [dbo].[T_USERS] (USE_id, USE_name, USE_password) VALUES (@id, @username, @password)";
			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue("@id", id);
				command.Parameters.AddWithValue("@username", username);
				command.Parameters.AddWithValue("@password", encryptedPassword);
				command.ExecuteNonQuery();
			}
		}

		public static bool isUserAlreadyCreated(string username, string password)
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

			SqlCommand command = connection.CreateCommand();
			command.CommandText = "SELECT * FROM [dbo].[T_USERS] WHERE USE_name = '" + username + "' AND USE_password = '" + password + "'";

			SqlDataReader dataReader = command.ExecuteReader();

			if (dataReader.Read())
				return true;

			return false;
		}
    }
}
