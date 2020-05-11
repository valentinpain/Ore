using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ore.ViewModels
{
	/// <summary>
	/// The view-model class of the task view that decides how a task view must behave
	/// </summary>
	public class TaskViewModel
	{
		#region Properties 

		/// <summary>
		/// The id of the task
		/// </summary>
		private int id;
		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		/// <summary>
		/// The name of the task
		/// </summary>
		private string name;
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		/// <summary>
		/// The description of the task
		/// </summary>
		private string description;
		public string Description
		{
			get { return description; }
			set { description = value; }
		}

		/// <summary>
		/// The color of the task
		/// </summary>
		private string color;
		public string Color
		{
			get { return color; }
			set { color = value; }
		}

		/// <summary>
		/// The start day of the task
		/// </summary>
		private string startDay;
		public string StartDay
		{
			get { return startDay; }
			set { startDay = value; }
		}

		/// <summary>
		/// The finish day of the day
		/// </summary>
		private string finishDay;
		public string FinishDay
		{
			get { return finishDay; }
			set { finishDay = value; }
		}

		/// <summary>
		/// The start time of the task
		/// </summary>
		private string startTime;
		public string StartTime
		{
			get { return startTime; }
			set { startTime = value; }
		}

		/// <summary>
		/// The finish time of the task
		/// </summary>
		private string finishTime;
		public string FinishTime
		{
			get { return finishTime; }
			set { finishTime = value; }
		}

		/// <summary>
		/// The start month of the task
		/// </summary>
		private string startMonth;
		public string StartMonth
		{
			get { return startMonth; }
			set { startMonth = value; }
		}

		/// <summary>
		/// The finish month of the task
		/// </summary>
		private string finishMonth;
		public string FinishMonth
		{
			get { return finishMonth; }
			set { finishMonth = value; }
		}

		/// <summary>
		/// The start year of the task
		/// </summary>
		private string startYear;
		public string StartYear
		{
			get { return startYear; }
			set { startYear = value; }
		}

		/// <summary>
		/// The finish year of the task
		/// </summary>
		private string finishYear;
		public string FinishYear
		{
			get { return finishYear; }
			set { finishYear = value; }
		}

		/// <summary>
		/// The task-done flag of the task
		/// </summary>
		private bool isComplete;
		public bool IsComplete
		{
			get { return isComplete; }
			set { isComplete = value; }
		}

		/// <summary>
		/// The id of the user that created the task
		/// </summary>
		private int useId;
		public int UseId
		{
			get { return useId; }
			set { useId = value; }
		}

		/// <summary>
		/// The id of the list where belongs the task
		/// </summary>
		/// <remarks>
		/// Equals 0 if the task belongs to a day
		/// </remarks>
		private int listId;
		public int ListId
		{
			get { return listId; }
			set { listId = value; }
		}


        #endregion

        #region Constructor

        #endregion

        #region Methods

		/// <summary>
		/// The method that formats the time so we can put it in the database 
		/// </summary>
		/// <param name="time">The time we want to convert</param>
		/// <returns>The formatted time</returns>
		public static string formatTime(string time)
        {
			if (time == null)
				return "";

			string[] dateSplitted = time.Split(' ');
			string[] timeSplitted = dateSplitted[1].Split(':');

			// Check if we use the AM/PM system so we have to convert the value
			if (dateSplitted[2] == "PM")
			{
				switch (timeSplitted[0])
				{
					case "12":
						timeSplitted[0] = "00";
						break;
					case "1":
						timeSplitted[0] = "13";
						break;
					case "2":
						timeSplitted[0] = "14";
						break;
					case "3":
						timeSplitted[0] = "15";
						break;
					case "4":
						timeSplitted[0] = "16";
						break;
					case "5":
						timeSplitted[0] = "17";
						break;
					case "6":
						timeSplitted[0] = "18";
						break;
					case "7":
						timeSplitted[0] = "19";
						break;
					case "8":
						timeSplitted[0] = "20";
						break;
					case "9":
						timeSplitted[0] = "21";
						break;
					case "10":
						timeSplitted[0] = "22";
						break;
					case "11":
						timeSplitted[0] = "23";
						break;
				}
			}

			return timeSplitted[0] + "h" + timeSplitted[1];
		}

		/// <summary>
		/// Finds the day of week for a special day
		/// </summary>
		/// <param name="chosenYear">The year of the day</param>
		/// <param name="chosenMonth">The month of the day</param>
		/// <param name="chosenDay">The number of the day</param>
		/// <returns>The day of week and the number of the day in a string</returns>
		public static string setDaysInMonth(int chosenYear, int chosenMonth, int chosenDay)
		{
			int numberOfDays = DateTime.DaysInMonth(chosenYear, chosenMonth);

			for (int i = 0; i < numberOfDays; i++)
			{
				int dayNumber = i + 1;
                string dayOfWeekConverted;
				DateTime dayNotConverted = new DateTime(chosenYear, chosenMonth, i + 1);

				if (int.Parse(dayNotConverted.Day.ToString()) == chosenDay)
				{
					switch (dayNotConverted.DayOfWeek.ToString())
					{
                        case "Monday":
                            dayOfWeekConverted = "Lundi";
                            break;
                        case "Tuesday":
                            dayOfWeekConverted = "Mardi";
                            break;
                        case "Wednesday":
                            dayOfWeekConverted = "Mercredi";
                            break;
                        case "Thursday":
                            dayOfWeekConverted = "Jeudi";
                            break;
                        case "Friday":
                            dayOfWeekConverted = "Vendredi";
                            break;
                        case "Saturday":
                            dayOfWeekConverted = "Samedi";
                            break;
                        case "Sunday":
                            dayOfWeekConverted = "Dimanche";
                            break;
                        default:
                            dayOfWeekConverted = "error";
                            break;
                    }

					return dayOfWeekConverted + " " + dayNotConverted.Day;
				}
			}
			
			return "error";
		}

		/// <summary>
		/// The method that formats the day so we can put it in the database and in our code as well
		/// </summary>
		/// <param name="dateNotFormatted">The day we want to convert</param>
		/// <returns>The formatted day</returns>
		public static string[] setDate(string dateNotFormatted)
		{
			string[] formattedDate = new string[3];
			string[] dateNotFormattedSplittedByDay = dateNotFormatted.Split('/');

			formattedDate[0] = EnglishDayOfWeekToFrench(new DateTime(int.Parse(dateNotFormattedSplittedByDay[2]), int.Parse(dateNotFormattedSplittedByDay[0]), int.Parse(dateNotFormattedSplittedByDay[1])).DayOfWeek.ToString()) + " " + dateNotFormattedSplittedByDay[1];
			formattedDate[1] = ShellViewModel.monthNumberToName(dateNotFormattedSplittedByDay[0]);
			formattedDate[2] = dateNotFormattedSplittedByDay[2];

			return formattedDate;
		}

		/// <summary>
		/// The method we use to convert an english day of week to a french one
		/// </summary>
		/// <param name="day">The day of week we want to convert</param>
		/// <returns>The converted day</returns>
		public static string EnglishDayOfWeekToFrench(string day)
		{
			switch (day)
			{
				case "Monday":
					return "Lundi";
				case "Tuesday":
					return "Mardi";
				case "Wednesday":
					return "Mercredi";
				case "Thursday":
					return "Jeudi";
				case "Friday":
					return "Vendredi";
				case "Saturday":
					return "Samedi";
				case "Sunday":
					return "Dimanche";
				default:
					return "error";
			}
		}

        #endregion
    }
}
