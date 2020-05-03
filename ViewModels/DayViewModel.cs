using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ore.ViewModels
{
    public class DayViewModel
    {
		private string name;

		private bool isToday;

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public bool IsToday
		{
			get { return isToday; }
			set { isToday = value; }
		}

		public DayViewModel(string name, bool isToday)
		{
			this.name = name;
			this.isToday = isToday;
		}

	}
}
