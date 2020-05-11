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
	/// <summary>
	/// The view-model class of the register view that decides how a register view must behave
	/// </summary>
	public class RegisterViewModel : INotifyPropertyChanged
    {
		#region Properties

		/// <summary>
		/// The user that wants to create an account
		/// </summary>
		private UserViewModel user = new UserViewModel();
		public UserViewModel User
		{
			get { return user; }
			set { user = value; }
		}

		/// <summary>
		/// The attributes that decides whether the user has entered valid informations
		/// </summary>
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

		/// <summary>
		/// The informations displayed when the user submit the form
		/// </summary>
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

		/// <summary>
		/// The last od used in the database to create an account
		/// </summary>
		private int lastUserId = RegisterModel.lastRowUserNumber() + 1;
		public int LastUserId
		{
			get { return lastUserId; }
			set { lastUserId = value; }
		}

		/// <summary>
		/// The attributes that prevents a change in the properties values
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region The command properties

		/// <summary>
		/// The command that register a new user
		/// </summary>
		public ICommand RegisterCommand { get { return new RegisterCommand(this); } }

		/// <summary>
		/// The command that leads the user back to the log in view
		/// </summary>
		public ICommand GoBackToConnectionCommand { get { return new GoBackToConnectionCommand(this); } }

		#endregion

		#region Constructor

		#endregion

		#region Methods

		/// <summary>
		/// The methods that notifies a change in the properties values
		/// </summary>
		/// <param name="name">The name of the property</param>
		private void NotifyPropertyChanged(String name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		#endregion

	}
}
