using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ore.ViewModels
{
	/// <summary>
	/// The view-model class of a list that decides how a view list must behave
	/// </summary>
	public class ListViewModel
    {
		#region Properties

		/// <summary>
		/// The id of the list
		/// </summary>
		private int idList;
		public int IdList
		{
			get { return idList; }
			set { idList = value; }
		}

		/// <summary>
		/// The name of the list currently used by the <c>ShellView</c> view
		/// </summary>
		private string focusedName;
		public string FocusedName
		{
			get { return focusedName; }
			set { focusedName = value; }
		}

		/// <summary>
		/// The name of the list
		/// </summary>
		private string name;
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		/// <summary>
		/// The id of the connected user
		/// </summary>
		private int userId;
		public int UserId
		{
			get { return userId; }
			set { userId = value; }
		}

		/// <summary>
		/// The tasks of the list
		/// </summary>
		private ObservableCollection<TaskViewModel> taskList;
		public ObservableCollection<TaskViewModel> TaskList
		{
			get { return taskList; }
			set { taskList = value; }
		}

		#endregion

		#region Constructor

		#endregion

		#region Methods

		#endregion

	}
}
