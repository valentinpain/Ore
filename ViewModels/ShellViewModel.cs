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
	public class ShellViewModel : INotifyPropertyChanged // Année de fin
	{
        #region Attributes

        private static ObservableCollection<TaskViewModel> tasks = new ObservableCollection<TaskViewModel>();
		private ObservableCollection<DayViewModel> daysInMonth = new ObservableCollection<DayViewModel>();

		private ObservableCollection<TaskViewModel> toDoNowTasks = ShellModel.retrieveAllTasks(1);

		public event PropertyChangedEventHandler PropertyChanged;

		private int taskId = 0;
		private string taskName;
		private string taskDescription;
		private string taskColor;
		private string taskStartDay;
		private string taskFinishDay;
		private string taskStartTime;
		private string taskFinishTime;
		private string taskStartMonth = monthNumberToName(DateTime.Now.Month.ToString());
		private string taskFinishMonth;
		private string taskYear = DateTime.Now.Year.ToString();
		private bool taskChecked;
		private int taskIdUser = 1;

		private string[] monthsTab = {"Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre" };

		private static string randomColor;
		private string[] yearsTab;


		public static string RandomColor
		{
			get { return randomColor; }
			set 
			{ 
				randomColor = value;
			}
		}




		public string[] MonthsTab
		{
			get { return monthsTab; }
			set { monthsTab = value; }
		}

		public string TaskColor
		{
			get { return taskColor; }
			set 
			{ 
				taskColor = value;
				NotifyPropertyChanged(TaskColor);
			}
		}

		public ObservableCollection<TaskViewModel> ToDoNowTasks
		{
			get { return SortTasks(ShellModel.retrieveAllTasks(1)); }
			set 
			{ 
				toDoNowTasks = value;
				NotifyPropertyChanged(nameof(ToDoNowTasks));
			}
		}

		public string TaskFinishMonth
		{
			get { return taskFinishMonth; }
			set { taskFinishMonth = value; }
		}


		public string[] YearsTab
		{
			get { return setYears(); }
			set { yearsTab = value; }
		}

		public string TaskStartDay
		{
			get { return taskStartDay; }
			set 
			{ 
				taskStartDay = value;
				NotifyPropertyChanged(TaskStartDay);
			}
		}

		public string TaskFinishDay
		{
			get { return taskFinishDay; }
			set 
			{ 
				taskFinishDay = value;
				NotifyPropertyChanged(TaskFinishDay);
			}
		}

		public string TaskStartTime
		{
			get { return taskStartTime; }
			set 
			{ 
				taskStartTime = value;
				NotifyPropertyChanged(taskStartTime);
			}
		}

		public string TaskFinishTime
		{
			get { return taskFinishTime; }
			set 
			{ 
				taskFinishTime = value;
				NotifyPropertyChanged(TaskFinishTime);
			}
		}

		public ObservableCollection<DayViewModel> DaysInMonth
		{
			get { return setDaysInMonth(taskYear, taskStartMonth); }
			set 
			{ 
				daysInMonth = value;
				NotifyPropertyChanged(nameof(DaysInMonth));
			}
		}

		private static string chosenDate;

		public static string ChosenDate
		{
			get { return chosenDate; }
			set { chosenDate = value; }
		}

		public string TaskDescription
		{
			get { return taskDescription; }
			set 
			{ 
				taskDescription = value;
				NotifyPropertyChanged(TaskDescription);
			}
		}


		private DateTime actualDate;

		public DateTime ActualDate
		{
			get { return DateTime.Now ; }
			set { actualDate = value; }
		}

		public int TaskIdUser
		{
			get { return taskIdUser; }
			set { taskIdUser = value; }
		}


		public string TaskStartMonth
		{
			get { return taskStartMonth; }
			set 
			{
				taskStartMonth = value;
				NotifyPropertyChanged(nameof(TaskStartMonth));
				NotifyPropertyChanged(nameof(DaysInMonth));
			}
		}

		public string TaskYear
		{
			get { return taskYear; }
			set 
			{
				taskYear = value;
				NotifyPropertyChanged(nameof(taskYear));
				NotifyPropertyChanged(nameof(DaysInMonth));
			}
		}


		private static char bitMonthActualisation;

		public static char BitMonthActualisation
		{
			get { return bitMonthActualisation; }
			set { bitMonthActualisation = value; }
		}


		public ObservableCollection<TaskViewModel> Tasks
		{
			get { return tasks; }
			set
			{
				tasks = value;
				NotifyPropertyChanged(nameof(Tasks));
				NotifyPropertyChanged(nameof(ToDoNowTasks));
			}
		}

		public int TaskId
		{
			get { return taskId; }
			set { taskId = value; }
		}

		public bool TaskChecked
		{
			get { return taskChecked; }
			set { taskChecked = value; }
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

		public string TaskName
		{
			get { return taskName; }
			set 
			{ 
				taskName = value;
				NotifyPropertyChanged(TaskName);
			}
		}

		public int TaskDay { get { return 1; } set { TaskDay = value; } }

		public ICommand TaskCommand { get { return new TaskCommand(this); } }
		public ICommand DeleteCommand { get { return new DeleteCommand(this); } }
		public ICommand RightArrowMonthCommand { get { return new RightArrowMonthCommand(this); } }
		public ICommand LeftArrowMonthCommand { get { return new LeftArrowMonthCommand(this); } }
		public ICommand LoginAccessCommand { get { return new LoginAccessCommand(this); } }
		public ICommand LoadDayViewCommand { get { return new LoadDayViewCommand(this); } }
		public ICommand LoadHomeViewCommand { get { return new LoadHomeViewCommand(this); } }

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

		public string EnglishDayOfWeekToFrench(string day)
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

		public string[] setYears()
		{
			string[] years = new string[150];

			for(int i = 0; i < 150; i++) 
			{
				years[i] = (int.Parse(DateTime.Now.Year.ToString()) + i).ToString();
			}

			return years;
		}

		public ObservableCollection<DayViewModel> setDaysInMonth(string chosenYear, string chosenMonth)
		{
			if(chosenMonth.Length > 2)
			{
				chosenMonth = monthNameToNumber(chosenMonth);
			}

			ObservableCollection<DayViewModel> daysInMonth = new ObservableCollection<DayViewModel>();
			int numberOfDays = DateTime.DaysInMonth(int.Parse(chosenYear), int.Parse(chosenMonth));

			for(int i = 0; i < numberOfDays; i++)
			{
				int dayNumber = i + 1;
				DateTime dayNotConverted = new DateTime(int.Parse(chosenYear), int.Parse(chosenMonth), i + 1);

				switch (dayNotConverted.DayOfWeek.ToString())
				{
					case "Monday":
						daysInMonth.Add(new DayViewModel("Lundi " + dayNumber.ToString(), isToday(dayNotConverted)));
						break;
					case "Tuesday":
						daysInMonth.Add(new DayViewModel("Mardi " + dayNumber.ToString(), isToday(dayNotConverted)));
						break;
					case "Wednesday":
						daysInMonth.Add(new DayViewModel("Mercredi " + dayNumber.ToString(), isToday(dayNotConverted)));
						break;
					case "Thursday":
						daysInMonth.Add(new DayViewModel("Jeudi " + dayNumber.ToString(), isToday(dayNotConverted)));
						break;
					case "Friday":
						daysInMonth.Add(new DayViewModel("Vendredi " + dayNumber.ToString(), isToday(dayNotConverted)));
						break;
					case "Saturday":
						daysInMonth.Add(new DayViewModel("Samedi " + dayNumber.ToString(), isToday(dayNotConverted)));
						break;
					case "Sunday":
						daysInMonth.Add(new DayViewModel("Dimanche " + dayNumber.ToString(), isToday(dayNotConverted)));
						break;
				}
			}

			return daysInMonth;
		}

		public bool isToday(DateTime day)
		{
			string[] daySplitted = day.ToString().Split(' ');
			string[] actualdaySplitted = DateTime.Now.ToString().Split(' ');

			if (daySplitted[0] == actualdaySplitted[0])
				return true;

			return false;
		}

		public string monthNameToNumber(string monthName)
		{
			switch (taskStartMonth)
			{
				case "Janvier":
					return "1";
				case "Février":
					return "2";
				case "Mars":
					return "3";
				case "Avril":
					return "4";
				case "Mai":
					return "5";
				case "Juin":
					return "6";
				case "Juillet":
					return "7";
				case "Août":
					return "8";
				case "Septembre":
					return "9";
				case "Octobre":
					return "10";
				case "Novembre":
					return "11";
				case "Décembre":
					return "12";
				default:
					return "error";
			}
		}

		public static string monthNumberToName(string monthNumber)
		{
			switch (monthNumber)
			{
				case "1":
					return "Janvier";
				case "2":
					return "Février";
				case "3":
					return "Mars";
				case "4":
					return "Avril";
				case "5":
					return "Mai";
				case "6":
					return "Juin";
				case "7":
					return "Juillet";
				case "8":
					return "Août";
				case "9":
					return "Septembre";
				case "10":
					return "Octobre";
				case "11":
					return "Novembre";
				case "12":
					return "Décembre";
				default:
					return "error";
			}
		}

		public ObservableCollection<TaskViewModel> SortTasks(ObservableCollection<TaskViewModel> tasksList)
		{
			ObservableCollection<TaskViewModel> sortedTasksList = new ObservableCollection<TaskViewModel>();
			TaskViewModel[] sortedTasks = tasksList.ToArray();
			bool isSorted = false;
			TaskViewModel memory;

			while (!isSorted)
			{
				isSorted = true;

				for(int i = 0; i < sortedTasks.Length - 1; i++)
				{
					if (int.Parse(sortedTasks[i + 1].year) < int.Parse(sortedTasks[i].year))
					{
						memory = sortedTasks[i];
						sortedTasks[i] = sortedTasks[i + 1];
						sortedTasks[i + 1] = memory;

						isSorted = false;
					}
					else if (int.Parse(sortedTasks[i + 1].year) == int.Parse(sortedTasks[i].year))
					{
						if (int.Parse(monthNameToNumber(sortedTasks[i + 1].month)) < int.Parse(monthNameToNumber(sortedTasks[i].month)))
						{
							memory = sortedTasks[i];
							sortedTasks[i] = sortedTasks[i + 1];
							sortedTasks[i + 1] = memory;

							isSorted = false;
						}
						else if (int.Parse(monthNameToNumber(sortedTasks[i + 1].month)) == int.Parse(monthNameToNumber(sortedTasks[i].month)))
						{
							string[] startDaySplitted1 = sortedTasks[i].startDay.Split(' ');
							string[] startDaySplitted2 = sortedTasks[i + 1].startDay.Split(' ');

							if (int.Parse(startDaySplitted2[1].ToString()) < int.Parse(startDaySplitted1[1].ToString()))
							{
								memory = sortedTasks[i];
								sortedTasks[i] = sortedTasks[i + 1];
								sortedTasks[i + 1] = memory;

								isSorted = false;
							}
						}
					}
				}
			}

			for (int i = 0; i < sortedTasks.Length; i++)
			{
				sortedTasksList.Add(sortedTasks[i]);
			}

			return sortedTasksList;
		}

		#endregion
	}
}
