using Ore.Models;
using Ore.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Ore.ViewModels
{
	public class ShellViewModel : INotifyPropertyChanged         // Charger les jours en forme de liste
	{
        #region Attributes

        private static ObservableCollection<TaskViewModel> tasks = new ObservableCollection<TaskViewModel>();
		private ObservableCollection<DayViewModel> days = new ObservableCollection<DayViewModel>();
		public event PropertyChangedEventHandler PropertyChanged;
		private static int bitOfDay = -3;
		private string taskTime;
		private string date;
		private string taskMonth = FindActualMonthYear(DateTime.Now, 0);
		private int id = 0;
		private string[] daysInMonth;

		public string[] DaysInMonth
		{
			get { return ArrayOfDaysInMonth(convertMonthInt(taskMonth)); }
			set 
			{ 
				daysInMonth = value;
				NotifyPropertyChanged(nameof(DaysInMonth));
			}
		}


		private static char bitMonthActualisation;

		public static char BitMonthActualisation
		{
			get { return bitMonthActualisation; }
			set { bitMonthActualisation = value; }
		}


		public ObservableCollection<DayViewModel> Days
		{
			get { return setDaysInMonth(days); }
			set { days = value; }
		}


		public ObservableCollection<TaskViewModel> Tasks
		{
			get { return tasks; }
			set
			{
				tasks = value;
				NotifyPropertyChanged(nameof(Tasks));
			}
		}

		public static int BitOfDay
		{
			get { return bitOfDay; }
			set { bitOfDay = value; }
		}

		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		public string actualMonday {
			get { return "LUN " + lastMonday(DateTime.Now).ToString(); }
			set { actualMonday = value; }
		}

		public string actualTuesday
		{
			get { return "MAR " + (lastMonday(DateTime.Now) + 1).ToString(); }
			set { actualTuesday = value; }
		}

		public string actualWednesday
		{
			get { return "MER " + (lastMonday(DateTime.Now) + 2).ToString(); }
			set { actualWednesday = value; }
		}

		public string actualThursday
		{
			get { return "JEU " + (lastMonday(DateTime.Now) + 3).ToString(); }
			set { actualThursday = value; }
		}

		public string actualFriday
		{
			get { return "VEN " + (lastMonday(DateTime.Now) + 4).ToString(); }
			set { actualFriday = value; }
		}

		public string actualSaturday
		{
			get { return "SAM " + (lastMonday(DateTime.Now) + 5).ToString(); }
			set { actualSaturday = value; }
		}

		public string actualSunday
		{
			get { return "DIM " + (lastMonday(DateTime.Now) + 6).ToString(); }
			set { actualSunday = value; }
		}


		private string actualMonthYear = FindActualMonthYear(DateTime.Now, 1);

		public string ActualMonthYear
		{
			get { return actualMonthYear; }
			set 
			{ 
				actualMonthYear = value;
				NotifyPropertyChanged(nameof(ActualMonthYear));
			}
		}



		public string TaskColor { get; set; }

		public string TaskName { get; set; }

		public string TaskTime
		{
			get { return FullDateToTime(taskTime); }
			set { taskTime = value; }
		}

		public string Date
		{
			get { return DateTimeToString(DateTime.Now); }
			set { date = value; }
		}

		public string TaskMonth
		{
			get { return taskMonth; }
			set 
			{ 
				taskMonth = value;
				NotifyPropertyChanged(nameof(TaskMonth));
			}
		}

		public int TaskDay { get { return 1; } set { TaskDay = value; } }

		public ICommand TaskCommand { get { return new TaskCommand(); } }
		public ICommand DeleteCommand { get { return new DeleteCommand(); } }
		public ICommand RightArrowMonthCommand { get { return new RightArrowMonthCommand(this); } }
		public ICommand LeftArrowMonthCommand { get { return new LeftArrowMonthCommand(this); } }

		#endregion


		#region Methods

		public static string DateTimeToString(DateTime date)
		{
			int index = (date.ToString()).IndexOf('/');
			return date.ToString().Substring(0, index);
		}

		public static string FindActualMonthYear(DateTime date, int yearFlag)
		{
			int index = (date.ToString()).IndexOf('/');
			string month = date.ToString().Substring(index + 1, 2);
			string year = date.ToString().Substring(index + 4, 4);
			string convertedAnswer;


			switch (month)
			{
				case "01":
					convertedAnswer = "JANVIER " + year;
					break;
				case "02":
					convertedAnswer = "FÉVRIER " + year;
					break;
				case "03":
					convertedAnswer = "MARS " + year;
					break;
				case "04":
					convertedAnswer = "AVRIL " + year;
					break;
				case "05":
					convertedAnswer = "MAI " + year;
					break;
				case "06":
					convertedAnswer = "JUIN " + year;
					break;
				case "07":
					convertedAnswer = "JUILLET " + year;
					break;
				case "08":
					convertedAnswer = "AOÛT " + year;
					break;
				case "09":
					convertedAnswer = "SEPTEMBRE " + year;
					break;
				case "10":
					convertedAnswer = "OCTOBRE " + year;
					break;
				case "11":
					convertedAnswer = "NOVEMBRE " + year;
					break;
				case "12":
					convertedAnswer = "DECEMBRE " + year;
					break;
				default:
					convertedAnswer = "error";
					break;
			}

			if (yearFlag == 1)
				return convertedAnswer;

			string[] converterAnswerSplitted = convertedAnswer.Split(' ');
			return converterAnswerSplitted[0].ToLower();

		}

		public int lastMonday(DateTime date)
		{
			string actualDay = date.DayOfWeek.ToString();
			int actualDateDay = Int32.Parse(date.ToString().Substring(0, (date.ToString()).IndexOf('/')));

			switch (actualDay)
			{
				case "Monday":
					return actualDateDay;
				case "Tuesday":
					return (actualDateDay - 1);
				case "Wednesday":
					return (actualDateDay - 2);
				case "Thursday":
					return (actualDateDay - 3);
				case "Friday":
					return (actualDateDay - 4);
				case "Saturday":
					return (actualDateDay - 5);
				case "Sunday":
					return (actualDateDay - 6);
				default:
					return 0;
			}
		}

		public string FullDateToTime(string fulldate)
		{
			if (fulldate == null)
				return "";

			string[] dateSplitted = fulldate.Split(' ');
			string[] timeSplitted = dateSplitted[1].Split(':');

			if(dateSplitted[2] == "PM")
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

		private void NotifyPropertyChanged(String name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		public static void retrieveDataFromModel(string month)
		{
			ObservableCollection<TaskViewModel> tempList = ShellModel.retrieveDataFromDatabase(month);

			if(tempList != null)
			{
				foreach(TaskViewModel task in tempList)
				{
					tasks.Add(new TaskViewModel() { _name = task._name, _complete = task._complete, _day = task._day, _time = task._time, _color = task._color, _id = task._id });
				}
			}
		}

		public ObservableCollection<DayViewModel> setDaysInMonth(ObservableCollection<DayViewModel> days)
		{
			int nbOfDays = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

			for(int i = 1; i <= nbOfDays; i++)
			{
				days.Add(new DayViewModel() { Number = i });
			}

			return days;
		}

		public void changeMonthUp()
		{
			string[] monthYearSplitted = ActualMonthYear.Split(' ');

			switch (monthYearSplitted[0])
			{
				case "JANVIER":
					monthYearSplitted[0] = "FÉVRIER";
					break;
				case "FÉVRIER":
					monthYearSplitted[0] = "MARS";
					break;
				case "MARS":
					monthYearSplitted[0] = "AVRIL";
					break;
				case "AVRIL":
					monthYearSplitted[0] = "MAI";
					break;
				case "MAI":
					monthYearSplitted[0] = "JUIN";
					break;
				case "JUIN":
					monthYearSplitted[0] = "JUILLET";
					break;
				case "JUILLET":
					monthYearSplitted[0] = "AOÛT";
					break;
				case "AOÛT":
					monthYearSplitted[0] = "SEPTEMBRE";
					break;
				case "SEPTEMBRE":
					monthYearSplitted[0] = "OCTOBRE";
					break;
				case "OCTOBRE":
					monthYearSplitted[0] = "NOVEMBRE";
					break;
				case "NOVEMBRE":
					monthYearSplitted[0] = "DÉCEMBRE";
					break;
				case "DÉCEMBRE":
					monthYearSplitted[0] = "JANVIER";
					monthYearSplitted[1] = (int.Parse(monthYearSplitted[1]) + 1).ToString();
					break;
				default:
					monthYearSplitted[0] = monthYearSplitted[1] = "error";
					break;
			}

			ActualMonthYear = TaskMonth = String.Join(" ", monthYearSplitted);

			string[] ActualMonthYearSplitted = ActualMonthYear.Split(' ');
			TaskMonth = ActualMonthYearSplitted[0].ToString().ToLower();
		}

		public void changeMonthDown()
		{
			string[] monthYearSplitted = ActualMonthYear.Split(' ');

			switch (monthYearSplitted[0])
			{
				case "JANVIER":
					monthYearSplitted[0] = "DÉCEMBRE";
					monthYearSplitted[1] = (int.Parse(monthYearSplitted[1]) - 1).ToString();
					break;
				case "FÉVRIER":
					monthYearSplitted[0] = "JANVIER";
					break;
				case "MARS":
					monthYearSplitted[0] = "FÉVRIER";
					break;
				case "AVRIL":
					monthYearSplitted[0] = "MARS";
					break;
				case "MAI":
					monthYearSplitted[0] = "AVRIL";
					break;
				case "JUIN":
					monthYearSplitted[0] = "MAI";
					break;
				case "JUILLET":
					monthYearSplitted[0] = "JUIN";
					break;
				case "AOÛT":
					monthYearSplitted[0] = "JUILLET";
					break;
				case "SEPTEMBRE":
					monthYearSplitted[0] = "AOÛT";
					break;
				case "OCTOBRE":
					monthYearSplitted[0] = "SEPTEMBRE";
					break;
				case "NOVEMBRE":
					monthYearSplitted[0] = "OCTOBRE";
					break;
				case "DÉCEMBRE":
					monthYearSplitted[0] = "NOVEMBRE";
					break;
				default:
					monthYearSplitted[0] = monthYearSplitted[1] = "error";
					break;
			}

			ActualMonthYear = String.Join(" ", monthYearSplitted);

			string[] ActualMonthYearSplitted = ActualMonthYear.Split(' ');
			TaskMonth = ActualMonthYearSplitted[0].ToString().ToLower();
		}

		public int convertMonthInt(string month)
		{
			switch (month)
			{
				case "janvier":
					return 1;
				case "février":
					return 2;
				case "mars":
					return 3;
				case "avril":
					return 4;
				case "mai":
					return 5;
				case "juin":
					return 6;
				case "juillet":
					return 7;
				case "août":
					return 8;
				case "septembre":
					return 9;
				case "octobre":
					return 10;
				case "novembre":
					return 11;
				case "décembre":
					return 12;
				default:
					return 0;
			}
		}

		public string[] ArrayOfDaysInMonth(int month)
		{
			string[] daysArray = new string[32];
			string DayOfMonth;
			int nbOfDays = DateTime.DaysInMonth(2020, month);

			for (int i = 1; i < nbOfDays; i++)
			{
				DayOfMonth = (new DateTime(int.Parse(DateTime.Now.Year.ToString()), month, i)).DayOfWeek.ToString();

				switch (DayOfMonth)
				{
					case "Monday":
						DayOfMonth = "LUNDI";
						break;
					case "Tuesday":
						DayOfMonth = "MARDI";
						break;
					case "Wednesday":
						DayOfMonth = "MERCREDI";
						break;
					case "Thursday":
						DayOfMonth = "JEUDI";
						break;
					case "Friday":
						DayOfMonth = "VENDREDI";
						break;
					case "Saturday":
						DayOfMonth = "SAMEDI";
						break;
					case "Sunday":
						DayOfMonth = "DIMANCHE";
						break;
					default:
						DayOfMonth = "error";
						break;
				}

				daysArray[i] = DayOfMonth + " " + i.ToString();
			}

			return daysArray;
		}

		#endregion
	}
}
