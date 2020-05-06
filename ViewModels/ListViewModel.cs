using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ore.ViewModels
{
    public class ListViewModel
    {
		private static string focusedName;

		public static string FocusedName
		{
			get { return focusedName; }
			set { focusedName = value; }
		}


		private string name;

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		private ObservableCollection<TaskViewModel> taskList;

		public ObservableCollection<TaskViewModel> TaskList
		{
			get { return taskList; }
			set { taskList = value; }
		}

	}
}
