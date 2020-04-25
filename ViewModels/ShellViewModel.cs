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
	public class ShellViewModel : INotifyPropertyChanged
	{
        #region Attributes

        private ObservableCollection<TaskViewModel> tasks = new ObservableCollection<TaskViewModel>();
		private ObservableCollection<DayViewModel> days = new ObservableCollection<DayViewModel>();
		private static int bitOfDay = -3;
		private string nameOfDay;
		private string taskTime;
		private string date;
		public event PropertyChangedEventHandler PropertyChanged;


		public ObservableCollection<TaskViewModel> Tasks
		{
			get { return tasks; }
			set
			{
				if (tasks != value)
				{
					tasks = value;
					NotifyPropertyChanged(nameof(Tasks));
				}
			}
		}

		public static int BitOfDay
		{
			get { return bitOfDay; }
			set { bitOfDay = value; }
		}

		public string NameOfDay
		{
			get { return DaysOfMonth(); }
			set { nameOfDay = value; }
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

		public string actualMonthYear
		{
			get { return FindActualMonthYear(DateTime.Now); }
			set { actualMonthYear = value; }
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
		public int TaskDay { get; set; }

		public ICommand TaskCommand { get { return new TaskCommand(); } }

		public ObservableCollection<DayViewModel> Days
		{
			get { return FillDaysCollection(days); }
			set 
			{
				if (days != value)
				{
					days = value;
					NotifyPropertyChanged(nameof(Tasks));
				}
			}
		}

        #endregion

        #region Methods

        public ObservableCollection<DayViewModel> FillDaysCollection(ObservableCollection<DayViewModel> daysCollection)
		{
			daysCollection.Add(new DayViewModel("1"));
			daysCollection.Add(new DayViewModel("2"));
			daysCollection.Add(new DayViewModel("3"));

			return daysCollection;
		}

		public static string DateTimeToString(DateTime date)
		{
			int index = (date.ToString()).IndexOf('/');
			return date.ToString().Substring(0, index);
		}

		public static string FindActualMonthYear(DateTime date)
		{
			int index = (date.ToString()).IndexOf('/');
			string month = date.ToString().Substring(index + 1, 2);
			string year = date.ToString().Substring(index + 4, 4);


			switch (month)
			{
				case "01":
					return "JANVIER " + year;
				case "02":
					return "FÉVRIER " + year;
				case "03":
					return "MARS " + year;
				case "04":
					return "AVRIL " + year;
				case "05":
					return "MAI " + year;
				case "06":
					return "JUIN " + year;
				case "07":
					return "JUILLET " + year;
				case "08":
					return "AOÛT " + year;
				case "09":
					return "SEPTEMBRE " + year;
				case "10":
					return "OCTOBRE " + year;
				case "11":
					return "NOVEMBRE " + year;
				case "12":
					return "DECEMBRE " + year;
				default:
					return "error";
			}
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

		public static string DaysOfMonth()
		{
			bitOfDay++;
			string DayOfMonth = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, bitOfDay)).DayOfWeek.ToString();
			string numberDayOfMonth = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, bitOfDay)).ToString();

			if (numberDayOfMonth[0] == '0')
				numberDayOfMonth = numberDayOfMonth.Substring(1, 1);
			else
				numberDayOfMonth = numberDayOfMonth.Substring(0, 2);

			switch (DayOfMonth.ToString())
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

			return DayOfMonth + " " + numberDayOfMonth;
		}

		#endregion
    }
}
