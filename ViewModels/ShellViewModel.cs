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
	/// <summary>
	/// The main view-model class that decides how the whole main interface must behave
	/// </summary>
	public class ShellViewModel : INotifyPropertyChanged
	{
        #region Attributes

		 /// <summary>
		 /// The lists of all the tasks that the connected user created
		 /// </summary>
        private static ObservableCollection<TaskViewModel> tasks = new ObservableCollection<TaskViewModel>();
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

		/// <summary>
		/// The list of the most urgent tasks the connected user created
		/// </summary>
		private static ObservableCollection<TaskViewModel> toDoNowTasks = new ObservableCollection<TaskViewModel>();
		public static ObservableCollection<TaskViewModel> ToDoNowTasks
		{
			get { return toDoNowTasks; }
			set { toDoNowTasks = value; }
		}

		/// <summary>
		/// The list of all the lists the connected user created
		/// </summary>
		private static ObservableCollection<ListViewModel> lists = new ObservableCollection<ListViewModel>();
		public static ObservableCollection<ListViewModel> Lists
		{
			get { return lists; }
			set { lists = value; }
		}

		/// <summary>
		/// The list of all the days in the ficused month
		/// </summary>
		private ObservableCollection<DayViewModel> daysInMonth = new ObservableCollection<DayViewModel>();
		public ObservableCollection<DayViewModel> DaysInMonth
		{
			get { return setDaysInMonth(taskStartYear, taskStartMonth); }
			set
			{
				daysInMonth = value;
				NotifyPropertyChanged(nameof(DaysInMonth));
			}
		}

		/// <summary>
		/// The list that the user chose in the view
		/// </summary>
		private static ObservableCollection<TaskViewModel> focusedList = new ObservableCollection<TaskViewModel>();
		public static ObservableCollection<TaskViewModel> FocusedList
		{
			get { return focusedList; }
			set { focusedList = value; }
		}

		/// <summary>
		/// The list of months displayed in the shell view so the user can pick one
		/// </summary>
		private string[] monthsTab = { "Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre" };
		public string[] MonthsTab
		{
			get { return monthsTab; }
			set { monthsTab = value; }
		}

		/// <summary>
		/// The list of years displayed in the shell view so the user can pick one
		/// </summary>
		private string[] yearsTab;
		public string[] YearsTab
		{
			get { return setYears(); }
			set { yearsTab = value; }
		}

		/// <summary>
		/// The actual month-year values when the program starts
		/// </summary>
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

		/// <summary>
		/// The focused date chosen by the user
		/// </summary>
		private static string chosenDate;
		public static string ChosenDate
		{
			get { return chosenDate; }
			set { chosenDate = value; }
		}

		/// <summary>
		/// The focused month chosen by the user
		/// </summary>
		private static string chosenMonth;
		public static string ChosenMonth
		{
			get { return chosenMonth; }
			set { chosenMonth = value; }
		}

		/// <summary>
		/// The focused yer chosen by the user
		/// </summary>
		private static string chosenYear;
		public static string ChosenYear
		{
			get { return chosenYear; }
			set { chosenYear = value; }
		}

		/// <summary>
		/// The id of the task the user is creating
		/// </summary>
		private int taskId = 0;
		public int TaskId
		{
			get { return taskId; }
			set { taskId = value; }
		}

		/// <summary>
		/// The name of the task the user is creating
		/// </summary>
		private string taskName;
		public string TaskName
		{
			get { return taskName; }
			set
			{
				taskName = value;
				NotifyPropertyChanged(TaskName);
			}
		}

		/// <summary>
		/// The description of the task the user is creating
		/// </summary>
		private string taskDescription;
		public string TaskDescription
		{
			get { return taskDescription; }
			set
			{
				taskDescription = value;
				NotifyPropertyChanged(TaskDescription);
			}
		}

		/// <summary>
		/// The color of the task the user is creating
		/// </summary>
		private string taskColor;
		public string TaskColor
		{
			get { return taskColor; }
			set
			{
				taskColor = value;
				NotifyPropertyChanged(TaskColor);
			}
		}

		/// <summary>
		/// The start day of the task the user is creating
		/// </summary>
		private string taskStartDay;
		public string TaskStartDay
		{
			get { return taskStartDay; }
			set
			{
				taskStartDay = value;
				NotifyPropertyChanged(TaskStartDay);
			}
		}

		/// <summary>
		/// The finish day of the task the user is creating
		/// </summary>
		private string taskFinishDay;
		public string TaskFinishDay
		{
			get { return taskFinishDay; }
			set
			{
				taskFinishDay = value;
				NotifyPropertyChanged(TaskFinishDay);
			}
		}

		/// <summary>
		/// The start time of the task the user is creating
		/// </summary>
		private string taskStartTime;
		public string TaskStartTime
		{
			get { return taskStartTime; }
			set
			{
				taskStartTime = value;
				NotifyPropertyChanged(taskStartTime);
			}
		}

		/// <summary>
		/// The finish time of the task the user is creating
		/// </summary>
		private string taskFinishTime;
		public string TaskFinishTime
		{
			get { return taskFinishTime; }
			set
			{
				taskFinishTime = value;
				NotifyPropertyChanged(TaskFinishTime);
			}
		}

		/// <summary>
		/// The start month of the task the user is creating
		/// </summary>
		private string taskStartMonth = monthNumberToName(DateTime.Now.Month.ToString());
		public string TaskStartMonth
		{
			get { return taskStartMonth; }
			set
			{
				this.taskStartMonth = value;
				NotifyPropertyChanged(nameof(TaskStartMonth));
				NotifyPropertyChanged(nameof(DaysInMonth));
			}
		}

		/// <summary>
		/// The finish month of the task the user is creating
		/// </summary>
		private string taskFinishMonth;
		public string TaskFinishMonth
		{
			get { return taskFinishMonth; }
			set
			{
				taskFinishMonth = value;
				NotifyPropertyChanged(nameof(TaskFinishMonth));
			}
		}

		/// <summary>
		/// The start year of the task the user is creating
		/// </summary>
		private string taskStartYear = DateTime.Now.Year.ToString();
		public string TaskStartYear
		{
			get { return taskStartYear; }
			set
			{
				taskStartYear = value;
				NotifyPropertyChanged(nameof(TaskStartYear));
				NotifyPropertyChanged(nameof(DaysInMonth));
			}
		}

		/// <summary>
		/// The finish year of the task the user is creating
		/// </summary>
		private string taskFinishYear;
		public string TaskFinishYear
		{
			get { return taskFinishYear; }
			set
			{
				taskFinishYear = value;
				NotifyPropertyChanged(nameof(TaskFinishYear));
			}
		}

		/// <summary>
		/// The task-done flag of the task the user is creating
		/// </summary>
		private bool taskChecked;
		public bool TaskChecked
		{
			get { return taskChecked; }
			set
			{
				taskChecked = value;
			}
		}

		/// <summary>
		/// The id of the user that is creating the task
		/// </summary>
		private static int taskIdUser = LoginViewModel.User.Id;
		public static int TaskIdUser
		{
			get { return taskIdUser; }
			set { taskIdUser = value; }
		}

		/// <summary>
		/// The boolean value that tells whether the user has entered wrong informations or not
		/// </summary>
		private bool wrongInformations = false;
		public bool WrongInformations
		{
			get { return wrongInformations; }
			set
			{
				wrongInformations = value;
				NotifyPropertyChanged(nameof(WrongInformations));
				NotifyPropertyChanged(nameof(TextInformations));
			}
		}

		/// <summary>
		/// The warning text informations the user gets when he has entered wrong informations
		/// </summary>
		private string textInformations;
		public string TextInformations
		{
			get { return textInformations; }
			set
			{
				textInformations = value;
				NotifyPropertyChanged(nameof(TextInformations));
				NotifyPropertyChanged(nameof(WrongInformations));
			}
		}

		/// <summary>
		/// The name of the actual focused list in the main view
		/// </summary>
		private static string focusedListName;
		public static string FocusedListName
		{
			get { return focusedListName; }
			set { focusedListName = value; }
		}

		/// <summary>
		/// The id of the focused list in the main view 
		/// </summary>
		private static int focusedListId = ShellModel.lastListRowNumber();
		public static int FocusedListId
		{
			get { return focusedListId; }
			set { focusedListId = value; }
		}

		/// <summary>
		/// The color randomly chosen for the task or list view
		/// </summary>
		private static string randomColor;
		public static string RandomColor
		{
			get { return randomColor; }
			set
			{
				randomColor = value;
			}
		}

		/// <summary>
		/// The attributes that prevents a new change in the properties values
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region The command properties

		/// <summary>
		/// The command that adds a new task created by the user
		/// </summary>
		public ICommand AddATaskCommand { get { return new AddATaskCommand(this); } }

		/// <summary>
		/// The command that deletes a task
		/// </summary>
		public ICommand DeleteATaskCommand { get { return new DeleteATaskCommand(this); } }

		/// <summary>
		/// The command that load the day view of the one chosen by the user
		/// </summary>
		public ICommand LoadDayViewCommand { get { return new LoadDayViewCommand(this); } }

		/// <summary>
		/// The command that loads the home view
		/// </summary>
		public ICommand LoadHomeViewCommand { get { return new LoadHomeViewCommand(this); } }

		/// <summary>
		/// The command that checks a task when it is done
		/// </summary>
		public ICommand TaskDoneCommand { get { return new TaskDoneCommand(this); } }

		/// <summary>
		/// The command that disconnects the user from the main view and leads him back to the log in view
		/// </summary>
		public ICommand DisconnectUserCommand { get { return new DisconnectUserCommand(this); } }

		/// <summary>
		/// The command that adds a new list created by the user
		/// </summary>
		public ICommand AddAListCommand { get { return new AddAListCommand(this); } }

		/// <summary>
		/// The command that loads the list view
		/// </summary>
		public ICommand LoadListViewCommand { get { return new LoadListViewCommand(this); } }

		/// <summary>
		/// The command that adds a new task to a list
		/// </summary>
		public ICommand AddTaskToListCommand { get { return new AddTaskToListCommand(this); } }

		/// <summary>
		/// The command that deletes a list
		/// </summary>
		public ICommand DeleteListCommand { get { return new DeleteListCommand(this); } }

		/// <summary>
		/// The command that displays the form to create a new list
		/// </summary>
		public ICommand ListCommand { get { return new ListCommand(this); } }

		#endregion

		#region Methods

		/// <summary>
		/// Finds the actual year-month values when the program starts
		/// </summary>
		/// <param name="date">The actual date when the program starts</param>
		/// <param name="yearFlag">The format mode flag</param>
		/// <returns>The actual month and year in a string</returns>
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

		/// <summary>
		/// Sets the list of available years to create a task
		/// </summary>
		/// <returns>An arrays of the available years</returns>
		public string[] setYears()
		{
			string[] years = new string[150];

			for(int i = 0; i < 150; i++)
				years[i] = (int.Parse(DateTime.Now.Year.ToString()) + i).ToString();

			return years;
		}

		/// <summary>
		/// Sets all the days in a list with the focused month-year values
		/// </summary>
		/// <param name="chosenYear">The focused year</param>
		/// <param name="chosenMonth">The focused month</param>
		/// <returns>The list of all the days in the month-year</returns>
		public ObservableCollection<DayViewModel> setDaysInMonth(string chosenYear, string chosenMonth)
		{
			if(chosenMonth.Length > 2)
				chosenMonth = monthNameToNumber(chosenMonth);

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

		/// <summary>
		/// Checks if the day sent is the actual real day the program is started
		/// </summary>
		/// <param name="day">The day</param>
		/// <returns>A boolean value that indicates whether the day is the actual one or not</returns>
		public bool isToday(DateTime day)
		{
			string[] daySplitted = day.ToString().Split(' ');
			string[] actualdaySplitted = DateTime.Now.ToString().Split(' ');

			if (daySplitted[0] == actualdaySplitted[0])
				return true;

			return false;
		}

		/// <summary>
		/// Converts a month name to his number in the year
		/// </summary>
		/// <param name="monthName">The name of the month</param>
		/// <returns>The number of the month in the year</returns>
		public static string monthNameToNumber(string monthName)
		{
			switch (monthName)
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

		/// <summary>
		/// Converts a month number in the year to his real name
		/// </summary>
		/// <param name="monthNumber">The number of the month in the year</param>
		/// <returns>The real name of the month</returns>
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

		/// <summary>
		/// Sorts the most urgent tasks in the basic tasks list
		/// </summary>
		/// <param name="tasksList">The list of tasks retrieved from the database</param>
		public static void SortTasks(ObservableCollection<TaskViewModel> tasksList)
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
					if (int.Parse(sortedTasks[i + 1].FinishYear) < int.Parse(sortedTasks[i].FinishYear))
					{
						memory = sortedTasks[i];
						sortedTasks[i] = sortedTasks[i + 1];
						sortedTasks[i + 1] = memory;

						isSorted = false;
					}
					else if (int.Parse(sortedTasks[i + 1].FinishYear) == int.Parse(sortedTasks[i].FinishYear))
					{
						if (int.Parse(monthNameToNumber(sortedTasks[i + 1].FinishMonth)) < int.Parse(monthNameToNumber(sortedTasks[i].FinishMonth)))
						{
							memory = sortedTasks[i];
							sortedTasks[i] = sortedTasks[i + 1];
							sortedTasks[i + 1] = memory;

							isSorted = false;
						}
						else if (int.Parse(monthNameToNumber(sortedTasks[i + 1].FinishMonth)) == int.Parse(monthNameToNumber(sortedTasks[i].FinishMonth)))
						{
							string[] finishDaySplitted1 = sortedTasks[i].FinishDay.Split(' ');
							string[] finishDaySplitted2 = sortedTasks[i + 1].FinishDay.Split(' ');

							if (int.Parse(finishDaySplitted2[1].ToString()) < int.Parse(finishDaySplitted1[1].ToString()))
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

			ToDoNowTasks.Clear();

			for (int i = 0; i < sortedTasks.Length; i++)
			{
				ToDoNowTasks.Add(sortedTasks[i]);
			}
		}

		/// <summary>
		/// Adds a new task to our basic tasks list of a day
		/// </summary>
		public void addTask()
		{

			//Adding a task inside our program for a day
			Tasks.Add(new TaskViewModel()
			{
				Id = TaskId,
				Name = TaskName,
				Description = TaskDescription,
				Color = TaskColor,
				StartDay = ChosenDate,
				FinishDay = TaskFinishDay,
				StartTime = TaskViewModel.formatTime(TaskStartTime),
				FinishTime = TaskViewModel.formatTime(TaskFinishTime),
				StartMonth = ChosenMonth,
				FinishMonth = TaskFinishMonth,
				StartYear = ChosenYear,
				FinishYear = TaskFinishYear,
				IsComplete = false,
				UseId = LoginViewModel.User.Id,
				ListId = 0
			});
		}

		/// <summary>
		/// Adds a new task to a list created by the user
		/// </summary>
		public void addTaskToList()
		{
			//Adding a task inside our program in a list
			FocusedList.Add(new TaskViewModel()
			{
				Id = TaskId,
				Name = TaskName,
				Description = TaskDescription,
				Color = TaskColor,
				StartDay = TaskStartDay,
				FinishDay = TaskFinishDay,
				StartTime = TaskViewModel.formatTime(TaskStartTime),
				FinishTime = TaskViewModel.formatTime(TaskFinishTime),
				StartMonth = TaskStartMonth,
				FinishMonth = TaskFinishMonth,
				StartYear = TaskStartYear,
				FinishYear = TaskFinishYear,
				IsComplete = false,
				UseId = LoginViewModel.User.Id,
				ListId = ShellViewModel.FocusedListId
			});
		}

		/// <summary>
		/// Prevents the code that a value property changed
		/// </summary>
		/// <param name="name">The name of the property</param>
		private void NotifyPropertyChanged(String name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		#endregion
	}
}
