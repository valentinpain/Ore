using Ore.Models.DatabaseConnection;
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
        public static bool isUserRegistered(string username, string password)
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

			while (dataReader.Read())
					return true;

			return false;

		}
    }
}
