using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ore.Models
{
    public class ShellModel : INotifyPropertyChanging
	{
		private string name;

		//private DateTime time;

		private string color;

		public string Color
		{
			get { return color; }
			set 
			{ 
				color = value;
				OnPropertyChanged("Color");
			}
		}

		private string time;

		public string Time
		{
			get { return time; }
			set { time = value; }
		}



		public ShellModel(string name)
		{
			this.Name = name;
		}

		public ShellModel()
		{

		}

		public string Name
		{
			get { return name; }
			set
			{
				name = value;
				OnPropertyChanged("Name");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public event PropertyChangingEventHandler PropertyChanging;

		private void OnPropertyChanged(string property)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		/*public DateTime Time
		{
			get { return time; }
			set { time = value;
			}
		} */

		public override string ToString()
		{
			return this.name;
		}
	}
}
