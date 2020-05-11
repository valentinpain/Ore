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

namespace Ore.Models                                       
{
	/// <summary>
	/// The main model class that communicates with the database for the main view and view-model
	/// </summary>
    public class ShellModel
	{
		#region Properties

		#endregion

		#region Methods

		/// <summary>
		/// Retrieve all the data of only one day
		/// </summary>
		/// <param name="day">The focused day</param>
		/// <param name="month">The focused month</param>
		/// <param name="year">The focused year</param>
		/// <param name="idUser">The user id</param>
		/// <returns>All the tasks of the focused day in a list</returns>
		public static ObservableCollection<TaskViewModel> retrieveDayTasksFromDatabase(int day, int month, int year, int idUser)
		{
			ObservableCollection<TaskViewModel> tasksFromDatabase = new ObservableCollection<TaskViewModel>();

			SqlConnection connection = DBConnection.openConnection();

			SqlCommand command = connection.CreateCommand();
			command.CommandText = "SELECT * FROM [dbo].[T_TASKS] WHERE CAST(TAS_startYear AS int) <= " + year + " AND CAST(TAS_finishYear AS int) >= " + year + " AND USE_id = " + idUser + " AND LIS_id IS NULL";

			SqlDataReader dataReader = command.ExecuteReader();

			while (dataReader.Read())
			{
				bool checkedTask;

				// Converting a int into a boolean value
				if (int.Parse(dataReader["TAS_checked"].ToString()) == 1)
					checkedTask = true;
				else
					checkedTask = false;

				// Instantiate the different datetimes used to know if a task is in the interval of days
				DateTime startDate = new DateTime(int.Parse(dataReader["TAS_startYear"].ToString()), int.Parse(dataReader["TAS_startMonth"].ToString()), int.Parse(dataReader["TAS_startDay"].ToString().Substring(dataReader["TAS_startDay"].ToString().IndexOf(' '), dataReader["TAS_startDay"].ToString().Length - dataReader["TAS_startDay"].ToString().IndexOf(' '))));
				DateTime focusedDate = new DateTime(year, month, day);
				DateTime finishDate = new DateTime(int.Parse(dataReader["TAS_finishYear"].ToString()), int.Parse(dataReader["TAS_finishMonth"].ToString()), int.Parse(dataReader["TAS_finishDay"].ToString().Substring(dataReader["TAS_finishDay"].ToString().IndexOf(' '), dataReader["TAS_finishDay"].ToString().Length - dataReader["TAS_finishDay"].ToString().IndexOf(' '))));

				int compareStart = DateTime.Compare(focusedDate, startDate);
				int compareFinish = DateTime.Compare(focusedDate, finishDate);

				if ((compareStart >= 0) && (compareFinish <= 0))
				{
					// Adding the task belonging to the day
					tasksFromDatabase.Add(new TaskViewModel()
					{
						Id = int.Parse(dataReader["TAS_id"].ToString()),
						Name = dataReader["TAS_name"].ToString(),
						Description = dataReader["TAS_description"].ToString(),
						Color = dataReader["TAS_color"].ToString(),
						StartDay = dataReader["TAS_startDay"].ToString(),
						FinishDay = dataReader["TAS_finishDay"].ToString(),
						StartTime = dataReader["TAS_startTime"].ToString(),
						FinishTime = dataReader["TAS_finishTime"].ToString(),
						StartMonth = ShellViewModel.monthNumberToName(dataReader["TAS_startMonth"].ToString()),
						FinishMonth = ShellViewModel.monthNumberToName(dataReader["TAS_finishMonth"].ToString()),
						StartYear = dataReader["TAS_startYear"].ToString(),
						FinishYear = dataReader["TAS_finishYear"].ToString(),
						IsComplete = checkedTask,
						UseId = int.Parse(dataReader["USE_id"].ToString())
					});
				}
			}

			return tasksFromDatabase;
		}

