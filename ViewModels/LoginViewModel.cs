using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MyToolkit.Command;
using Ore.ViewModels.Commands;

namespace Ore.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
		private UserViewModel user = new UserViewModel();

		public UserViewModel User
		{
			get { return user; }
			set { user = value; }
		}

		public ICommand LoginAccessCommand { get { return new LoginAccessCommand(this); } }
		public ICommand LoadRegisterViewCommand { get { return new LoadRegisterViewCommand(this); } }

		public Action CloseAction { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		private string wrongInformations = "Collapsed";

		public string WrongInformations
		{
			get { return wrongInformations; }
			set 
			{ 
				wrongInformations = value;
				NotifyPropertyChanged(nameof(WrongInformations));
			}
		}

		private void NotifyPropertyChanged(String name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

	}
}
