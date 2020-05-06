using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ore.ViewModels
{
    public class TaskViewModel
    {

        // Les traitements concernant les tâches doivent se faire ici !
		// propfull à faire

        public int id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public string color { get; set; }

        public string startDay { get; set; }

        public string finishDay { get; set; }

        public string startTime { get; set; }

        public string finishTime { get; set; }

        public string startMonth { get; set; }

		public string finishMonth { get; set; }

        public string startYear { get; set; }

        public string finishYear { get; set; }

        public bool isComplete { get; set; }

        public int useId { get; set; }

        
        public static string formatDay(string day)
        {
            string[] daySplitted = day.Split('/');
            string[] yearSplitted = daySplitted[2].Split(' ');

            return setDaysInMonth(int.Parse(yearSplitted[0]), int.Parse(daySplitted[0]), int.Parse(daySplitted[1]));
        }

        public static string formatTime(string time)
        {
			if (time == null)
				return "";

			string[] dateSplitted = time.Split(' ');
			string[] timeSplitted = dateSplitted[1].Split(':');

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

		public static string[] setDate(string dateNotFormatted)
		{
			string[] formattedDate = new string[3];
			string[] dateNotFormattedSplittedByDay = dateNotFormatted.Split('/');

			formattedDate[0] = EnglishDayOfWeekToFrench(new DateTime(int.Parse(dateNotFormattedSplittedByDay[2]), int.Parse(dateNotFormattedSplittedByDay[0]), int.Parse(dateNotFormattedSplittedByDay[1])).DayOfWeek.ToString()) + " " + dateNotFormattedSplittedByDay[1];
			formattedDate[1] = ShellViewModel.monthNumberToName(dateNotFormattedSplittedByDay[0]);
			formattedDate[2] = dateNotFormattedSplittedByDay[2];

			return formattedDate;
		}

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
	}
}