		/// <summary>
		/// Gives the last id used for a task in the database
		/// </summary>
		/// <returns>The last task id</returns>
		public static int lastRowTaskNumber()
		{
			int rowNumber;

			SqlConnection connection = DBConnection.openConnection();

			SqlCommand command = connection.CreateCommand();
			command.CommandText = "SELECT MAX(TAS_id) AS 'rowNumber' FROM [dbo].[T_TASKS]";

			SqlDataReader dataReader = command.ExecuteReader();

			if (dataReader.Read() && int.TryParse(dataReader["rowNumber"].ToString(), out rowNumber))
				return rowNumber;

			return 0;
		}

		/// <summary>
		/// Deletes a task in the database whether it belongs to a list or not
		/// </summary>
		/// <param name="idTask">The task id in the database</param>
		public static void deleteTask(int idTask)
		{
			SqlConnection connection = DBConnection.openConnection();

			SqlCommand command = connection.CreateCommand();
			command.CommandText = "DELETE FROM [dbo].[T_TASKS] WHERE TAS_id = " + idTask;

			SqlDataReader dataReader = command.ExecuteReader();
		}

		/// <summary>
		/// Deletes a list in the database and all the tasks associated
		/// </summary>
		/// <param name="idList">The list id</param>
		public static void deleteList(int idList)
		{
			SqlConnection connection = DBConnection.openConnection();

			// First command to delete the tasks
			SqlCommand firstCommand = connection.CreateCommand();
			firstCommand.CommandText = "DELETE FROM [dbo].[T_TASKS] WHERE LIS_id = " + idList;

			SqlDataReader firstDataReader = firstCommand.ExecuteReader();

			// Closing the data reader to execute an other command
			firstDataReader.Close();

			// Second command to delete the list
			SqlCommand secondCommand = connection.CreateCommand();
			secondCommand.CommandText = "DELETE FROM [dbo].[T_LISTS] WHERE LIS_id = " + idList;

			SqlDataReader secondDataReader = secondCommand.ExecuteReader();
		}

