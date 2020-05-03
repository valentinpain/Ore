using Ore.Models;
using Ore.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ore.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
		private UserViewModel user = new UserViewModel();

		public UserViewModel User
		{
			get { return user; }
			set { user = value; }
		}

		public ICommand RegisterCommand { get { return new RegisterCommand(this); } }
		public ICommand GoBackToConnectionCommand { get { return new GoBackToConnectionCommand(this); } }

		private string wrongInformations = "Collapsed";

		private string textInformations = "*** Informations incorrectes ***";

		public string TextInformations
		{
			get { return textInformations; }
			set 
			{ 
				textInformations = value;
				NotifyPropertyChanged(nameof(TextInformations));
			}
		}

		private int lastUserId = RegisterModel.lastRowNumberUser() + 1;

		public int LastUserId
		{
			get { return lastUserId; }
			set { lastUserId = value; }
		}



		public string WrongInformations
		{
			get { return wrongInformations; }
			set
			{
				wrongInformations = value;
				NotifyPropertyChanged(nameof(WrongInformations));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(String name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
