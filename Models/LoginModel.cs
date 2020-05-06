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
    public class LoginModel
    {
		public static int findUserId(string username, string password)
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

			SqlCommand command = connection.CreateCommand();
			command.CommandText = "SELECT * FROM [dbo].[T_USERS] WHERE USE_name = '" + username + "' AND USE_password = '" + encryptedPassword + "'";

			SqlDataReader dataReader = command.ExecuteReader();

			while (dataReader.Read())
				return int.Parse(dataReader["USE_id"].ToString());

			return 0;
		}
    }
}