		/// <summary>
		/// Adds a task to the database with all its informations
		/// </summary>
		/// <param name="task">The task we want to add</param>
		public static void addTaskToDatabase(TaskViewModel task)
		{
			SqlConnection connection = DBConnection.openConnection();

			string sql = "INSERT INTO [dbo].[T_TASKS] (TAS_id, TAS_name, TAS_description, TAS_color, TAS_startDay, TAS_finishDay, TAS_startTime, TAS_finishTime, TAS_startMonth, TAS_finishMonth, TAS_startYear, TAS_finishYear, TAS_checked, USE_id, LIS_id) VALUES (@id, @name, @description, @color, @startDay, @finishDay, @startTime, @finishTime, @startMonth, @finishMonth, @startYear, @finishYear, @checked, @user_id, @list_id)";
			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				// Adding all the informations of the task
				command.Parameters.AddWithValue("@id", task.Id);
				command.Parameters.AddWithValue("@name", task.Name);
				command.Parameters.AddWithValue("@description", task.Description);
				command.Parameters.AddWithValue("@color", task.Color);
				command.Parameters.AddWithValue("@startDay", task.StartDay);
				command.Parameters.AddWithValue("@finishDay", task.FinishDay);
				command.Parameters.AddWithValue("@startTime", task.StartTime);
				command.Parameters.AddWithValue("@finishTime", task.FinishTime);
				command.Parameters.AddWithValue("@startMonth", ShellViewModel.monthNameToNumber(task.StartMonth));
				command.Parameters.AddWithValue("@finishMonth", ShellViewModel.monthNameToNumber(task.FinishMonth));
				command.Parameters.AddWithValue("@startYear", task.StartYear);
				command.Parameters.AddWithValue("@finishYear", task.FinishYear);
				command.Parameters.AddWithValue("@checked", task.IsComplete);
				command.Parameters.AddWithValue("@user_id", task.UseId);

				if(task.ListId == 0)
					command.Parameters.AddWithValue("@list_id", DBNull.Value);
				else
					command.Parameters.AddWithValue("@list_id", task.ListId);

				command.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// Retrieves to the behavior code all the different tasks of the user that are not in a list
		/// </summary>
		/// <param name="idUser">The user id</param>
		/// <returns>All the tasks in a list</returns>
		public static ObservableCollection<TaskViewModel> retrieveAllTasks(int idUser)
		{
			ObservableCollection<TaskViewModel> tasksFromDatabase = new ObservableCollection<TaskViewModel>();

			SqlConnection connection = DBConnection.openConnection();

			SqlCommand command = connection.CreateCommand();

			if (idUser == 0)
				return new ObservableCollection<TaskViewModel>();

			command.CommandText = "SELECT * FROM [dbo].[T_TASKS] WHERE USE_id = " + idUser + " AND TAS_checked = 0 AND LIS_id IS NULL";

			SqlDataReader dataReader = command.ExecuteReader();

			while (dataReader.Read())
			{
				bool checkedTask;

				if (int.Parse(dataReader["TAS_checked"].ToString()) == 1)
					checkedTask = true;
				else
					checkedTask = false;

				tasksFromDatabase.Add(new TaskViewModel()
				{
					Id = int.Parse(dataReader["TAS_id"].ToString()),
					Name = dataReader["TAS_name"].ToString(),
					Description = dataReader["TAS_description"].ToString(),
					Color = dataReader["TAS_color"].ToString(),
					StartDay = dataReader["TAS_startDay"].ToString(),
					FinishDay = dataReader["TAS_finishDay"].ToString(),
					StartTime = dataReader["TAS_startTime"].ToString(),
					FinishTime = dataReader["TAS_finishTime"].ToString(),
					StartMonth = ShellViewModel.monthNumberToName(dataReader["TAS_startMonth"].ToString()),
					FinishMonth = ShellViewModel.monthNumberToName(dataReader["TAS_finishMonth"].ToString()),
					StartYear = dataReader["TAS_startYear"].ToString(),
					FinishYear = dataReader["TAS_finishYear"].ToString(),
					IsComplete = checkedTask,
					UseId = int.Parse(dataReader["USE_id"].ToString())
				});
			}

			return tasksFromDatabase;
		}

		/// <summary>
		/// Retrieves to the behavior code all the different lists created by the user
		/// </summary>
		/// <param name="idUser">The user id</param>
		/// <returns>All the lists in a list</returns>
		public static ObservableCollection<ListViewModel> retrieveAllLists(int idUser)
		{
			ObservableCollection<ListViewModel> listsFromDatabase = new ObservableCollection<ListViewModel>();

			SqlConnection connection = DBConnection.openConnection();

			SqlCommand command = connection.CreateCommand();

			if (idUser == 0)
				return new ObservableCollection<ListViewModel>();

			command.CommandText = "SELECT * FROM [dbo].[T_LISTS] WHERE USE_id = " + idUser;

			SqlDataReader dataReader = command.ExecuteReader();

			while (dataReader.Read())
			{
				listsFromDatabase.Add(new ListViewModel()
				{
					IdList = int.Parse(dataReader["LIS_id"].ToString()),
					Name = dataReader["LIS_name"].ToString()
				});
			}

			return listsFromDatabase;
		}

		/// <summary>
		/// Checks the task checked information in the database using her id
		/// </summary>
		/// <param name="idTask">The task id</param>
		public static void checkTask(int idTask)
		{
			SqlConnection connection = DBConnection.openConnection();

			SqlCommand command = connection.CreateCommand();
			command.CommandText = "UPDATE [dbo].[T_TASKS] SET TAS_checked = 1 WHERE TAS_id = " + idTask;

			SqlDataReader dataReader = command.ExecuteReader();
		}

		/// <summary>
		/// Unchecks the task checked information in the database using her id
		/// </summary>
		/// <param name="idTask">The task id</param>
		public static void unCheckTask(int idTask)
		{
			SqlConnection connection = DBConnection.openConnection();

			SqlCommand command = connection.CreateCommand();
			command.CommandText = "UPDATE [dbo].[T_TASKS] SET TAS_checked = 0 WHERE TAS_id = " + idTask;

			SqlDataReader dataReader = command.ExecuteReader();
		}

		/// <summary>
		/// Checks if the task is checked in the database
		/// </summary>
		/// <param name="idTask">The task id</param>
		/// <returns>A boolean value that indicates whether the task is checked or not in the database</returns>
		public static bool isChecked(int idTask)
		{
			SqlConnection connection = DBConnection.openConnection();

			SqlCommand command = connection.CreateCommand();
			command.CommandText = "SELECT * FROM [dbo].[T_TASKS] WHERE TAS_id = " + idTask;

			SqlDataReader dataReader = command.ExecuteReader();

			while (dataReader.Read())
			{
				if (int.Parse(dataReader["TAS_checked"].ToString()) == 1)
					return true;
			}

			return false;
		}

		/// <summary>
		/// Finds the last id used to save a list in the database
		/// </summary>
		/// <returns>The last list id used</returns>
		public static int lastListRowNumber()
		{
			int rowNumber;

			SqlConnection connection = DBConnection.openConnection();

			SqlCommand command = connection.CreateCommand();
			command.CommandText = "SELECT MAX(LIS_id) AS 'rowNumber' FROM [dbo].[T_LISTS]";

			SqlDataReader dataReader = command.ExecuteReader();

			if (dataReader.Read() && int.TryParse(dataReader["rowNumber"].ToString(), out rowNumber))
				return rowNumber;

			return 0;
		}

		/// <summary>
		/// Adds a list to the database with its informations
		/// </summary>
		/// <param name="list">The list id</param>
		public static void addListToDatabase(ListViewModel list)
		{
			SqlConnection connection = DBConnection.openConnection();

			string sql = "INSERT INTO [dbo].[T_LISTS] (LIS_id, LIS_name, USE_id) VALUES (@id, @name, @userId)";
			using (SqlCommand command = new SqlCommand(sql, connection))
			{
				// Adding the list informations
				command.Parameters.AddWithValue("@id", list.IdList);
				command.Parameters.AddWithValue("@name", list.Name);
				command.Parameters.AddWithValue("@userId", list.UserId);
				command.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// Retrieves all the tasks that belongs to a certain list
		/// </summary>
		/// <param name="listId">The list id</param>
		/// <returns>The tasks belonging to the list in a list</returns>
		public static ObservableCollection<TaskViewModel> retrieveTaskListFromDatabase(int listId)
		{
			ObservableCollection<TaskViewModel> tasksFromDatabase = new ObservableCollection<TaskViewModel>();

			SqlConnection connection = DBConnection.openConnection();

			SqlCommand command = connection.CreateCommand();
			command.CommandText = "SELECT * FROM [dbo].[T_TASKS] WHERE LIS_id = " + listId;

			SqlDataReader dataReader = command.ExecuteReader();

			while (dataReader.Read())
			{
				bool checkedTask;

				if (int.Parse(dataReader["TAS_checked"].ToString()) == 1)
					checkedTask = true;
				else
					checkedTask = false;

				tasksFromDatabase.Add(new TaskViewModel() 
				{
					Id = int.Parse(dataReader["TAS_id"].ToString()),
					Name = dataReader["TAS_name"].ToString(),
					Description = dataReader["TAS_description"].ToString(),
					Color = dataReader["TAS_color"].ToString(),
					StartDay = dataReader["TAS_startDay"].ToString(),
					FinishDay = dataReader["TAS_finishDay"].ToString(),
					StartTime = dataReader["TAS_startTime"].ToString(),
					FinishTime = dataReader["TAS_finishTime"].ToString(),
					StartMonth = ShellViewModel.monthNumberToName(dataReader["TAS_startMonth"].ToString()),
					FinishMonth = ShellViewModel.monthNumberToName(dataReader["TAS_finishMonth"].ToString()),
					StartYear = dataReader["TAS_startYear"].ToString(),
					FinishYear = dataReader["TAS_finishYear"].ToString(),
					IsComplete = checkedTask,
					UseId = int.Parse(dataReader["USE_id"].ToString())
				});
			}

			return tasksFromDatabase;
		}

        #endregion

    }
}
