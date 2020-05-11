using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ore.ViewModels
{
	/// <summary>
	/// The view-model class of a day that decides how a view day must behave
	/// </summary>
    public class DayViewModel
    {
		#region Properties

		/// <summary>
		/// The name of the day
		/// </summary>
		private string name;
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		/// <summary>
		/// The flag that says whether this today is today or not
		/// </summary>
		private bool isToday;
		public bool IsToday
		{
			get { return isToday; }
			set { isToday = value; }
		}

		#endregion

		#region Constructor

		/// <summary>
		/// The constructor of a full day
		/// </summary>
		/// <param name="name">The name of the day</param>
		/// <param name="isToday">The flag that says whether this today is today or not</param>
		public DayViewModel(string name, bool isToday)
		{
			this.name = name;
			this.isToday = isToday;
		}

		#endregion

		#region Methods

		#endregion

	}
}
