using Ore.Models.DatabaseConnection;
using Ore.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ore.Models                                        // PENSER A RENOMMER LES CLASSES !!! + ATTRIBUTS DE CLASSES LOGIQUES Ex : Days contient des tâches
{
    public class ShellModel
	{
		private string name;

		private string color;

		private string time;

		private int id;

		public int Id
		{
			get { return id; }
			set { id = value; }
		}


		public string Color
		{
			get { return color; }
			set 
			{ 
				color = value;
			}
		}

		public string Time
		{
			get { return time; }
			set { time = value; }
		}



		public ShellModel(string name)
		{
			this.Name = name;
		}

		public ShellModel()
		{

		}

		public string Name
		{
			get { return name; }
			set
			{
				name = value;
			}
		}

		public static ObservableCollection<TaskViewModel> retrieveDataFromDatabase(string month)
		{
			/* Récupération de la chaîne de connexion dans un objet de type "SqlConnection"
               grâce à nos méthodes précédemment définies */

			ObservableCollection<TaskViewModel> tasksFromDatabase = new ObservableCollection<TaskViewModel>();

			SqlConnection connection = DBUtils.GetDBConnection();

			try
			{
				// Connexion à la base de données 
				connection.Open();
			}
			catch (Exception e)
			{
				// Message renvoyé en cas d'erreur
				Console.WriteLine("Error: " + e.Message);
			}

			// Instanciation d'une requête SQL avec le type "SqlCommand"
			SqlCommand command = connection.CreateCommand();
			command.CommandText = "SELECT * FROM [dbo].[T_TASKS] WHERE TAS_month = '" + month + "'";

			/* Exécution de la requête par la base de données et stockage
               de celles-ci dans une variable qui nous aidera à lire
               ces données */
			SqlDataReader dataReader = command.ExecuteReader();

			// Lecture et traitement des données récupérées à l'aide d'une boucle
		    while (dataReader.Read())
			{
				bool checkedTask;

				if (int.Parse(dataReader["TAS_checked"].ToString()) == 1)
					checkedTask = true;
				else
					checkedTask = false;

				tasksFromDatabase.Add(new TaskViewModel() { _name = dataReader["TAS_description"].ToString(), _complete = checkedTask, _day = int.Parse(dataReader["TAS_day"].ToString()), _time = dataReader["TAS_time"].ToString(), _color = dataReader["TAS_color"].ToString(), _id = int.Parse(dataReader["TAS_id"].ToString()) });
			}

			return tasksFromDatabase;
		}


		public static int lastRowNumber()
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
			command.CommandText = "SELECT MAX(TAS_id) AS 'rowNumber' FROM [dbo].[T_TASKS]";

			SqlDataReader dataReader = command.ExecuteReader();

			if (dataReader.Read() && int.TryParse(dataReader["rowNumber"].ToString(), out rowNumber))
				return rowNumber;

			return 0;
		}

		public static void deleteTask(int idTask)
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
			command.CommandText = "DELETE FROM [dbo].[T_TASKS] WHERE TAS_id = " + idTask;

			SqlDataReader dataReader = command.ExecuteReader();
		}

		public static void addTaskToDatabase(TaskViewModel task)
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

			string sql = "INSERT INTO [dbo].[T_TASKS] (TAS_id, TAS_description, TAS_color, TAS_day, TAS_time, TAS_month, TAS_year, TAS_checked, USE_id) VALUES (@id, @description, @color, @day, @time, @month, @year, @checked, @user_id)";
			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue("@id", task._id);
				command.Parameters.AddWithValue("@description", task._name);
				command.Parameters.AddWithValue("@color", task._color);
				command.Parameters.AddWithValue("@day", task._day);
				command.Parameters.AddWithValue("@time", task._time);
				command.Parameters.AddWithValue("@month", task._month);
				command.Parameters.AddWithValue("@year", "2020");
				command.Parameters.AddWithValue("@checked", 0);
				command.Parameters.AddWithValue("@user_id", 1);
				command.ExecuteNonQuery();
			}
		}
	}
}
